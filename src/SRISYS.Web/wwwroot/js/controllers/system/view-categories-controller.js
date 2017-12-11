(function (module) {
    var viewCategoriesController = function ($q, categoryService, utils) {
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
            { text: "Category", value: "Name" },
            { text: "Subcategories", value: "" },
            { text: "", value: "" }
        ];

        // Methods
        vm.fetchCategories = function () {
            utils.showLoading();

            categoryService.searchCategories(vm.filters)
                .then(processCategoryList, utils.onError)
                .finally(utils.hideLoading);
        };

        vm.clearFilter = function () {
            vm.filters.searchTerm = "";

            vm.focus = true;
        };

        vm.changePage = function () {
            vm.fetchCategories();
        };

		vm.delete = function (id) {
			categoryService.deleteCategory(id)
				.then((response) => { vm.fetchCategories(); }, utils.onError)
				.finally(utils.hideLoading);
		};

        var processCategoryList = function (response) {
            response.data.items.map((item) => {
                item.subs = item.subcategories.map((sc) => sc.name).join(', ');
            });

            angular.copy(response.data, vm.summaryResult);
        };

        var processResponses = function (responses) {
            processCategoryList(responses.category);
        };

        var loadAll = function () {
            utils.showLoading();

            var requests = {
                category: categoryService.searchCategories(vm.filters)
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

    module.controller("viewCategoriesController", ["$q", "categoryService", "utils", viewCategoriesController]);

})(angular.module("srisys-app"));