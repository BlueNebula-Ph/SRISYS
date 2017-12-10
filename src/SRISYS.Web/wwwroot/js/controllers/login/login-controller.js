(function (module) {
    var loginController = function (authService, loginRedirect, currentUser) {
        var vm = this;
        vm.defaultFocus = true;

        vm.username = "";
        vm.password = "";
        vm.userProfile = currentUser;

        vm.login = function (form) {
            if (form.$valid) {
                authService.login(vm.username, vm.password)
                    .then(function (response) {
                        loginRedirect.redirectPostLogin();
                    }, function (errorResponse) {
                        console.log(errorResponse);
                        toastr.error(errorResponse.data.error);
                    }).finally(function () {
                        vm.password = vm.username = "";
                        form.$setUntouched();

                        vm.defaultFocus = true;
                    });
            }
        };

        return vm;
    };

    module.controller("loginController", ["authService", "loginRedirect", "currentUser", loginController]);

})(angular.module("srisys-app"));