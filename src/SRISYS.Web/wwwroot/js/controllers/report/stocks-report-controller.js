(function (module) {
    var stocksReportController = function ($window, reportService, utils) {
        var vm = this;

        // Main properties
        vm.items = [];
        vm.date = new Date().toLocaleDateString();

        vm.print = () => {
            $window.print();
        };

        // Private methods
        var loadReport = function () {
            utils.showLoading();

            reportService.getStocks()
                .then(processResponse, utils.hideLoading)
                .finally(utils.hideLoading);
        };

        var processResponse = function (response) {
            angular.copy(response.data, vm.items);
        };

        $(function () {
            loadReport();
        });

        return vm;
    };

    module.controller("stocksReportController", ["$window", "reportService", "utils", stocksReportController]);

})(angular.module("srisys-app"));