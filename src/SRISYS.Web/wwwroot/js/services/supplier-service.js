(function (module) {
    var supplierService = function ($http, env) {
        var urlBase = env.baseUrl + "/api/supplier";
        var dataFactory = {};

        dataFactory.getSupplierLookup = function () {
            var url = urlBase + "/lookup";
            return $http.get(url);
        };

        dataFactory.searchSuppliers = function (search) {
            var url = urlBase + "/search";
            return $http.post(url, search);
        };

        dataFactory.getSupplierById = function (id) {
            var url = urlBase + "/" + id;
            return $http.get(url);
        };

        dataFactory.saveSupplier = function (id, supplier) {
            if (id == 0) {
                return $http.post(urlBase, supplier);
            } else {
                var url = urlBase + "/" + id;
                return $http.put(url, supplier);
            }
        };

        dataFactory.deleteSupplier = function (id) {
            var url = urlBase + "/" + id;
            return $http.delete(url);
        };

        return dataFactory;
    };

    module.factory("supplierService", ["$http", "env", supplierService]);

})(angular.module("srisys-app"));