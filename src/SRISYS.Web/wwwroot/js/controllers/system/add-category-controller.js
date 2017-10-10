(function (module) {
    var addCategoryController = function () {
        var vm = this;
        vm.defaultFocus = true;
        vm.category = {};

        return vm;
    };

    module.controller("addCategoryController", [addCategoryController]);

})(angular.module("srisys-app"));