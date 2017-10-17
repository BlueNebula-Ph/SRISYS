(function (module) {
    var utils = function () {
        var service = {};

        service.showSuccessMessage = function (message) {
            toastr.success(message, "Success");
        };

        service.onError = function (error) {
            toastr.error("There was an error processing your requests.", "Error");
            console.log(error);
        };

        return service;
    };

    module.factory("utils", [utils]);

})(angular.module("srisys-app"));