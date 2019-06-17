(function (module) {
    var viewActivitiesController = function ($q, activityService, inventoryService, borrowerService, utils) {
        var vm = this;

        // Data
        vm.summaryResult = {
            items: []
        };
        vm.itemList = [];
        vm.borrowerList = [];

        // Helper Properties
        vm.focus = true;
        vm.currentPage = 1;
        vm.filters = {
            sortBy: "Date",
            sortDirection: "desc",
            pageIndex: vm.currentPage,
            materialId: 0,
            borrowedById: 0,
            selectedDateFrom: undefined,
            selectedDateTo: undefined
        };

        // Public Methods
        vm.fetchActivities = function () {
            utils.showLoading();

            if (vm.filters.isComplete) {
                vm.filters.activityStatus = 2;
            } else {
                vm.filters.activityStatus = undefined;
            }

            if (vm.filters.selectedDateFrom) {
                vm.filters.dateFrom = vm.filters.selectedDateFrom.toDateString();
            }

            if (vm.filters.selectedDateTo) {
                vm.filters.dateTo = vm.filters.selectedDateTo.toDateString();
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

        // Paging
        vm.changePage = function () {
            vm.fetchActivities();
        };

        // Private Methods
        var processActivityList = function (response) {
            angular.copy(response.data, vm.summaryResult);
        };

        var processResponses = function (responses) {
            processActivityList(responses.activity);
            utils.populateDropdownlist(responses.item, vm.itemList, "name", "Filter by material..");
            utils.populateDropdownlist(responses.borrower, vm.borrowerList, "name", "Filter by borrower..");
        };

        var loadAll = function () {
            utils.showLoading();

            var requests = {
                activity: activityService.searchActivities(vm.filters),
                item: inventoryService.getItemLookup(),
                borrower: borrowerService.getBorrowerLookup()
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

    module.controller("viewActivitiesController", ["$q", "activityService", "inventoryService", "borrowerService", "utils", viewActivitiesController]);

})(angular.module("srisys-app"));