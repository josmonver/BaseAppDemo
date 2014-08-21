(function () {
    'use strict';

    var serviceId = 'AuthService';

    angular.module('app').factory(serviceId, [
        '$rootScope', '$location', '$state',
        '$window', '$stateParams', 'Base64',
        'common', 'config', '$http',
        'redirectToStateAfterLogin', 'redirectToStateAfterLogout',
        AuthService]);

    function AuthService($rootScope, $location, $state, $window, $stateParams, Base64,
        common, config, $http, redirectToStateAfterLogin, redirectToStateAfterLogout) {

        var log = common.logger.getLogFn(serviceId);
        var currentUser = $.parseJSON($window.sessionStorage.getItem('user')) || createAnonymousUser();

        var service = {
            login: login,
            isLoggedIn: isLoggedIn,
            changeUser: changeUser,
            currentUser: currentUser,
            saveAttemptState: saveAttemptState,
            redirectToAttemptedState: redirectToAttemptedState,
            logout: logout
        };

        return service;

        function login(user) {
            return $http.post(config.api + '/authenticate', user);
        }

        function isLoggedIn(user) {
            if(user === undefined) {
                user = currentUser;
            }
            return user.role != 'anon';
        }

        function changeUser(data) {
            if (data) {
                var profile = decodeProfile(data.token);
                angular.extend(currentUser, profile);
                $window.sessionStorage.token = data.token;
                $window.sessionStorage.setItem('user', JSON.stringify(currentUser))
            } else {
                angular.extend(currentUser, createAnonymousUser());
                delete $window.sessionStorage.token;
                delete $window.sessionStorage.user;
            }
        }

        function saveAttemptState(state) {
            if(state.name != 'anon.login') {
                redirectToStateAfterLogin.stateName = state.name;
            }
            else
                redirectToStateAfterLogin.stateName = 'anon.home';
        }

        function redirectToAttemptedState() {
            $state.go(redirectToStateAfterLogin.stateName);
        }

        function logout(redirectTostateName, data) {
            changeUser();
            if (redirectTostateName) {
                $state.go(redirectTostateName);
            } else {
                $state.go(redirectToStateAfterLogout.stateName);
            }

            if (data) log(data.msg);
        }

        // Private
        function createAnonymousUser() {
            return {
                userName: '',
                role: 'anon'
            }
        }

        function decodeProfile(token) {
            var encodedProfile = token.split('.')[1];
            return JSON.parse(Base64.decode(encodedProfile));
        }
    }

})();
