angular.module('Loyalty')
    .factory('Loyalty.webApi', ['$resource', function ($resource) {
        return $resource('api/Loyalty');
}]);
