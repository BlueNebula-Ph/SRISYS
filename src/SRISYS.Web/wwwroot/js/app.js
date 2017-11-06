﻿(function () {
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
                }).state("inventory.purchase", {
                    url: "/purchase",
                    templateUrl: "/views/inventory/purchase-item.html",
                    controller: "purchaseItemController",
                    controllerAs: "ctrl"
                }).state("inventory.adjust", {
                    url: "/adjust",
                    templateUrl: "/views/inventory/adjust-item.html",
                    controller: "adjustItemController",
                    controllerAs: "ctrl"
                }).state("inventory.details", {
                    url: "/details/{id}",
                    templateUrl: "/views/inventory/item-details.html",
                    controller: "itemDetailsController",
                    controllerAs: "ctrl"
                })

                .state("activity", {
                    url: "/activities",
                    templateUrl: "/views/common/index.html",
                    controller: "manageActivityController",
                    controllerAs: "ctrl"
                }).state("activity.log", {
                    url: "/log",
                    templateUrl: "/views/activity/activity-list.html",
                    controller: "viewActivitiesController",
                    controllerAs: "ctrl"
                }).state("activity.borrow", {
                    url: "/borrow",
                    templateUrl: "/views/activity/borrow-items.html",
                    controller: "borrowItemController",
                    controllerAs: "ctrl"
                }).state("activity.return", {
                    url: "/return",
                    templateUrl: "/views/activity/return-items.html",
                    controller: "returnItemController",
                    controllerAs: "ctrl"
                })

                .state("reports", {
                    url: "/reports",
                    templateUrl: "/views/common/index.html",
                    controller: "manageReportsController",
                    controllerAs: "ctrl"
                }).state("reports.items", {
                    url: "/reports/daily/items",
                    template: "<div>Items Borrowed</div>"
                }).state("reports.consumables", {
                    url: "/reports/daily/consumables",
                    template: "<div>Consumables</div>"
                }).state("reports.count-sheet", {
                    url: "/reports/countsheet",
                    template: "<div>Count sheet</div>"
                }).state("reports.low-stocks", {
                    url: "/reports/lowstocks",
                    template: "<div>Low stocks</div>"
                }).state("reports.stocks", {
                    url: "/reports/stocks",
                    template: "<div>Stocks Report</div>"
                });
        }])
        .run(["$rootScope", "$state", "$stateParams",
            function ($rootScope, $state, $stateParams) {
                $rootScope.$state = $state;
                $rootScope.$stateParams = $stateParams;
            }]);;
})();