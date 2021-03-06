﻿(function (module) {
    var returnItemController = function ($q, activityService, inventoryService, borrowerService, currentUser, utils) {
        var vm = this;
        var defaultReturns = {
            type: 2,
            selectedReturnDate: new Date(),
            returnedById: 0,
            receivedByUser: currentUser.userProfile.name,
            receivedById: currentUser.userProfile.userId,
            activities: []
        };

        // Data
        vm.activities = [];
        vm.itemList = [];
        vm.borrowerList = [];
        vm.filters = {
            sortBy: "Date",
            sortDirection: "desc",
            pageIndex: 1,
            pageSize: 200,
            materialId: 0,
            borrowedById: 0,
            activityStatus: 1,
            selectedDateFrom: undefined,
            selectedDateTo: undefined
        };
        vm.returns = {};

        // Helper Properties
        vm.defaultFocus = true;
        vm.saveEnabled = true;

        // Public Methods
        vm.save = function () {
            utils.showLoading();
            vm.saveEnabled = false;

            // If qty returned != 0, save it.
            for (var i = 0, l = vm.activities.length; i < l; i++) {
                var obj = vm.activities[i];
                if (obj.quantityReturned && obj.quantityReturned != 0) {
                    var newReturn = {
                        id: obj.id,
                        date: vm.returns.selectedReturnDate.toDateString(),
                        materialId: obj.materialId,
                        quantity: obj.quantityReturned <= obj.balance ? obj.quantityReturned : obj.balance,
                        returnedById: vm.returns.returnedById,
                        receivedById: vm.returns.receivedById
                    };
                    vm.returns.activities.push(newReturn);
                }
            }

            if (vm.returns.activities.length == 0) {
                utils.showErrorMessage("Please input at least 1 return item.");
                onSaveComplete();
                return;
            }

            console.log(vm.returns);

            // Perform save.
            activityService.saveActivity(vm.returns)
                .then(saveSuccessful, utils.onError)
                .finally(onSaveComplete);
        };

        vm.reset = function () {
            clearForm();
        };

        vm.performFilter = function () {
            utils.showLoading();

            if (vm.filters.selectedDateFrom) {
                vm.filters.dateFrom = vm.filters.selectedDateFrom.toDateString();
            }

            if (vm.filters.selectedDateTo) {
                vm.filters.dateTo = vm.filters.selectedDateTo.toDateString();
            }

            activityService.getActivities(vm.filters)
                .then(processActivities, utils.onError)
                .finally(utils.hideLoading);
        };

        // Private Methods
        var clearForm = function () {
            angular.copy(defaultReturns, vm.returns);
            vm.defaultFocus = true;

            if (vm.returnForm) {
                vm.returnForm.$setPristine();
                vm.returnForm.$setUntouched();
            }
        };

        var saveSuccessful = function () {
            utils.showSuccessMessage("Materials returned successfully.");
            vm.performFilter();
            clearForm();
        };

        var onSaveComplete = function () {
            utils.hideLoading();
            vm.saveEnabled = true;
            vm.defaultFocus = true;
        };

        var processActivities = function (response) {
            vm.activities = response.data;

            vm.activities.map((itm) => { itm.quantityReturned = 0; });
        };

        var processResponses = function (responses) {
            processActivities(responses.activity);
            utils.populateDropdownlist(responses.items, vm.itemList, "name", "Filter by materials..");
            utils.populateDropdownlist(responses.borrower, vm.borrowerList, "", "");
        };

        var loadAll = function () {
            utils.showLoading();

            var requests = {
                activity: activityService.getActivities(vm.filters),
                items: inventoryService.getItemLookup(),
                borrower: borrowerService.getBorrowerLookup()
            };

            $q.all(requests)
                .then(processResponses, utils.onError)
                .finally(utils.hideLoading);
        };

        // Initialize
        $(function () {
            loadAll();
            clearForm();
        });

        return vm;
    };

    module.controller("returnItemController", ["$q", "activityService", "inventoryService", "borrowerService", "currentUser", "utils", returnItemController]);

})(angular.module("srisys-app"));