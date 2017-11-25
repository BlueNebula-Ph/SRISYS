(function (module) {
    var lowStocksReportController = function (reportService, utils) {
        var vm = this;

        // Main properties
        vm.items = [];
        vm.date = new Date().toLocaleDateString();

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

    module.controller("lowStocksReportController", ["reportService", "utils", lowStocksReportController]);

})(angular.module("srisys-app"));