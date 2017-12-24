(function (module) {
    var borrowItemController = function ($q, $scope, $state, activityService, inventoryService, borrowerService, currentUser, utils) {
        var vm = this;
        var defaultBorrow = {
            type: 1,
            borrowedById: 0,
            releasedById: currentUser.userProfile.userId,
            releasedByUser: currentUser.userProfile.name,
            date: new Date(),
            activities: []
        };
        var newActivity = {
            quantity: 0,
            isFocused: true,
            materialId: 0,
        };
        var type = $state.current.data.type;

        // Data
        vm.borrow = {};
		vm.itemList = [];
		vm.borrowerList = [];

        // Helper properties
        vm.defaultFocus = true;
        vm.saveEnabled = true;
        vm.header = type === "Tools" ? "Borrow Tools." : "Use Consumables.";
        vm.typeId = type === "Tools" ? 1 : 2;

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
                vm.borrow.activities[i].borrowedById = vm.borrow.borrowedById;
                vm.borrow.activities[i].releasedById = vm.borrow.releasedById;
                vm.borrow.activities[i].date = vm.borrow.date.toLocaleDateString();
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

        vm.removeItemBorrowed = function ($index) {
            vm.borrow.activities.splice($index, 1);
        };

        // Private methods
        var assignValues = function () {
            for (var i = 0, l = vm.borrow.activities.length; i < l; i++) {
                var item = vm.borrow.activities[i];
                var selectedMaterial = item.selectedMaterial;

                if (selectedMaterial) {
                    console.log(selectedMaterial);
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

            if (vm.borrowForm) {
                vm.borrowForm.$setPristine();
                vm.borrowForm.$setUntouched();
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

        var loadAll = function () {
            utils.showLoading();

            var typeId = type === "Tools" ? 1 : 2;
            var requests = {
				item: inventoryService.getItemLookup(typeId),
				borrower: borrowerService.getBorrowerLookup()
            };

            $q.all(requests)
                .then((responses) => {
                    utils.populateDropdownlist(responses.item, vm.itemList, "", "");
                    utils.populateDropdownlist(responses.borrower, vm.borrowerList, "", "");
                }, utils.onError)
                .finally(utils.hideLoading);
        };

        $(function () {
            loadAll();
            clearForm();
        });

        return vm;
    };

    module.controller("borrowItemController", ["$q", "$scope", "$state", "activityService", "inventoryService", "borrowerService", "currentUser", "utils", borrowItemController]);

})(angular.module("srisys-app"));