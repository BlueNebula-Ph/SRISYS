(function (module) {
    var addSupplierController = function (supplierService, utils) {
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
            utils.hideLoading();
            vm.saveEnabled = true;
        };

        return vm;
    };

    module.controller("addSupplierController", ["supplierService", "utils", addSupplierController]);

})(angular.module("srisys-app"));