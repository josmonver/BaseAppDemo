(function () {
    'use strict';

    var app = angular.module('app');

    app.directive('authenticated', ['AuthService', function (AuthService) {
        // Description:
        //  sets content visible if user is authenticated
        // Usage:
        //  <div authenticated="true|false"></div>
        //  true: visible if authenticated; false: visible if not authenticated
        return {
            link: link,
            restrict: 'A'
        };

        function link(scope, element, attrs) {
            var prevDisp = element.css('display');
            var mustShow = attrs.authenticated ? scope.$eval(attrs.authenticated) : true;
            scope.currentUser = AuthService.currentUser;
            scope.$watch('currentUser', function (currentUser) {
                if (!AuthService.isLoggedIn())
                    element.css('display', mustShow ? 'none' : prevDisp);
                else
                    element.css('display', mustShow ? prevDisp : 'none');
            }, true);
        }
    }]);

    app.directive('ccSpinner', ['$window', function ($window) {
        // Description:
        //  Creates a new Spinner and sets its options
        // Usage:
        //  <div data-cc-spinner="vm.spinnerOptions"></div>
        return {
            link: link,
            restrict: 'A'
        };

        function link(scope, element, attrs) {
            scope.spinner = null;
            scope.$watch(attrs.ccSpinner, function (options) {
                if (scope.spinner) {
                    scope.spinner.stop();
                }
                scope.spinner = new $window.Spinner(options);
                scope.spinner.spin(element[0]);
            }, true);
        }
    }]);

})();