(function (module) {
    var viewSuppliersController = function ($q, supplierService, utils) {
        var vm = this;
        vm.focus = true;
        vm.currentPage = 1;
        vm.filters = {
            sortBy: "Name",
            sortDirection: "asc",
            searchTerm: "",
            pageIndex: vm.currentPage
        };
        vm.summaryResult = {
            items: []
        };

        // Headers
        vm.headers = [
            { text: "Name", value: "Name" },
            { text: "Address", value: "Address" },
            { text: "Phone Number", value: "PhoneNumber" },
            { text: "Email", value: "Email" },
            { text: "Other Details", value: "OtherDetails" },
            { text: "", value: "" }
        ];

        // Methods
        vm.fetchSuppliers = function () {
            utils.showLoading();

            supplierService.searchSuppliers(vm.filters)
                .then(processSupplierList, utils.onError)
                .finally(utils.hideLoading);
        };

        vm.clearFilter = function () {
            vm.filters.searchTerm = "";

            vm.focus = true;
        };

        // Paging
        vm.changePage = function () {
            vm.fetchSuppliers();
        };

        var processSupplierList = function (response) {
            angular.copy(response.data, vm.summaryResult);
        };

        var loadAll = function () {
            utils.showLoading();

            var requests = {
                supplier: supplierService.searchSuppliers(vm.filters)
            };

            $q.all(requests)
                .then((responses) => {
                    processSupplierList(responses.supplier);
                }, utils.onError)
                .finally(utils.hideLoading);
        };

        $(function () {
            loadAll();
        });

        return vm;
    };

    module.controller("viewSuppliersController", ["$q", "supplierService", "utils", viewSuppliersController]);

})(angular.module("srisys-app"));