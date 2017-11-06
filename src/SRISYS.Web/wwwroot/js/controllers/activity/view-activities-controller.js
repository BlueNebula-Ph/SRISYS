(function (module) {
    var viewActivitiesController = function ($q, activityService, inventoryService, utils) {
        var vm = this;

        // Data
        vm.summaryResult = {
            items: []
        };
        vm.itemList = [];

        // Helper Properties
        vm.focus = true;
        vm.currentPage = 1;
        vm.filters = {
            sortBy: "Date",
            sortDirection: "desc",
            pageIndex: vm.currentPage,
            materialId: 0,
            borrowedBy: ""
        };

        // Headers
        vm.headers = [
            { text: "Material", value: "Material.Name" },
            { text: "Date Borrowed", value: "Date" },
            { text: "Qty Borrowed", value: "QuantityBorrowed" },
            { text: "Borrower", value: "BorrowedBy" },
            { text: "Date Returned", value: "LastDateReturned" },
            { text: "Qty Returned", value: "TotalQuantityReturned" },
            { text: "Returned By", value: "ReturnedBy" },
            { text: "", value: "" }
        ];

        // Public Methods
        vm.fetchActivities = function () {
            utils.showLoading();

            if (vm.filters.isComplete) {
                vm.filters.activityStatus = 2;
            } else {
                vm.filters.activityStatus = undefined;
            }

            activityService.searchActivities(vm.filters)
                .then(processActivityList, utils.onError)
                .finally(utils.hideLoading);
        };

        vm.clearFilter = function () {
            vm.filters.searchTerm = "";

            vm.focus = true;
        };

        vm.showHideDetails = function (item) {
            if (item.show) {
                item.show = false;
            } else {
                item.show = true;
            }
        };

        // Private Methods
        var processActivityList = function (response) {
            angular.copy(response.data, vm.summaryResult);
        };

        var processResponses = function (responses) {
            processActivityList(responses.activity);
            utils.populateDropdownlist(responses.item, vm.itemList, "name", "Filter by material..");
        };

        var loadAll = function () {
            utils.showLoading();

            var requests = {
                activity: activityService.searchActivities(vm.filters),
                item: inventoryService.getItemLookup()
            };

            $q.all(requests)
                .then(processResponses, utils.onError)
                .finally(utils.hideLoading);
        };

        $(function () {
            loadAll();
        });

        return vm;
    };

    module.controller("viewActivitiesController", ["$q", "activityService", "inventoryService", "utils", viewActivitiesController]);

})(angular.module("srisys-app"));