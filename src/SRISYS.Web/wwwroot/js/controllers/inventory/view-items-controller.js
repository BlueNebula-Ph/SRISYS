(function (module) {
    var viewItemsController = function ($q, inventoryService, loadingService, utils) {
        var vm = this;
        vm.focus = true;
        vm.currentPage = 1;
        vm.filters = {
            sortBy: "Name",
            sortDirection: "asc",
            pageIndex: vm.currentPage,
            searchTerm: "",
            typeId: "0",
            categoryId: "0",
            subcategoryId: "0"
        };
        vm.summaryResult = {
            items: []
        };

        // Headers
        vm.headers = [
            { text: "Name", value: "Name" },
            { text: "(In) Qty", value: "Quantity", class: "text-center" },
            { text: "Type", value: "Type.Code" },
            { text: "Category", value: "Category.Code" },
            { text: "Subcategory", value: "SubCategory.Code" },
            { text: "Brand", value: "Brand" },
            { text: "Model", value: "Model" },
            { text: "Size", value: "Size" },
            { text: "", value: "" }
        ];

        // Methods
        vm.fetchItems = function () {
            loadingService.showLoading();

            inventoryService.searchItems(vm.filters)
                .then(processItemList, utils.onError)
                .finally(hideLoading);
        };

        vm.clearFilter = function () {
            vm.filters.searchTerm = "";

            vm.focus = true;
        };

        var processItemList = function (response) {
            angular.copy(response.data, vm.summaryResult);
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
                }, utils.onError)
                .finally(hideLoading);
        };

        $(function () {
            loadAll();
        });

        return vm;
    };

    module.controller("viewItemsController", ["$q", "inventoryService", "loadingService", "utils", viewItemsController]);

})(angular.module("srisys-app"));