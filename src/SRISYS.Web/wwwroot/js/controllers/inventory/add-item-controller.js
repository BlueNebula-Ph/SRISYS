(function (module) {
    var addItemController = function ($scope) {
        var vm = this;
        vm.defaultFocus = true;
        vm.item = {
            type: "Material"
        };
        vm.showConsumableFields = false;

        $scope.$watch(function () {
            return vm.item.type;
        }, function (newVal, oldVal) {
            if (newVal === "Consumable") {
                vm.showConsumableFields = true;
            } else {
                vm.showConsumableFields = false;
            }

            vm.defaultFocus = true;
        });

        return vm;
    };

    module.controller("addItemController", ["$scope", addItemController]);

})(angular.module("srisys-app"));