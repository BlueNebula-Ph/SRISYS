(function (module) {
    var purchaseItemController = function ($q, inventoryService, supplierService, utils) {
        var vm = this;
        var defaultPurchase = {
            materialId: 0,
            adjustmentType: "1",
            quantity: 1,
            isPurchase: true,
            purchaseDate: new Date(),
            price: 0,
            updatePrice: false,
            remarks: "",
            receiptNumber: ""
        };

        // Data
        vm.purchase = {};
        vm.itemList = [];
        vm.supplierList = [];

        // Helper properties
        vm.defaultFocus = true;
        vm.saveEnabled = true;

        // Public methods
        vm.save = function () {
            utils.showLoading();
            vm.saveEnabled = false;

            inventoryService.adjustQuantity(vm.purchase)
                .then(saveSuccessful, utils.onError)
                .finally(onSaveComplete);
        };

        vm.reset = function () {
            clearForm();
        };

        // Private methods
        var clearForm = function () {
            angular.copy(defaultPurchase, vm.purchase);
            vm.defaultFocus = true;
            resetSupplier();

            if (vm.addPurchaseForm) {
                vm.addPurchaseForm.$setPristine();
                vm.addPurchaseForm.$setUntouched();
            }
        };

        var saveSuccessful = function () {
            utils.showSuccessMessage("Purchase saved successfully.");
            clearForm();
        };

        var onSaveComplete = function () {
            utils.hideLoading();
            vm.saveEnabled = true;
        };

        var resetSupplier = function () {
            // Select 1st supplier as default.
            vm.purchase.supplier = vm.supplierList[0];
        };

        var processLists = function (responses) {
            utils.populateDropdownlist(responses.item, vm.itemList, "name", "-- Select Material --");
            utils.populateDropdownlist(responses.supplier, vm.supplierList, "", "");

            resetSupplier();
        };

        var loadAll = function () {
            utils.showLoading();

            var requests = {
                item: inventoryService.getItemLookup(),
                supplier: supplierService.getSupplierLookup()
            };

            $q.all(requests)
                .then(processLists, utils.onError)
                .finally(utils.hideLoading);
        };

        $(function () {
            loadAll();
            clearForm();
        });

        return vm;
    };

    module.controller("purchaseItemController", ["$q", "inventoryService", "supplierService", "utils", purchaseItemController]);

})(angular.module("srisys-app"));