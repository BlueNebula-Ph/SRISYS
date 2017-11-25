(function (module) {
    var homeController = function ($q, activityService, reportService, utils) {
        var vm = this;

        // Main properties
        vm.sidebarItems = [
            { text: "Quick Links", isHeader: true, isItem: false, icon: "fa-link" },
            { text: "Borrow Materials", isHeader: false, isItem: true, icon: "fa-mail-forward", link: "activity.borrow" },
            { text: "Return Materials", isHeader: false, isItem: true, icon: "fa-undo", link: "activity.return" },
            { text: "Search Inventory", isHeader: false, isItem: true, icon: "fa-search", link: "inventory.list" },
            { text: "Add New Material", isHeader: false, isItem: true, icon: "fa-plus", link: "inventory.add({ id: 0 })" }
        ];
        vm.recentActivities = [];
        vm.lowStock = [];

        // Private methods
        var processResponses = function (responses) {
            var recentActivitiesResponse = responses.recentActivities.data;
            angular.copy(recentActivitiesResponse.items, vm.recentActivities);

            var lowStockResponse = responses.lowStock.data;
            angular.copy(lowStockResponse, vm.lowStock);
        };

        var loadAll = function () {
            utils.showLoading();

            var filters = {
                sortBy: "Date",
                sortDirection: "desc",
                pageIndex: 1,
                pageSize: 50
            };

            var requests = {
                recentActivities: activityService.searchActivities(filters),
                lowStock: reportService.getLowStocks()
            };

            $q.all(requests)
                .then(processResponses, utils.onError)
                .finally(utils.hideLoading);
        };

        $(function () {
            loadAll();
        });

        return vm;
    };

    module.controller("homeController", ["$q", "activityService", "reportService", "utils", homeController]);

})(angular.module("srisys-app"));