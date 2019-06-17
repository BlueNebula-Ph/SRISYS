(function (module) {
    var borrowItemController = function ($q, $scope, $state, activityService, inventoryService, borrowerService, currentUser, utils) {
        var vm = this;
        var newActivity = {
            quantity: 0,
            materialId: 0
        };
        var defaultBorrow = {
            type: 1,
            borrowedById: 0,
            releasedById: currentUser.userProfile.userId,
            releasedByUser: currentUser.userProfile.name,
            selectedDate: new Date(),
            activities: [newActivity]
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

        // On select from autocomplete
        vm.onEnter = function (item) {
            if (item.selectedMaterial) {
                var selectedMaterial = item.selectedMaterial;
                item.unit = selectedMaterial.unit;
                item.brand = selectedMaterial.brand;
                item.model = selectedMaterial.model;
                item.size = selectedMaterial.size;
                item.remainingQuantity = selectedMaterial.remainingQuantity;
            }
        };

        $scope.$watch(() => { return vm.borrow.activities; },
            function (newVal, oldVal) {
                assignValues();
            }, true);

        // Public methods
        vm.save = function () {
            utils.showLoading();
            vm.saveEnabled = false;

            // Cleanup activities before saving
            vm.borrow.activities = vm.borrow.activities.filter((val) => { return val.materialId && val.materialId !== 0; });

            for (var i = 0, l = vm.borrow.activities.length; i < l; i++) {
                vm.borrow.activities[i].borrowedById = vm.borrow.borrowedById;
                vm.borrow.activities[i].releasedById = vm.borrow.releasedById;
                vm.borrow.activities[i].date = vm.borrow.selectedDate.toDateString();
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
            detail.isFocused = true;
            vm.borrow.activities.push(detail);
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
                    item.materialId = selectedMaterial.id;
                    item.unit = selectedMaterial.unit;
                    item.brand = selectedMaterial.brand;
                    item.model = selectedMaterial.model;
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
            utils.showSuccessMessage("Materials borrowed/used successfully.");
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