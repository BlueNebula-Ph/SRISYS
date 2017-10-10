(function (module) {
    var addSupplierController = function () {
        var vm = this;
        vm.defaultFocus = true;
        vm.supplier = {};

        return vm;
    };

    module.controller("addSupplierController", [addSupplierController]);

})(angular.module("srisys-app"));