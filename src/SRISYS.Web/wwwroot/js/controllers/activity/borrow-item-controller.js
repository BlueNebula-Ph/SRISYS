(function (module) {
    var borrowItemController = function ($q, $scope, activityService, inventoryService, borrowerService, utils) {
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
		vm.borrowerList = [];

        // Helper properties
        vm.defaultFocus = true;
        vm.saveEnabled = true;

        // Watchers
        $scope.$watch(() => { return vm.borrow.activities; },
            function (newVal, oldVal) {
                assignValues();
            }, true);

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
        var assignValues = function () {
            for (var i = 0, l = vm.borrow.activities.length; i < l; i++) {
                var item = vm.borrow.activities[i];
                var selectedMaterial = item.selectedMaterial;

                if (selectedMaterial) {
                    item.materialId = selectedMaterial.id;
                    item.unit = selectedMaterial.unit;
                    item.brand = selectedMaterial.brand;
                    item.size = selectedMaterial.size;
                    item.remainingQuantity = selectedMaterial.remainingQuantity;
                }
            }
        };

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
            loadAll();
            clearForm();
        };

        var onSaveComplete = function () {
            utils.hideLoading();
            vm.saveEnabled = true;
        };

        var processItemList = function (response) {
			utils.populateDropdownlist(response, vm.itemList, "", "");
			utils.populateDropdownlist(response, vm.borrowerList, "name", "");
        };

        var loadAll = function () {
            utils.showLoading();

            var requests = {
				item: inventoryService.getItemLookup(),
				borrower: borrowerService.getBorrowerLookup()
            };

            $q.all(requests)
                .then((responses) => {
					processItemList(responses.item);
					processItemList(responses.borrower);
                }, utils.onError)
                .finally(utils.hideLoading);
        };

        $(function () {
            loadAll();
            clearForm();
        });

        return vm;
    };

    module.controller("borrowItemController", ["$q", "$scope", "activityService", "inventoryService", "borrowerService", "utils", borrowItemController]);

})(angular.module("srisys-app"));