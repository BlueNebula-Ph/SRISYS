(function (module) {
    var addBorrowerController = function (borrowerService, utils) {
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

            borrowerService.saveBorrower(0, vm.borrower)
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
            clearForm();
        };

        var onSaveComplete = function () {
            utils.hideLoading();
            vm.saveEnabled = true;
        };

        return vm;
    };

    module.controller("addBorrowerController", ["borrowerService", "utils", "utils", addBorrowerController]);

})(angular.module("srisys-app"));