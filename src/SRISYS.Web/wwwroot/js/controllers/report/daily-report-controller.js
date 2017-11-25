(function (module) {
    var dailyReportController = function ($state, reportService, utils) {
        var vm = this;

        // Main properties
        vm.activities = [];
        vm.type = $state.current.data.type;
        vm.filters = {
            selectedDate: new Date(),
            materialTypeId: vm.type == "Tools" ? 1 : 2
        };

        // Helpers
        vm.defaultFocus = true;

        // Public methods
        vm.fetchReport = function () {
            utils.showLoading();

            vm.filters.reportDate = vm.filters.selectedDate.toLocaleDateString();
            console.log(vm.filters.reportDate);

            reportService.getDailyActivities(vm.filters)
                .then(processResponse, utils.onError)
                .finally(utils.hideLoading);
        };

        // Private methods
        var processResponse = function (response) {
            angular.copy(response.data, vm.activities);
        };

        $(function () {
            vm.fetchReport();
        });

        return vm;
    };

    module.controller("dailyReportController", ["$state", "reportService", "utils", dailyReportController]);

})(angular.module("srisys-app"));