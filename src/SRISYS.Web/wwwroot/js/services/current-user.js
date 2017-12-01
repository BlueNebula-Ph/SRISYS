(function (module) {
    var currentUser = function (localStorage) {
        var USERKEY = "userkey";

        var setUserProfile = function (username, token) {
            userProfile.username = username;
            userProfile.token = token;

            // Setup other properties from token such as permissions
            var payload = JSON.parse(window.atob(token.split('.')[1]));
            userProfile.userId = payload.sid;

            localStorage.add(USERKEY, userProfile);
        };

        var initialize = function () {
            var user = {
                userId: 0,
                username: "",
                token: "",
                get loggedIn() {
                    return this.token;
                }
            };

            var localUser = localStorage.get(USERKEY);
            if (localUser) {
                user.userId = localUser.userId;
                user.username = localUser.username;
                user.token = localUser.token;
            }

            return user;
        };

        var userProfile = initialize();

        return {
            setUserProfile: setUserProfile,
            userProfile: userProfile
        };
    };

    module.factory("currentUser", ["localStorage", currentUser]);

})(angular.module("srisys-app"));