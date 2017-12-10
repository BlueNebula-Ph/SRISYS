(function (module) {
    var manageActivityController = function ($state) {
        var vm = this;

        vm.sidebarItems = [
            { text: "Activity Log", isHeader: true, isItem: false, icon: "fa-exchange" },
            { text: "View Log", isHeader: false, isItem: true, icon: "fa-file-text-o", link: ".log" },
            { text: "Borrow Tools", isHeader: false, isItem: true, icon: "fa-wrench", link: ".borrow" },
            { text: "Use Consumables", isHeader: false, isItem: true, icon: "fa-paperclip", link: ".consume" },
            { text: "Return Materials", isHeader: false, isItem: true, icon: "fa-undo", link: ".return" }
        ];

        $(function () {
            $state.go(".log");
        });

        return vm;
    };

    module.controller("manageActivityController", ["$state", manageActivityController]);

})(angular.module("srisys-app"));