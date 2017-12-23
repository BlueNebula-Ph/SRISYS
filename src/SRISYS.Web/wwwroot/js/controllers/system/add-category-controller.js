(function (module) {
    var addCategoryController = function ($stateParams, categoryService, utils) {
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
            utils.showLoading();
            vm.saveEnabled = false;

            categoryService.saveCategory($stateParams.id, vm.category)
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
            var subcategory = vm.category.subcategories[$index];
            if (subcategory && subcategory.id != 0) {
                if (!confirm("This subcategory has already been saved. Are you sure?")) {
                    return;
                }

                categoryService
                    .deleteSubcategory($stateParams.id, subcategory.id)
                    .then(() => {
                        vm.category.subcategories.splice($index, 1);
                        loadCategory();
                    }, utils.onError);
            } else {
                vm.category.subcategories.splice($index, 1);
            }
        };

        // Private methods
        var clearForm = function () {
            angular.copy(defaultCategory, vm.category);
            vm.addCategoryForm.$setPristine();
            vm.defaultFocus = true;
        };

        var saveSuccessful = function (respose) {
            utils.showSuccessMessage("Category saved successfully.");

            if ($stateParams.id == 0) {
                clearForm();
            } else {
                loadCategory();
            }
        };

        var onSaveComplete = function () {
            utils.hideLoading();
            vm.saveEnabled = true;
        };

        // Load
        var processCategory = function (response) {
            angular.copy(response.data, defaultCategory);
            clearForm();
        };

        var loadCategory = function () {
            if ($stateParams.id != 0) {
                utils.showLoading();

                categoryService.getCategoryById($stateParams.id)
                    .then(processCategory, utils.onError)
                    .finally(utils.hideLoading);
            }
        };

        $(function () {
            loadCategory();
        });

        return vm;
    };

    module.controller("addCategoryController", ["$stateParams", "categoryService", "utils", addCategoryController]);

})(angular.module("srisys-app"));