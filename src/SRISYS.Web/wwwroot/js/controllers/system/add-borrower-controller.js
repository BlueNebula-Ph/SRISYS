(function (module) {
    var addBorrowerController = function ($stateParams, borrowerService, utils) {
        var vm = this;

        // Data
        var defaultBorrower = {};
        vm.borrower = {};

        // Helper properties
        vm.defaultFocus = true;
        vm.saveEnabled = true;

        // Public methods
        vm.save = function () {
            utils.showLoading();
            vm.saveEnabled = false;

            borrowerService.saveBorrower($stateParams.id, vm.borrower)
                .then(saveSuccessful, utils.onError)
                .finally(onSaveComplete);
        };

        vm.reset = function () {
            clearForm();
        };

        // Private methods
        var clearForm = function () {
            angular.copy(defaultBorrower, vm.borrower);
            vm.addBorrowerForm.$setPristine();
            vm.defaultFocus = true;
        };

        var saveSuccessful = function (respose) {
            utils.showSuccessMessage("Borrower saved successfully.");

            // If edit, update the default values
            if ($stateParams.id != 0) {
                angular.copy(vm.borrower, defaultBorrower);
            }

            clearForm();
        };

        var onSaveComplete = function () {
            utils.hideLoading();
            vm.saveEnabled = true;
        };

        // Load
        var processBorrower = function (response) {
            angular.copy(response.data, defaultBorrower);
            clearForm();
        };

        var loadBorrower = function () {
            if ($stateParams.id != 0) {
                utils.showLoading();

                borrowerService.getBorrowerById($stateParams.id)
                    .then(processBorrower, utils.onError)
                    .finally(utils.hideLoading);
            }
        };

        $(function () {
            loadBorrower();
        });

        return vm;
    };

    module.controller("addBorrowerController", ["$stateParams", "borrowerService", "utils", addBorrowerController]);

})(angular.module("srisys-app"));