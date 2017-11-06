(function (module) {
    var addSupplierController = function ($stateParams, supplierService, utils) {
        var vm = this;
        var defaultSupplier = {};

        // Data
        vm.supplier = {};

        // Helper properties
        vm.defaultFocus = true;
        vm.saveEnabled = true;

        // Public methods
        vm.save = function () {
            utils.showLoading();
            vm.saveEnabled = false;

            supplierService
                .saveSupplier($stateParams.id, vm.supplier)
                .then(saveSuccessful, utils.onError)
                .finally(onSaveComplete);
        };

        vm.reset = function () {
            resetForm();
        };

        // Private methods
        var resetForm = function () {
            angular.copy(defaultSupplier, vm.supplier);
            vm.defaultFocus = true;

            if (vm.addSupplierForm) {
                vm.addSupplierForm.$setPristine();
            }
        };

        var saveSuccessful = function (respose) {
            utils.showSuccessMessage("Supplier saved successfully.");

            // If edit, set defaultItem to newly saved item
            if ($stateParams.id != 0) {
                angular.copy(vm.supplier, defaultSupplier);
            }

            resetForm();
        };

        var onSaveComplete = function () {
            utils.hideLoading();
            vm.saveEnabled = true;
        };

        var processSupplier = function (response) {
            angular.copy(response.data, defaultSupplier);
            resetForm();
        };

        var loadSupplier = function () {
            if ($stateParams.id != 0) {
                utils.showLoading();

                supplierService.getSupplierById($stateParams.id)
                    .then(processSupplier, utils.onError)
                    .finally(utils.hideLoading);
            }
        };

        // Initialize
        $(function () {
            loadSupplier();
        });

        return vm;
    };

    module.controller("addSupplierController", ["$stateParams", "supplierService", "utils", addSupplierController]);

})(angular.module("srisys-app"));