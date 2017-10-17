(function (module) {
    var addSupplierController = function (supplierService, loadingService, utils) {
        var vm = this;

        // Data
        var defaultSupplier = {};
        vm.supplier = {};

        // Helper properties
        vm.defaultFocus = true;
        vm.saveEnabled = true;

        // Public methods
        vm.save = function () {
            loadingService.showLoading();
            vm.saveEnabled = false;

            supplierService
                .saveSupplier(0, vm.supplier)
                .then(saveSuccessful, utils.onError)
                .finally(onSaveComplete);
        };

        vm.reset = function () {
            clearForm();
        };

        // Private methods
        var clearForm = function () {
            angular.copy(defaultSupplier, vm.supplier);
            vm.addSupplierForm.$setPristine();
            vm.defaultFocus = true;
        };

        var saveSuccessful = function (respose) {
            utils.showSuccessMessage("Supplier saved successfully.");
            clearForm();
        };

        var onSaveComplete = function () {
            loadingService.hideLoading();
            vm.saveEnabled = true;
        };

        return vm;
    };

    module.controller("addSupplierController", ["supplierService", "loadingService", "utils", addSupplierController]);

})(angular.module("srisys-app"));