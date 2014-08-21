(function () {
    'use strict';

    var controllerId = 'HomeController';

    angular.module('app').controller(controllerId,
        ['$rootScope', '$scope', '$location', '$stateParams', 'common', 'config', '$http', homeController]);

    function homeController($rootScope, $scope, $location, $stateParams, common, config, $http) {
        var vm = this;
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);

        activate();

        $scope.public = function () {
            $http.get(config.api + '/test').success(function() {
            }).error(function(a, b, c, d) {
            });
        }

        $scope.restricted = function () {
            $http.get(config.api + '/restricted/test').success(function () {
            }).error(function (a, b, c, d) {
            });
        }

        function activate() {
            common.activateController([], controllerId)
                .then(function () {
                    //log(controllerId + ' activated');
                    //console.log($scope.$parent.isAuthenticated); // This way it can be possible to access to parent scope
                });
        }
    }

})();
