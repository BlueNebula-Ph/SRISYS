(function (module) {
    var addCategoryController = function (referenceService, loadingService, utils) {
        var vm = this;

        // Data
        var referenceTypeId = 2;
        var childReferenceTypeId = 3;
        var defaultCategory = {};
        vm.category = {
            referenceTypeId: referenceTypeId,
            children: []
        };

        // Helper properties
        vm.defaultFocus = true;
        vm.saveEnabled = true;

        // Public methods
        vm.save = function () {
            loadingService.showLoading();

            referenceService.saveReference(0, vm.category)
                .then(saveSuccessful, utils.onError)
                .finally(onSaveComplete);
        };

        vm.reset = function () {
            clearForm();
        };

        vm.addSubcategory = function () {
            var newSub = { code: "", referenceTypeId: childReferenceTypeId, focus: true };
            vm.category.children.push(newSub);
        };

        // Private methods
        var clearForm = function () {
            angular.copy(defaultCategory, vm.category);
            vm.addCategoryForm.$setPristine();
            vm.defaultFocus = true;
        };

        var saveSuccessful = function (respose) {
            utils.showSuccessMessage("Category saved successfully.");
            clearForm();
        };

        var onSaveComplete = function () {
            loadingService.hideLoading();
            vm.saveEnabled = true;
        };

        return vm;
    };

    module.controller("addCategoryController", ["referenceService", "loadingService", "utils", addCategoryController]);

})(angular.module("srisys-app"));