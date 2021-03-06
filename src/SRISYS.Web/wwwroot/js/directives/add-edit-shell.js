﻿(function (module) {

    var addEditShell = function () {
        return {
            restrict: "E",
            transclude: {
                title: "shellTitle",
                form: "shellForm",
                controls: "shellControls"
            },
            templateUrl: "/views/common/add-edit-shell.html?" + $.now()
        };
    };

    module.directive("addEditShell", [addEditShell]);

})(angular.module("srisys-app"));