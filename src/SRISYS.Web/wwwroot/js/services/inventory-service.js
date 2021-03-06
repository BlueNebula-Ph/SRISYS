﻿(function (module) {
    var inventoryService = function ($http, env) {
        var urlBase = env.baseUrl + "/api/material";
        var dataFactory = {};

        dataFactory.getItemLookup = function (typeId) {
            var tid = typeId || 0;
            var url = urlBase + "/lookup/" + tid;
            return $http.get(url);
        };

        dataFactory.searchItems = function (search) {
            var url = urlBase + "/search";
            return $http.post(url, search);
        };

        dataFactory.getItemById = function (id) {
            var url = urlBase + "/" + id;
            return $http.get(url);
        };

        dataFactory.saveItem = function (id, item) {
            if (id == 0) {
                return $http.post(urlBase, item);
            } else {
                var url = urlBase + "/" + id;
                return $http.put(url, item);
            }
        };

        dataFactory.deleteItem = function (id) {
            var url = urlBase + "/" + id;
            return $http.delete(url);
        };

        dataFactory.adjustQuantity = function (adjustment) {
            var url = urlBase + "/adjust";
            return $http.post(url, adjustment);
        };

        return dataFactory;
    };

    module.factory("inventoryService", ["$http", "env", inventoryService]);

})(angular.module("srisys-app"));