(function (module) {
    var noValueFilter = function () {
        return function (input) {
            if (!input || input == '') {
                return '-';
            }
            return input;
        };
    };

    module.filter('noValue', [noValueFilter]);

})(angular.module("srisys-app"));