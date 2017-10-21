﻿(function (module) {
    var utils = function (loadingService) {
        var service = {};

        service.showSuccessMessage = function (message) {
            toastr.success(message, "Success");
        };

        service.onError = function (error) {
            toastr.error("There was an error processing your requests.", "Error");
            console.log(error);
        };

        service.showLoading = function () {
            loadingService.showLoading();
        };

        service.hideLoading = function () {
            loadingService.hideLoading();
        };

        service.populateDropdownlist = function (response, copyTo, prop, defaultText) {
            var data = response.data;
            angular.copy(data, copyTo);

            if (defaultText != "") {
                var defaultItem = { id: 0 };
                defaultItem[prop] = defaultText;

                copyTo.splice(0, 0, defaultItem);
            }
        };

        return service;
    };

    module.factory("utils", ["loadingService", utils]);

})(angular.module("srisys-app"));