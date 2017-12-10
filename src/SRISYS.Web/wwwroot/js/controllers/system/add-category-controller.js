(function (module) {
    var addCategoryController = function (categoryService, loadingService, utils) {
        var vm = this;

        // Data
        var defaultCategory = {};
        vm.category = {
            name: "",
            subcategories: []
        };

        // Helper properties
        vm.defaultFocus = true;
        vm.saveEnabled = true;

        // Public methods
        vm.save = function () {
            loadingService.showLoading();

            categoryService.saveCategory(0, vm.category)
                .then(saveSuccessful, utils.onError)
                .finally(onSaveComplete);
        };

        vm.reset = function () {
            clearForm();
        };

        vm.addSubcategory = function () {
            var newSub = { id: 0, name: "", focus: true };
            vm.category.subcategories.push(newSub);
        };

        vm.removeSubcategory = function ($index) {
            vm.category.subcategories.splice($index, 1);
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

    module.controller("addCategoryController", ["categoryService", "loadingService", "utils", addCategoryController]);

})(angular.module("srisys-app"));