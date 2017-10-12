(function (module) {
    var viewItemsController = function ($q, inventoryService, loadingService) {
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
            { text: "Type", value: "Type.Code" },
            { text: "Qty", value: "Quantity", class: "text-right" },
            { text: "Qty Unused", value: "RemainingQuantity", class: "text-right" },
            { text: "Brand", value: "Brand" },
            { text: "Model", value: "Model" },
            { text: "", value: "" }
        ];

        // Methods
        vm.fetchItems = function () {
            loadingService.showLoading();

            inventoryService.searchItems(vm.filters)
                .then(processItemList, onFetchError)
                .finally(hideLoading);
        };

        vm.clearFilter = function () {
            vm.filters.searchTerm = "";

            vm.focus = true;
        };

        var processItemList = function (response) {
            angular.copy(response.data, vm.summaryResult);
        };

        var onFetchError = function (error) {
            toastr.error("There was an error processing your requests.", "error");
            console.log(error);
        };

        var hideLoading = function () {
            loadingService.hideLoading();
        };

        var loadAll = function () {
            loadingService.showLoading();

            var requests = {
                item: inventoryService.searchItems(vm.filters)
            };

            $q.all(requests)
                .then((responses) => {
                    processItemList(responses.item);
                }, onFetchError)
                .finally(hideLoading);
        };

        $(function () {
            loadAll();
        });

        return vm;
    };

    module.controller("viewItemsController", ["$q", "inventoryService", "loadingService", viewItemsController]);

})(angular.module("srisys-app"));