(function (module) {
    var addItemController = function ($scope, $q, $stateParams, inventoryService, supplierService, referenceService, utils) {
        var vm = this;
        var defaultItem = {
            typeId: "1",
            price: 0
        };

        // Data
        vm.item = {};
        vm.categoryList = [];
        vm.subcategoryList = [];
        vm.supplierList = [];

        // Helper properties
        vm.defaultFocus = true;
        vm.showConsumableFields = false;
        vm.saveEnabled = true;
        vm.quantityEnabled = $stateParams.id == 0;

        // Watchers
        $scope.$watch(() => { return vm.item.typeId; },
            function (newVal, oldVal) {
                // Set type id to string to allow radio button to pickup
                vm.item.typeId = newVal.toString();

                if (newVal == "2") {
                    vm.showConsumableFields = true;
                } else {
                    vm.showConsumableFields = false;
                }

                vm.defaultFocus = true;
            });

        $scope.$watch(() => { return vm.item.categoryId; },
            function (newVal, oldVal) {
                vm.subcategoryList = [];
                if (newVal != oldVal && newVal != 0) {
                    utils.showLoading();

                    referenceService.getReferenceLookup(3, newVal)
                        .then((response) => {
                            utils.populateDropdownlist(response, vm.subcategoryList, "", "");
                        }, utils.onError)
                        .finally(utils.hideLoading);
                } else if (newVal == 0) {
                    vm.subcategoryList.push({ id: 0, code: "-- Select subcategory --" });
                }
            });

        // Public Methods
        vm.save = function () {
            utils.showLoading();
            vm.saveEnabled = false;

            inventoryService
                .saveItem($stateParams.id, vm.item)
                .then(saveSuccessful, utils.onError)
                .finally(onSaveComplete);
        };

        vm.reset = function () {
            resetForm();
        };

        // Private methods
        var resetForm = function () {
            angular.copy(defaultItem, vm.item);
            vm.defaultFocus = true;
        };

        var saveSuccessful = function (response) {
            utils.showSuccessMessage("Material saved successfully.");

            // If edit, set defaultItem to newly saved item
            if ($stateParams.id != 0) {
                angular.copy(vm.item, defaultItem);
            }

            resetForm();
        };

        var onSaveComplete = function () {
            utils.hideLoading();
            vm.saveEnabled = true;
        };

        var processResponses = function (responses) {
            utils.populateDropdownlist(responses.category, vm.categoryList, "code", "-- Select category --");
            utils.populateDropdownlist(responses.supplier, vm.supplierList, "name", "-- Select supplier --");

            if (responses.item) {
                angular.copy(responses.item.data, defaultItem);
            }
            resetForm();
        };

        var loadAll = function () {
            utils.showLoading();

            var requests = {
                category: referenceService.getReferenceLookup(2),
				supplier: supplierService.getSupplierLookup()
            };

            if ($stateParams.id != 0) {
                requests.item = inventoryService.getItemById($stateParams.id);
            }

            $q.all(requests)
                .then(processResponses, utils.onError)
                .finally(utils.hideLoading);
        };

        // Initialize
        $(function () {
            loadAll();
        });

        return vm;
    };

    module.controller("addItemController", ["$scope", "$q", "$stateParams", "inventoryService", "supplierService", "referenceService", "utils", addItemController]);

})(angular.module("srisys-app"));