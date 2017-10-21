(function (module) {
    var borrowItemController = function ($q, activityService, inventoryService, utils) {
        var vm = this;
        var defaultBorrow = {
            type: 1,
            borrowedBy: "",
            releasedBy: "",
            date: new Date(),
            activities: []
        };
        var newActivity = {
            quantity: 0,
            isFocused: true,
            materialId: 0
        };

        // Data
        vm.borrow = {};
        vm.itemList = [];

        // Helper properties
        vm.defaultFocus = true;
        vm.saveEnabled = true;

        // Public methods
        vm.save = function () {
            utils.showLoading();
            vm.saveEnabled = false;

            for (var i = 0, l = vm.borrow.activities.length; i < l; i++) {
                vm.borrow.activities[i].borrowedBy = vm.borrow.borrowedBy;
                vm.borrow.activities[i].releasedBy = vm.borrow.releasedBy;
                vm.borrow.activities[i].date = vm.borrow.date;
            }

            activityService.saveActivity(vm.borrow)
                .then(saveSuccessful, utils.onError)
                .finally(onSaveComplete);
        };

        vm.reset = function () {
            clearForm();
        };

        vm.addItemBorrowed = function () {
            var detail = angular.copy(newActivity);
            vm.borrow.activities.splice(0, 0, detail);
        };

        // Private methods
        var clearForm = function () {
            angular.copy(defaultBorrow, vm.borrow);
            vm.defaultFocus = true;

            if (vm.adjustmentForm) {
                vm.adjustmentForm.$setPristine();
                vm.adjustmentForm.$setUntouched();
            }
        };

        var saveSuccessful = function () {
            utils.showSuccessMessage("Materials borrowed successfully.");
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

    module.controller("borrowItemController", ["$q", "activityService", "inventoryService", "utils", borrowItemController]);

})(angular.module("srisys-app"));