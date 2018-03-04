(function (module) {
    var reportService = function ($http, env) {
        var urlBase = env.baseUrl + "/api/report";
        var dataFactory = {};

        dataFactory.getDailyActivities = function (filter) {
            var url = urlBase + "/daily-activity";
            return $http.post(url, filter);
        };

        dataFactory.getLowStocks = function () {
            var url = urlBase + "/low-stock";
            return $http.get(url);
        };

        dataFactory.getStocks = function () {
            var url = urlBase + "/stocks";
            return $http.get(url);
        };

        dataFactory.getCountSheet = function () {
            var url = urlBase + "/count-sheet";
            return $http.get(url);
        };

        return dataFactory;
    };

    module.factory("reportService", ["$http", "env", reportService]);

})(angular.module("srisys-app"));