(function (module) {
    var viewBorrowersController = function ($q, borrowerService, utils) {
        var vm = this;

        vm.focus = true;
        vm.currentPage = 1;
        vm.filters = {
            sortBy: "Name",
            sortDirection: "asc",
            searchTerm: "",
            pageIndex: vm.currentPage
        };
        vm.summaryResult = {
            items: []
        };

        // Headers
        vm.headers = [
            { text: "Name", value: "Name" },
            { text: "", value: "" }
        ];

        // Methods
        vm.fetchBorrowers = function () {
            utils.showLoading();

            borrowerService.searchBorrowers(vm.filters)
                .then(processBorrowerList, utils.onError)
                .finally(utils.hideLoading);
        };

        vm.clearFilter = function () {
            vm.filters.searchTerm = "";

            vm.focus = true;
        };

        vm.changePage = function () {
            vm.fetchBorrowers();
        };

        var processBorrowerList = function (response) {
            angular.copy(response.data, vm.summaryResult);
        };

        var processResponses = function (responses) {
            processBorrowerList(responses.borrower);
        };

        var loadAll = function () {
            utils.showLoading();

            var requests = {
                borrower: borrowerService.searchBorrowers(vm.filters)
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

    module.controller("viewBorrowersController", ["$q", "borrowerService", "utils", viewBorrowersController]);

})(angular.module("srisys-app"));