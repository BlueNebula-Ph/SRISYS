(function (module) {
    var noValueFilter = function () {
        return function (input) {
            if (!input || input == "" || input.toString() == "0001-01-01T00:00:00") {
                return "-";
            }
            return input;
        };
    };

    module.filter("noValue", [noValueFilter]);

})(angular.module("srisys-app"));