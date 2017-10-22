(function (module) {
    var itemDetailsController = function ($stateParams, inventoryService, utils) {
        var vm = this;
        vm.item = {};

        var processResponse = function (response) {
            angular.copy(response.data, vm.item);
        };

        var loadItemDetails = function () {
            if ($stateParams.id == 0) {
                return;
            }

            utils.showLoading();

            inventoryService.getItemById($stateParams.id)
                .then(processResponse, utils.onError)
                .finally(utils.hideLoading);
        };

        $(function () {
            loadItemDetails();
        });

        return vm;
    };

    module.controller("itemDetailsController", ["$stateParams", "inventoryService", "utils", itemDetailsController]);

})(angular.module("srisys-app"));