(function (module) {
    var stocksReportController = function ($state, $window, reportService, utils) {
        var vm = this;

        // Main properties
        vm.items = [];

        vm.date = new Date().toLocaleDateString();

        vm.print = () => {
            $window.print();
        };

        // Private methods
        var loadStocksReport = function () {
            utils.showLoading();

            reportService.getStocks()
                .then(processResponse, utils.hideLoading)
                .finally(utils.hideLoading);
        };

        var loadCountSheet = function () {
            utils.showLoading();

            reportService.getCountSheet()
                .then(processResponse, utils.hideLoading)
                .finally(utils.hideLoading);
        };

        var processResponse = function (response) {
            angular.copy(response.data, vm.items);
        };

        $(function () {
            var reportType = $state.current.data.reportType;
            if (reportType == "Countsheet") {
                loadCountSheet();
            } else if (reportType == "Stocks") {
                loadStocksReport();
            }
        });

        return vm;
    };

    module.controller("stocksReportController", ["$state", "$window", "reportService", "utils", stocksReportController]);

})(angular.module("srisys-app"));