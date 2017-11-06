(function (module) {
    var manageReportsController = function () {
        var vm = this;

        vm.sidebarItems = [
            { text: "Daily Reports", isHeader: true, isItem: false, icon: "fa-line-chart" },
            { text: "Items Borrowed", isHeader: false, isItem: true, icon: "fa-area-chart", link: ".items" },
            { text: "Consumables", isHeader: false, isItem: true, icon: "fa-area-chart", link: ".consumables" },
            { text: "View Reports", isHeader: true, isItem: false, icon: "fa-bar-chart-o" },
            { text: "Count Sheet", isHeader: false, isItem: true, icon: "fa-pie-chart", link: ".count-sheet" },
            { text: "Low Stocks", isHeader: false, isItem: true, icon: "fa-pie-chart", link: ".low-stocks" },
            { text: "Stocks", isHeader: false, isItem: true, icon: "fa-pie-chart", link: ".stocks" }
        ];

        return vm;
    };

    module.controller("manageReportsController", [manageReportsController]);

})(angular.module("srisys-app"));