(function () {

    'use strict'

    var app = angular.module('app');

    app.config([
        '$stateProvider', '$urlRouterProvider', '$locationProvider'
        , function ($stateProvider, $urlRouterProvider, $locationProvider) {

            // When there is an empty route, redirect to index   
            $urlRouterProvider.when('', '/home');

            // Anonymous routes (states)
            $stateProvider
                .state('anon', {
                    abstract: true,
                    template: "<ui-view/>",
                    data: {
                        requireLogin: false
                    }
                })
                .state('anon.login', {
                    url: '/login',
                    templateUrl: '/views/login.html',
                    controller: 'LoginController'
                })
                .state('anon.home', {
                    url: "/home",
                    templateUrl: "/views/partials/home.html",
                    controller: 'HomeController'
                })
                .state('anon.home.list', {
                    url: "/list",
                    templateUrl: "/views/partials/home.list.html",
                    controller: function ($scope) {
                        $scope.items = ["A", "Set", "Of", "Things"];
                    }
                })
                .state('anon.404', {
                    url: '/404',
                    templateUrl: '/views/404.html'
                });

            // Private routes (states)
            $stateProvider
                .state('private', {
                    abstract: true,
                    template: "<ui-view/>",
                    data: {
                        requireLogin: true
                    }
                })
                .state('private.state2', {
                    url: "/state2",
                    templateUrl: "/views/partials/state2.html"
                })
                .state('private.state2.list', {
                    url: "/list",
                    templateUrl: "/views/partials/state2.list.html",
                    controller: function ($scope) {
                        $scope.things = ["A", "Set", "Of", "Things"];
                    }
                });

            // For any unmatched url, redirect to /404
            $urlRouterProvider.otherwise('/404');

            // Use the HTML5 History API
            //$locationProvider.html5Mode(true); // Require server side configuration to work

        }]);

})();