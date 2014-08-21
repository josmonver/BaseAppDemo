(function () {
    'use strict';

    var controllerId = 'UserInfoController';

    angular.module('app').controller(controllerId,
        ['$scope', 'common', 'AuthService', UserInfoController]);

    function UserInfoController($scope, common, AuthService) {
        var vm = this;

        $scope.logout = function () {
            AuthService.logout();
        };

        activate();

        function activate() {
            common.activateController([], controllerId)
                .then(function () {
                    //log(controllerId + ' activated');
                });
        }
    }

})();
