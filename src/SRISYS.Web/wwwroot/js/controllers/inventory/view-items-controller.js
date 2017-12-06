(function (module) {
    var viewItemsController = function ($q, $scope, inventoryService, categoryService, utils) {
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
            { text: "Category", value: "Category.Name" },
            { text: "Subcategory", value: "SubCategory.Name" },
            { text: "Brand", value: "Brand" },
            { text: "Model", value: "Model" },
            { text: "Size", value: "Size" },
            { text: "", value: "" }
        ];

        // Watchers
        $scope.$watch(() => { return vm.filters.categoryId; },
            function (newVal, oldVal) {
                var tempList = [];

                if (newVal != 0) {
                    var subcategories = vm.categoryList.find((cat) => { return cat.id == newVal; }).subcategories;
                    angular.copy(subcategories, tempList);
                }

                tempList.splice(0, 0, { id: 0, name: "Filter by subcategory.." });
                angular.copy(tempList, vm.subcategoryList);
                vm.filters.subCategoryId = 0;

            }, true);

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
            utils.populateDropdownlist(responses.category, vm.categoryList, "name", "Filter by category..");
        };

        var loadAll = function () {
            utils.showLoading();

            var requests = {
                item: inventoryService.searchItems(vm.filters),
                category: categoryService.getCategoryLookup(),
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

    module.controller("viewItemsController", ["$q", "$scope", "inventoryService", "categoryService", "utils", viewItemsController]);

})(angular.module("srisys-app"));