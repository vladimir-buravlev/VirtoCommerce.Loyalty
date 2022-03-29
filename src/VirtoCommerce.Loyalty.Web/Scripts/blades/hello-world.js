angular.module('Loyalty')
    .controller('Loyalty.helloWorldController', ['$scope', 'Loyalty.webApi', function ($scope, api) {
        var blade = $scope.blade;
        blade.title = 'Loyalty';

        blade.refresh = function () {
            api.get(function (data) {
                blade.title = 'Loyalty.blades.hello-world.title';
                blade.data = data.result;
                blade.isLoading = false;
            });
        };

        blade.refresh();
    }]);
