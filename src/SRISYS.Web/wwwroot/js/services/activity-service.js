(function (module) {
    var activityService = function ($http, env) {
        var urlBase = env.baseUrl + "/api/activity";
        var dataFactory = {};

        dataFactory.searchActivities = function (search) {
            var url = urlBase + "/search";
            return $http.post(url, search);
        };

        dataFactory.saveActivity = function (item) {
            return $http.post(urlBase, item);
        };

        dataFactory.deleteActivity = function (id) {
            var url = urlBase + "/" + id;
            return $http.delete(url);
        };

        return dataFactory;
    };

    module.factory("activityService", ["$http", "env", activityService]);

})(angular.module("srisys-app"));