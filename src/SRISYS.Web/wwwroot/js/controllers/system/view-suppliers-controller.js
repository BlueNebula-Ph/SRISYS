(function (module) {
    var viewSuppliersController = function ($q, supplierService, loadingService) {
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
            loadingService.showLoading();

            supplierService.searchSuppliers(vm.filters)
                .then(processSupplierList, onFetchError)
                .finally(hideLoading);
        };

        vm.clearFilter = function () {
            vm.filters.searchTerm = "";

            vm.focus = true;
		};

		vm.delete = function (id) {
			supplierService.deleteSupplier(id)
				.then((response) => { vm.fetchSuppliers(); }, onFetchError)
				.finally(utils.hideLoading);
		};

        var processSupplierList = function (response) {
            angular.copy(response.data, vm.summaryResult);
        };

        var onFetchError = function (error) {
            toastr.error("There was an error processing your requests.", "error");
            console.log(error);
        };

        var hideLoading = function () {
            loadingService.hideLoading();
        };

        var loadAll = function () {
            loadingService.showLoading();

            var requests = {
                supplier: supplierService.searchSuppliers(vm.filters)
            };

            $q.all(requests)
                .then((responses) => {
                    processSupplierList(responses.supplier);
                }, onFetchError)
                .finally(hideLoading);
        };

        $(function () {
            loadAll();
        });

        return vm;
    };

    module.controller("viewSuppliersController", ["$q", "supplierService", "loadingService", viewSuppliersController]);

})(angular.module("srisys-app"));