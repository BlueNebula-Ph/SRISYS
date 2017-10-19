(function (module) {
    var viewActivitiesController = function ($q, activityService, loadingService, utils) {
        var vm = this;
        vm.focus = true;
        vm.currentPage = 1;
        vm.filters = {
            sortBy: "Date",
            sortDirection: "desc",
            pageIndex: vm.currentPage,
        };
        vm.summaryResult = {
            items: []
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

        // Methods
        vm.fetchActivities = function () {
            loadingService.showLoading();

            activityService.searchActivities(vm.filters)
                .then(processActivityList, utils.onError)
                .finally(hideLoading);
        };

        vm.clearFilter = function () {
            vm.filters.searchTerm = "";

            vm.focus = true;
        };

        var processActivityList = function (response) {
            angular.copy(response.data, vm.summaryResult);
        };

        var hideLoading = function () {
            loadingService.hideLoading();
        };

        var loadAll = function () {
            loadingService.showLoading();

            var requests = {
                activity: activityService.searchActivities(vm.filters)
            };

            $q.all(requests)
                .then((responses) => {
                    processActivityList(responses.activity);
                }, utils.onError)
                .finally(hideLoading);
        };

        $(function () {
            loadAll();
        });

        return vm;
    };

    module.controller("viewActivitiesController", ["$q", "activityService", "loadingService", "utils", viewActivitiesController]);

})(angular.module("srisys-app"));