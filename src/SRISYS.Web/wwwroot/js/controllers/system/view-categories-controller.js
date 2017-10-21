(function (module) {
    var viewCategoriesController = function ($q, referenceService, utils) {
        var vm = this;
        var refTypeId = 3;

        vm.focus = true;
        vm.filters = {
            sortBy: "ParentReference.Code",
            sortDirection: "asc",
            searchTerm: "",
            pageIndex: 1
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
            utils.showLoading();

            referenceService.searchReferences(refTypeId, vm.filters)
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

        var processCategoryList = function (response) {
            angular.copy(response.data, vm.summaryResult);
        };

        var processResponses = function (responses) {
            processCategoryList(responses.category);
        };

        var loadAll = function () {
            utils.showLoading();

            var requests = {
                category: referenceService.searchReferences(refTypeId, vm.filters)
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

    module.controller("viewCategoriesController", ["$q", "referenceService", "utils", viewCategoriesController]);

})(angular.module("srisys-app"));