(function (module) {
    var stocksReportController = function (reportService, utils) {
        var vm = this;

        // Main properties
        vm.items = [];
        vm.date = new Date().toLocaleDateString();

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

    module.controller("stocksReportController", ["reportService", "utils", stocksReportController]);

})(angular.module("srisys-app"));