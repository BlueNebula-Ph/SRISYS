(function (module) {
    var addSupplierController = function (supplierService, loadingService) {
        var vm = this;
        vm.defaultFocus = true;
        vm.supplier = {};

        vm.save = function () {
            loadingService.showLoading();
            supplierService
                .saveSupplier(0, vm.supplier)
                .then((response) => {
                    toastr.success("Supplier saved successfully.", "Success");
                }, (error) => {
                    toastr.error("An error occurred while processing your request.", "Error");
                    console.log(error);
                })
                .finally(() => { loadingService.hideLoading(); })
        };

        vm.reset = function () {

        };

        return vm;
    };

    module.controller("addSupplierController", ["supplierService", "loadingService", addSupplierController]);

})(angular.module("srisys-app"));