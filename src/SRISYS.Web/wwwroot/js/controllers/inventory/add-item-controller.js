(function (module) {
    var addItemController = function ($scope, $q, inventoryService, loadingService) {
        var vm = this;
        vm.defaultFocus = true;
        vm.item = {
            typeId: "1"
        };
        vm.showConsumableFields = false;

        // Watchers
        $scope.$watch(function () {
            return vm.item.typeId;
        }, function (newVal, oldVal) {
            if (newVal === "2") {
                vm.showConsumableFields = true;
            } else {
                vm.showConsumableFields = false;
            }

            vm.defaultFocus = true;
        });

        // Methods
        vm.save = function () {
            loadingService.showLoading();
            inventoryService
                .saveItem(0, vm.item)
                .then((response) => {
                    toastr.success("Material saved successfully.", "Success");
                }, (error) => {
                    toastr.error("An error occurred while processing the request.", "Error");
                    console.log(error);
                })
                .finally(() => { loadingService.hideLoading(); });
        };

        vm.reset = function () { };

        // Load

        return vm;
    };

    module.controller("addItemController", ["$scope", "$q", "inventoryService", "loadingService", addItemController]);

})(angular.module("srisys-app"));