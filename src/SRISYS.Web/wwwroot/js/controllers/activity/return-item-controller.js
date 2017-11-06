(function (module) {
    var returnItemController = function ($q, activityService, inventoryService, utils) {
        var vm = this;
        var defaultReturns = {
            type: 2,
            date: new Date(),
            activities: []
        };

        // Data
        vm.activities = [];
        vm.itemList = [];
        vm.filters = {
            sortBy: "Date",
            sortDirection: "desc",
            pageIndex: 1,
            pageSize: 200,
            materialId: 0,
            borrowedBy: "",
            activityStatus: 1
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
                        date: vm.returns.date,
                        materialId: obj.materialId,
                        quantity: obj.quantityReturned <= obj.balance ? obj.quantityReturned : obj.balance,
                        returnedBy: vm.returns.returnedBy,
                        receivedBy: vm.returns.receivedBy
                    };
                    vm.returns.activities.push(newReturn);
                }
            }

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

            activityService.getActivities(vm.filters)
                .then(processActivities, utils.onError)
                .finally(utils.hideLoading);
        };

        // Private Methods
        var clearForm = function () {
            angular.copy(defaultReturns, vm.returns);
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
        };

        var loadAll = function () {
            utils.showLoading();

            var requests = {
                activity: activityService.getActivities(vm.filters),
                items: inventoryService.getItemLookup()
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

    module.controller("returnItemController", ["$q", "activityService", "inventoryService", "utils", returnItemController]);

})(angular.module("srisys-app"));