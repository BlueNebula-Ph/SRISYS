(function (module) {
    var addItemController = function ($scope, $q, inventoryService, loadingService, utils) {
        var vm = this;

        // Data
        var defaultItem = {};
        vm.item = {
            typeId: "1"
        };

        // Helper properties
        vm.defaultFocus = true;
        vm.showConsumableFields = false;
        vm.saveEnabled = true;

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

        // Public Methods
        vm.save = function () {
            loadingService.showLoading();
            vm.saveEnabled = false;

            inventoryService
                .saveItem(0, vm.item)
                .then(saveSuccessful, utils.onError)
                .finally(onSaveComplete);
        };

        vm.reset = function () {
            clearForm();
        };

        // Private methods
        var clearForm = function () {

        };

        var saveSuccessful = function (response) {
            utils.showSuccessMessage("Material saved successfully.");
            clearForm();
        };

        var onSaveComplete = function () {
            loadingService.hideLoading();
            vm.saveEnabled = true;
        };

        return vm;
    };

    module.controller("addItemController", ["$scope", "$q", "inventoryService", "loadingService", "utils", addItemController]);

})(angular.module("srisys-app"));