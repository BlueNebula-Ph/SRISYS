(function (module) {
    var manageInventoryController = function ($state) {
        var vm = this;

        vm.sidebarItems = [
            { text: "Inventory", isHeader: true, isItem: false, icon: "fa-database" },
            { text: "Add New Item", isHeader: false, isItem: true, icon: "fa-plus", link: ".add({ id: 0 })" },
            { text: "Search Items", isHeader: false, isItem: true, icon: "fa-search", link: ".list" }
        ];

        $(function () {
            $state.go(".list");
        });

        return vm;
    };

    module.controller("manageInventoryController", ["$state", manageInventoryController]);

})(angular.module("srisys-app"));