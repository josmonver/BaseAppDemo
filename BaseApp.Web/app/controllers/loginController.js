(function () {
    'use strict';

    var controllerId = 'LoginController';

    angular.module('app').controller(controllerId,
        ['$rootScope', '$scope', '$location', '$window', 'common', 'config', '$http',  '$state', 'Base64', 'AuthService', LoginController]);

    function LoginController($rootScope, $scope, $location, $window, common, config, $http, $state, Base64, AuthService) {
        var vm = this;
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);
        var keyCodes = config.keyCodes;

        // Bindable properties and functions are placed on vm.
        $scope.userName = '';
        $scope.password = '';

        $scope.login = function () {
            AuthService.login({
                userName: $scope.userName,
                password: $scope.password,
            }).success(function (data, status, headers, config) {
                AuthService.changeUser(data);
                AuthService.redirectToAttemptedState();
                //$state.go('anon.home'); // TODO redirigir a página visitada
            }).error(function (data, status, headers, config) {
                AuthService.changeUser();
            });
        };

        activate();

        function activate() {
            common.activateController([], controllerId)
                .then(function () {
                    //log(controllerId + ' activated');
                    //log($state.current.data); // TODO: Way to access data from state. In this case, from abstract parent state 'anon'
                });
        }
    }

})();
