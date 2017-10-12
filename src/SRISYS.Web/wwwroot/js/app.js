(function () {
    'use strict';

    // Set the environment variables
    var env = {};

    if (window) {
        Object.assign(env, window._environment);
    };

    angular.module("srisys-app", ["ui.router"])
        .constant("env", env)
        .config(["$stateProvider", "$urlRouterProvider", function ($stateProvider, $urlRouterProvider) {

            $urlRouterProvider.otherwise("/home");

            $stateProvider
                .state("home", {
                    url: "/home",
                    templateUrl: "/views/home/index.html"
                })

                .state("system", {
                    url: "/system",
                    templateUrl: "/views/common/index.html",
                    controller: "manageSystemController",
                    controllerAs: "ctrl"
                }).state("system.list-suppliers", {
                    url: "/list/suppliers",
                    templateUrl: "/views/system/supplier-list.html",
                    controller: "viewSuppliersController",
                    controllerAs: "ctrl"
                }).state("system.add-supplier", {
                    url: "/add/supplier/{id}",
                    templateUrl: "/views/system/add-supplier.html",
                    controller: "addSupplierController",
                    controllerAs: "ctrl"
                }).state("system.list-categories", {
                    url: "/list/categories",
                    templateUrl: "/views/system/category-list.html",
                    controller: "viewCategoriesController",
                    controllerAs: "ctrl"
                }).state("system.add-category", {
                    url: "/add/category/{id}",
                    templateUrl: "/views/system/add-category.html",
                    controller: "addCategoryController",
                    controllerAs: "ctrl"
                }).state("system.list-users", {
                    url: "/list/users",
                    template: "<div>List users</div>"
                }).state("system.add-user", {
                    url: "/add/user/{id}",
                    template: "<div>ADD user</div>"
                })

                .state("inventory", {
                    url: "/inventory",
                    templateUrl: "/views/common/index.html",
                    controller: "manageInventoryController",
                    controllerAs: "ctrl"
                }).state("inventory.list", {
                    url: "/list",
                    templateUrl: "/views/inventory/item-list.html",
                    controller: "viewItemsController",
                    controllerAs: "ctrl"
                }).state("inventory.add", {
                    url: "/add/{id}",
                    templateUrl: "/views/inventory/add-item.html",
                    controller: "addItemController",
                    controllerAs: "ctrl"
                })

                .state("activity", {
                    url: "/activities",
                    templateUrl: "/views/common/index.html",
                    controller: "manageActivityController",
                    controllerAs: "ctrl"
                }).state("activity.log", {
                    url: "/log",
                    template: "<div>Activities</div>"
                }).state("activity.borrow", {
                    url: "/borrow",
                    template: "<div>Borrow Item</div>"
                }).state("activity.return", {
                    url: "/return",
                    template: "<div>Return Items</div>"
                })

                .state("reports", {
                    url: "/reports",
                    templateUrl: "/views/common/index.html",
                    controllerAs: "ctrl"
                });
        }])
        .run(["$rootScope", "$state", "$stateParams",
            function ($rootScope, $state, $stateParams) {
                $rootScope.$state = $state;
                $rootScope.$stateParams = $stateParams;
            }]);;
})();