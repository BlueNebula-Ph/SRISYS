(function (module) {

    var pager = function () {

        var pagingController = ["$scope", "$timeout", function ($scope, $timeout) {
            var vm = this;

            vm.goToPrev = function () {
                if ($scope.currentPage != 1) {
                    vm.goToPage($scope.currentPage - 1);
                }
            };

            vm.goToNext = function () {
                if ($scope.currentPage < $scope.totalPages) {
                    var page = parseInt($scope.currentPage);
                    vm.goToPage(page + 1);
                }
            };

            vm.goToPage = function (page) {
                $scope.currentPage = page;
                $timeout(function () {
                    $scope.onPageChange();
                }, 50);
            };

            vm.getTotalPages = function () {
                var result = new Array();

                // If total pages <= 10, return all pages
                if ($scope.totalPages <= 10) {
                    for (var i = 1, total = $scope.totalPages; i <= total; i++) {
                        result.push(i);
                    }
                } else { // If more than 10, return current page - 5 until 15th page
                    var i = 1;
                    if ($scope.currentPage > 6) {
                        i = $scope.currentPage - 5;
                    }
                    for (var j = 1; j <= 10; j++) {
                        if (i <= $scope.totalPages) {
                            result.push(i);
                            i++;
                        }
                    }
                }

                return result;
            };

            return vm;
        }];

        return {
            restrict: "E",
            templateUrl: "/views/common/pager.html?" + $.now(),
            controller: pagingController,
            controllerAs: "ctrl",
            scope: {
                currentPage: "=",
                totalPages: "=",
                onPageChange: "&",
                hasPreviousPage: "=",
                hasNextPage: "="
            }
        };
    };

    module.directive("pager", [pager]);

})(angular.module("srisys-app"));
