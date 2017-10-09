﻿(function (module) {

    var focusMe = function ($timeout) {
        return {
            scope: { trigger: "=focusMe" },
            link: function (scope, element) {
                scope.$watch("trigger", function (value) {
                    if (value === true) {
                        $timeout(function () {
                            element[0].focus();
                            scope.trigger = false;
                        });
                    }
                });
            }
        };
    };

    module.directive("focusMe", ["$timeout", focusMe]);

})(angular.module("srisys-app"));
