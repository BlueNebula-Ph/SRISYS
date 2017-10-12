(function (module) {
    var referenceService = function ($http, env) {
        var urlBase = env.baseUrl + "/api/reference";
        var dataFactory = {};

        dataFactory.getReferenceLookup = function (referenceTypeId) {
            var url = urlBase + "/" + referenceTypeId + "/lookup";
            return $http.get(url);
        };

        dataFactory.searchReferences = function (referenceTypeId, search) {
            var url = urlBase + "/" + referenceTypeId + "/search";
            return $http.post(url, search);
        };

        dataFactory.getReferenceById = function (referenceTypeId, id) {
            var url = urlBase + "/" + referenceTypeId + "/" + id;
            return $http.get(url);
        };

        dataFactory.saveReference = function (id, reference) {
            if (id === 0) {
                return $http.post(urlBase, reference);
            } else {
                var url = urlBase + "/" + id;
                return $http.put(url, reference);
            }
        };

        dataFactory.deleteReference = function (id) {
            var url = urlBase + "/" + id;
            return $http.delete(url);
        };

        return dataFactory;
    };

    module.factory("referenceService", ["$http", "env", referenceService]);

})(angular.module("srisys-app"));