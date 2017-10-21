(function (module) {
    var adjustItemController = function ($q, inventoryService, utils) {
        var vm = this;
        var defaultAdjustment = {
            materialId: 0,
            adjustmentType: "1",
            quantity: 1,
            remarks: ""
        };

        // Data
        vm.adjustment = {};
        vm.itemList = [];

        // Helper properties
        vm.defaultFocus = true;
        vm.saveEnabled = true;

        // Public methods
        vm.save = function () {
            utils.showLoading();
            vm.saveEnabled = false;

            inventoryService.adjustQuantity(vm.adjustment)
                .then(saveSuccessful, utils.onError)
                .finally(onSaveComplete);
        };

        vm.reset = function () {
            clearForm();
        };

        // Private methods
        var clearForm = function () {
            angular.copy(defaultAdjustment, vm.adjustment);
            vm.defaultFocus = true;

            if (vm.adjustmentForm) {
                vm.adjustmentForm.$setPristine();
                vm.adjustmentForm.$setUntouched();
            }
        };

        var saveSuccessful = function () {
            utils.showSuccessMessage("Adjustment saved successfully.");
            clearForm();
        };

        var onSaveComplete = function () {
            utils.hideLoading();
            vm.saveEnabled = true;
        };

        var processItemList = function (response) {
            utils.populateDropdownlist(response, vm.itemList, "name", "-- Select Material --");
        };

        var loadAll = function () {
            utils.showLoading();

            var requests = {
                item: inventoryService.getItemLookup()
            };

            $q.all(requests)
                .then((responses) => {
                    processItemList(responses.item);
                }, utils.onError)
                .finally(utils.hideLoading);
        };

        $(function () {
            loadAll();
            clearForm();
        });

        return vm;
    };

    module.controller("adjustItemController", ["$q", "inventoryService", "utils", adjustItemController]);

})(angular.module("srisys-app"));