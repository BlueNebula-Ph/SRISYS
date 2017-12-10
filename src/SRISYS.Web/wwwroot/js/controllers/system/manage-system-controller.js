(function (module) {
    var manageSystemController = function () {
        var vm = this;

        vm.sidebarItems = [
            { text: "Borrowers", isHeader: true, isItem: false, icon: "fa-users" },
            { text: "Add New Borrower", isHeader: false, isItem: true, icon: "fa-plus", link: ".add-borrower({ id: 0 })" },
            { text: "Search Borrowers", isHeader: false, isItem: true, icon: "fa-search", link: ".list-borrowers" },
            { text: "Categories", isHeader: true, isItem: false, icon: "fa-file-o" },
            { text: "Add New Category", isHeader: false, isItem: true, icon: "fa-plus", link: ".add-category({ id: 0 })" },
            { text: "Search Categories", isHeader: false, isItem: true, icon: "fa-search", link: ".list-categories" },
            { text: "Suppliers", isHeader: true, isItem: false, icon: "fa-truck" },
            { text: "Add New Supplier", isHeader: false, isItem: true, icon: "fa-plus", link: ".add-supplier({ id: 0 })" },
            { text: "Search Suppliers", isHeader: false, isItem: true, icon: "fa-search", link: ".list-suppliers" },
            { text: "Users", isHeader: true, isItem: false, icon: "fa-user-circle" },
            { text: "Add New User", isHeader: false, isItem: true, icon: "fa-plus", link: ".add-user({ id: 0 })" },
            { text: "Search Users", isHeader: false, isItem: true, icon: "fa-search", link: ".list-users" }
        ];

        return vm;
    };

    module.controller("manageSystemController", [manageSystemController]);

})(angular.module("srisys-app"));