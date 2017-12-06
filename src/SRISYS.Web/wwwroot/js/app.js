(function () {
    'use strict';

    // Set the environment variables
    var env = {};

    if (window) {
        Object.assign(env, window._environment);
    };

    angular.module("srisys-app", ["ui.router", "permission", "permission.ui"])
        .constant("env", env)
        .constant("keys", { userkey: "USERKEY", userpermissions: "USERPERMISSIONS" })
        .config(["$stateProvider", "$urlRouterProvider", function ($stateProvider, $urlRouterProvider) {

            $urlRouterProvider.otherwise("/home");

            $stateProvider
                .state("login", {
                    url: "/login",
                    templateUrl: "/views/common/login.html",
                    controller: "loginController",
                    controllerAs: "ctrl"
                })

                .state("home", {
                    url: "/home",
                    templateUrl: "/views/home/index.html",
                    controller: "homeController",
                    controllerAs: "ctrl",
                    data: {
                        permission: {
                            only: ["admin", "canWrite"],
                            redirectTo: "unauthorized"
                        }
                    }
                })

                .state("system", {
                    url: "/system",
                    templateUrl: "/views/common/index.html",
                    controller: "manageSystemController",
                    controllerAs: "ctrl",
                    data: {
                        permissions: {
                            only: "admin",
                            redirectTo: "unauthorized"
                        }
                    }
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
                    templateUrl: "/views/system/user-list.html",
                    controller: "viewUsersController",
                    controllerAs: "ctrl"
                }).state("system.add-user", {
                    url: "/add/user/{id}",
                    templateUrl: "/views/system/add-user.html",
                    controller: "addUserController",
                    controllerAs: "ctrl"
                }).state("system.list-borrowers", {
                    url: "/list/borrowers",
                    templateUrl: "/views/system/borrower-list.html",
                    controller: "viewBorrowersController",
                    controllerAs: "ctrl"
                }).state("system.add-borrower", {
                    url: "/add/borrower/{id}",
                    templateUrl: "/views/system/add-borrower.html",
                    controller: "addBorrowerController",
                    controllerAs: "ctrl"
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
                    controllerAs: "ctrl",
                    data: {
                        type: "Tools"
                    }
                }).state("activity.consume", {
                    url: "/consume",
                    templateUrl: "/views/activity/borrow-items.html",
                    controller: "borrowItemController",
                    controllerAs: "ctrl",
                    data: {
                        type: "Consumables"
                    }
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
                    url: "/daily/items",
                    templateUrl: "/views/report/daily-report.html",
                    controller: "dailyReportController",
                    controllerAs: "ctrl",
                    data: {
                        type: "Tools"
                    }
                }).state("reports.consumables", {
                    url: "/daily/consumables",
                    templateUrl: "/views/report/daily-report.html",
                    controller: "dailyReportController",
                    controllerAs: "ctrl",
                    data: {
                        type: "Consumables"
                    }
                }).state("reports.count-sheet", {
                    url: "/countsheet",
                    templateUrl: "/views/report/count-sheet-report.html",
                    controller: "stocksReportController",
                    controllerAs: "ctrl",
                }).state("reports.low-stocks", {
                    url: "/lowstocks",
                    templateUrl: "/views/report/low-stocks-report.html",
                    controller: "lowStocksReportController",
                    controllerAs: "ctrl"
                }).state("reports.stocks", {
                    url: "/stocks",
                    templateUrl: "/views/report/stocks-report.html",
                    controller: "stocksReportController",
                    controllerAs: "ctrl"
                })

                .state("unauthorized", {
                    url: "/unauthorized",
                    templateUrl: "/views/common/unauthorized.html"
                });
        }])
        .run(["$rootScope", "$state", "$stateParams", "$transitions", "loginRedirect", "currentUser",
            function ($rootScope, $state, $stateParams, $transitions, loginRedirect, currentUser) {
                $rootScope.$state = $state;
                $rootScope.$stateParams = $stateParams;

                $transitions.onBefore({}, function (transition) {
                    var newState = transition.to().name;

                    if (newState != "login" && !currentUser.userProfile.loggedIn) {
                        loginRedirect.setLastState(newState);
                        return transition.router.stateService.target("login");
                    }
                });
            }]);
})();