(function (module) {
    var addItemController = function ($scope, $q, $stateParams, inventoryService, supplierService, categoryService, utils) {
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
                if (!newVal) {
                    return;
                }

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
                var tempList = [];

                if (newVal && newVal != 0) {
                    var subcategories = vm.categoryList.find((cat) => { return cat.id == newVal; }).subcategories;
                    angular.copy(subcategories, tempList);
                }

                angular.copy(tempList, vm.subcategoryList);

            }, true);

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
            if (vm.addItemForm) {
                vm.addItemForm.$setPristine();
            }
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
            utils.populateDropdownlist(responses.category, vm.categoryList, "", "");
            utils.populateDropdownlist(responses.supplier, vm.supplierList, "", "");

            if (responses.item) {
                angular.copy(responses.item.data, defaultItem);
            }
            resetForm();
        };

        var loadAll = function () {
            utils.showLoading();

            var requests = {
                category: categoryService.getCategoryLookup(),
                supplier: supplierService.getSupplierLookup(),
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

    module.controller("addItemController", ["$scope", "$q", "$stateParams", "inventoryService", "supplierService", "categoryService", "utils", addItemController]);

})(angular.module("srisys-app"));