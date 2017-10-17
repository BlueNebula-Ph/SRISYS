(function (module) {
    var adjustItemController = function () {
        var vm = this;

        // Data
        vm.adjustment = {};

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

    module.controller("adjustItemController", [adjustItemController]);

})(angular.module("srisys-app"));