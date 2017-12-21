(function (module) {
    var lowStocksReportController = function ($window, reportService, utils) {
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

            reportService.getLowStocks()
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

    module.controller("lowStocksReportController", ["$window", "reportService", "utils", lowStocksReportController]);

})(angular.module("srisys-app"));