(function (module) {
    var autocompleteController = ["$scope", "$timeout", "lookupService", function ($scope, $timeout, lookupService) {
        $scope.search = "";
        $scope.items = [];
        $scope.selectedItem = {};
        $scope.selectedIndex = -1;
        $scope.isFocused = $scope.isFocused || false;
        $scope.showLoading = false;

        $scope.$watch("selectedValue", function (newValue, oldValue) {
            if (newValue == undefined || newValue == 0) {
                clearAutocomplete();
            } else {
                // If there is no item object, fetch it from the DB
                if ($scope.selectedItem == undefined) {
                    lookupService.fetchItemById($scope.route, newValue)
                        .then(function (response) {
                            var item = response.data;
                            $scope.itemSelected(item);
                        }, function (error) {
                            toastr.error("Unable to fetch autocomplete item.", "Error")
                        });
                }
            }
        });

        $scope.itemSelected = function (item) {
            if (item != undefined) {
                $scope.selectedItem = item;
                $scope.selectedValue = item.Id;
                $scope.search = item.ItemName;
            } else {
                clearAutocomplete();
            }
            $scope.items = [];
        };

        $scope.handleKeydown = function (event) {
            // On Tab, if list is only 1 item, select that item
            // If user selected item using up or down, select that item
            // If user did not select anything, clear the control and resume correct tabbing
            switch (event.keyCode) {
                case 13:
                    event.preventDefault();
                    break;
                case 9:
                    if ($scope.items.length == 1) {
                        var selectedIndex = 0;
                        var selectedItem = $scope.items[selectedIndex];
                        $scope.itemSelected(selectedItem);
                    } else if ($scope.items.length > 0 && $scope.selectedIndex != -1) {
                        var selectedItem = $scope.items[$scope.selectedIndex];
                        $scope.itemSelected(selectedItem);
                    }
                    break;
            }
        };

        $scope.handleKeyup = function (event) {
            switch (event.keyCode) {
                case 13: // Enter
                    var selectedItem = $scope.items[$scope.selectedIndex];
                    $scope.itemSelected(selectedItem);

                    if ($scope.onEnter) {
                        $timeout(function () {
                            $scope.onEnter();
                        }, 50);
                    }
                    break;
                case 38: // Up
                    if ($scope.selectedIndex != 0) {
                        $scope.selectedIndex -= 1;
                    }
                    break;
                case 40: // Down
                    if ($scope.selectedIndex != $scope.items.length - 1) {
                        $scope.selectedIndex += 1;
                    }
                    break;
                default:
                    if ($scope.route == undefined || $scope.route == "") {
                        return;
                    }

                    if ($scope.search == "") {
                        clearAutocomplete();
                        return;
                    }

                    $scope.showLoading = true;
                    lookupService.fetchItems($scope.route, $scope.search)
                        .then(function (response) {
                            var data = response.data;
                            $scope.items = data;
                            $scope.selectedIndex = -1;
                            $scope.showLoading = false;
                        }, function (error) {
                            toastr.error("Unable to fetch autocomplete items.", "Error");
                            $scope.showLoading = false;
                        });
                    break;
            }
        };

        var clearAutocomplete = function () {
            $scope.items = [];
            $scope.search = "";
            $scope.selectedIndex = -1;
            $scope.selectedItem = {};
            $scope.selectedValue = 0;
        };
    }];

    var autocomplete = function () {
        return {
            restrict: "E",
            templateUrl: "/views/common/autocomplete.html",
            scope: {
                selectedItem: "=",
                selectedValue: "=",
                route: "@",
                isFocused: "=?",
                onEnter: "&?"
            },
            controller: autocompleteController
        };
    };

    module.directive("autocomplete", autocomplete);

})(angular.module("srisys-app"));