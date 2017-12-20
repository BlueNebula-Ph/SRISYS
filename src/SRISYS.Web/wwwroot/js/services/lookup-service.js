(function (module) {
    var lookupService = function (env, $http) {
        var dataFactory = {};
        var urlBase = env.baseUrl;

        dataFactory.fetchItems = function (route, search) {
            var url = urlBase + route + "?search=" + search + "&pageSize=20";
            return $http.get(url);
        };

        dataFactory.fetchItemById = function (route, id) {
            var url = urlBase + route + "/" + id;
            return $http.get(url);
        };

        return dataFactory;
    };

    module.factory("lookupService", ["env", "$http", lookupService]);

})(angular.module("srisys-app"));