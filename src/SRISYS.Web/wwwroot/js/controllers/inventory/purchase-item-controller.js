(function (module) {
    var purchaseItemController = function (loadingService) {
        var vm = this;

        // Data
        vm.purchase = {};

        // Helper properties
        vm.defaultFocus = true;
        vm.saveEnabled = true;

        // Public methods
        vm.save = function () {

        };

        vm.reset = function () {

        };

        // Private methods

        return vm;
    };

    module.controller("purchaseItemController", ["loadingService", purchaseItemController]);

})(angular.module("srisys-app"));