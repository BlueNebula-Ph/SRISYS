(function (module) {
    var navigation = function () {
        var navController = ["$state", "currentUser", "localStorage", "keys", function ($state, currentUser, localStorage, keys) {
            var vm = this;

            vm.loggedUser = currentUser.userProfile;

            vm.logout = function () {
                localStorage.remove(keys.userkey);
                currentUser.setUserProfile("", "");
                $state.go("login");
            };

            return vm;
        }];

        return {
            restrict: "E",
            controller: navController,
            controllerAs: "navCtrl",
            templateUrl: "/views/common/navigation.html?" + $.now()
        };
    };

    module.directive("navigation", [navigation]);

})(angular.module("srisys-app"));
