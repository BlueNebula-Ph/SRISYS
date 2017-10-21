(function (module) {
    var viewItemsController = function ($q, inventoryService, referenceService, utils) {
        var vm = this;
        vm.focus = true;
        vm.currentPage = 1;
        vm.filters = {
            sortBy: "Name",
            sortDirection: "asc",
            pageIndex: vm.currentPage,
            searchTerm: "",
            typeId: "0",
            categoryId: 0,
            subCategoryId: 0
        };
        vm.summaryResult = {
            items: []
        };
        vm.categoryList = [];
        vm.subcategoryList = [];

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
            utils.showLoading();

            inventoryService.searchItems(vm.filters)
                .then(processItemList, utils.onError)
                .finally(utils.hideLoading);
        };

        vm.clearFilter = function () {
            vm.filters.searchTerm = "";

            vm.focus = true;
        };

        vm.changePage = function () {
            vm.fetchItems();
        };

        var processItemList = function (response) {
            angular.copy(response.data, vm.summaryResult);
        };

        var processLists = function (responses) {
            // Item list
            processItemList(responses.item);

            // Dropdown lists
            utils.populateDropdownlist(responses.category, vm.categoryList, "code", "Filter by category..");
            utils.populateDropdownlist(responses.subcategory, vm.subcategoryList, "code", "Filter by subcategory..");
        };

        var loadAll = function () {
            utils.showLoading();

            var requests = {
                item: inventoryService.searchItems(vm.filters),
                category: referenceService.getReferenceLookup(2),
                subcategory: referenceService.getReferenceLookup(3)
            };

            $q.all(requests)
                .then(processLists, utils.onError)
                .finally(utils.hideLoading);
        };

        $(function () {
            loadAll();
        });

        return vm;
    };

    module.controller("viewItemsController", ["$q", "inventoryService", "referenceService", "utils", viewItemsController]);

})(angular.module("srisys-app"));