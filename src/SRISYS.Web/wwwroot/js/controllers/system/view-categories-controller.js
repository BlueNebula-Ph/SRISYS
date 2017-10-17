(function (module) {
    var viewCategoriesController = function ($q, referenceService, loadingService, utils) {
        var vm = this;
        var refTypeId = 3;

        vm.focus = true;
        vm.currentPage = 1;
        vm.filters = {
            sortBy: "ParentReference.Code",
            sortDirection: "asc",
            searchTerm: "",
            pageIndex: vm.currentPage
        };
        vm.summaryResult = {
            items: []
        };

        // Headers
        vm.headers = [
            { text: "Category", value: "ParentReference.Code" },
            { text: "Subcategory", value: "Code" },
            { text: "", value: "" }
        ];

        // Methods
        vm.fetchCategories = function () {
            loadingService.showLoading();

            referenceService.searchReferences(refTypeId, vm.filters)
                .then(processCategoryList, utils.onError)
                .finally(hideLoading);
        };

        vm.clearFilter = function () {
            vm.filters.searchTerm = "";

            vm.focus = true;
        };

        var processCategoryList = function (response) {
            angular.copy(response.data, vm.summaryResult);
        };

        var hideLoading = function () {
            loadingService.hideLoading();
        };

        var loadAll = function () {
            loadingService.showLoading();

            var requests = {
                category: referenceService.searchReferences(refTypeId, vm.filters)
            };

            $q.all(requests)
                .then((responses) => {
                    processCategoryList(responses.category);
                }, utils.onError)
                .finally(hideLoading);
        };

        $(function () {
            loadAll();
        });

        return vm;
    };

    module.controller("viewCategoriesController", ["$q", "referenceService", "loadingService", "utils", viewCategoriesController]);

})(angular.module("srisys-app"));