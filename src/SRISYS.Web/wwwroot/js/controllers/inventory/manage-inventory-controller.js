(function (module) {
    var manageInventoryController = function ($state) {
        var vm = this;

        vm.sidebarItems = [
            { text: "Inventory", isHeader: true, isItem: false, icon: "fa-database" },
            { text: "Add New Item", isHeader: false, isItem: true, icon: "fa-plus", link: ".add({ id: 0 })" },
            { text: "Search Items", isHeader: false, isItem: true, icon: "fa-search", link: ".list" },
            { text: "Inventory Adjustment", isHeader: true, isItem: false, icon: "fa-sliders" },
            { text: "Purchase Items", isHeader: false, isItem: true, icon: "fa-credit-card", link: ".purchase" },
            { text: "Adjust Quantity", isHeader: false, isItem: true, icon: "fa-arrows-h", link: ".adjust" }
        ];

        $(function () {
            $state.go(".list");
        });

        return vm;
    };

    module.controller("manageInventoryController", ["$state", manageInventoryController]);

})(angular.module("srisys-app"));