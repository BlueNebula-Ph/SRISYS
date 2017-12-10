(function (module) {
    var categoryService = function ($http, env) {
        var urlBase = env.baseUrl + "/api/category";
        var dataFactory = {};

        dataFactory.getCategoryLookup = function () {
            var url = urlBase + "/lookup";
            return $http.get(url);
        };

        dataFactory.searchCategories = function (search) {
            var url = urlBase + "/search";
            return $http.post(url, search);
        };

        dataFactory.getCategoryById = function (id) {
            var url = urlBase + "/" + id;
            return $http.get(url);
        };

        dataFactory.saveCategory = function (id, category) {
            if (id == 0) {
                return $http.post(urlBase, category);
            } else {
                var url = urlBase + "/" + id;
                return $http.put(url, category);
            }
        };

        dataFactory.deleteCategory = function (id) {
            var url = urlBase + "/" + id;
            return $http.delete(url);
        };

        dataFactory.deleteSubcategory = function (id, subcategoryId) {
            var url = urlBase + "/" + id + "/subcategory/" + subcategoryId;
            return $http.delete(url);
        };

        return dataFactory;
    };

    module.factory("categoryService", ["$http", "env", categoryService]);

})(angular.module("srisys-app"));