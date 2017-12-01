(function (module) {
	var borrowerService = function ($http, env) {
		var urlBase = env.baseUrl + "/api/borrower";
		var dataFactory = {};

		dataFactory.getBorrowerLookup = function () {
			var url = urlBase + "/lookup";
			return $http.get(url);
		};

		dataFactory.searchBorrowers = function (search) {
			var url = urlBase + "/search";
			return $http.post(url, search);
		};

		dataFactory.getBorrowerById = function (id) {
			var url = urlBase + "/" + id;
			return $http.get(url);
		};

		dataFactory.saveBorrower = function (id, borrower) {
			if (id == 0) {
				return $http.post(urlBase, borrower);
			} else {
				var url = urlBase + "/" + id;
				return $http.put(url, borrower);
			}
		};

		dataFactory.deleteBorrower = function (id) {
			var url = urlBase + "/" + id;
			return $http.delete(url);
		};

		return dataFactory;
	};

	module.factory("borrowerService", ["$http", "env", borrowerService]);

})(angular.module("srisys-app"));