(function () {
    'use strict';

    var app = angular.module('app');

    // Api
    var apiConfig = {
        protocol: 'http://',
        host: 'localhost',
        port: ':27811',
        base: '/api',
    },
    api = apiConfig.protocol + apiConfig.host + apiConfig.port + apiConfig.base;

    // Configure Toastr
    toastr.options.timeOut = 4000;
    toastr.options.positionClass = 'toast-bottom-right';

    var spinnerOptions = {
        radius: 40,
        lines: 7,
        length: 0,
        width: 30,
        speed: 1.7,
        corners: 1.0,
        trail: 100,
        color: '#F58A00'
    };

    var keyCodes = {
        backspace: 8,
        tab: 9,
        enter: 13,
        esc: 27,
        space: 32,
        pageup: 33,
        pagedown: 34,
        end: 35,
        home: 36,
        left: 37,
        up: 38,
        right: 39,
        down: 40,
        insert: 45,
        del: 46
    };

    var events = {
        controllerActivateSuccess: 'controller.activateSuccess',
        spinnerToggle: 'spinner.toggle'
    };

    var config = {
        api: api,
        appErrorPrefix: '[CC Error] ', //Configure the exceptionHandler decorator
        docTitle: 'CC: ',
        spinnerOptions: spinnerOptions,
        events: events,
        keyCodes: keyCodes,
        version: '0.1.0'
    };

    // General configuration
    app.value('config', config);

    // State to redirect to after login
    app.value('redirectToStateAfterLogin', { stateName: 'anon.home' });
    // State to redirect to after logout (default)
    app.value('redirectToStateAfterLogout', { stateName: 'anon.home' });

    // Add Authorization header and build url to request. Manage response
    app.factory('authInterceptor', ['$q', '$window', '$injector', function ($q, $window, $injector) {
        return {
            request: function (config) {
                config.headers = config.headers || {};
                if ($window.sessionStorage.token) {
                    config.headers.Authorization = 'Bearer ' + $window.sessionStorage.token;
                }
                return config;
            },
            responseError: function (response) {
                if (response.status === 401 || response.status === 403) {
                    // The user is not authenticated or has not authorization
                    // AuthService can not be injected due to circular dependency
                    if ($injector.get('AuthService').isLoggedIn()) {
                        // token has expired
                        var data = { msg: 'The session has expired' };
                    }
                    $injector.get('AuthService').logout('anon.login', data);
                }
                return $q.reject(response);
            }
        }
    }]);
    app.config(['$httpProvider', function ($httpProvider) {
        $httpProvider.interceptors.push('authInterceptor');
    }]);

    // CORS
    app.config(['$httpProvider', function ($httpProvider) {
        $httpProvider.defaults.useXDomain = true;
        delete $httpProvider.defaults.headers.common['X-Requested-With'];
    }]);

    app.config(['$logProvider', function ($logProvider) {
        // turn debugging off/on (no info or warn)
        if ($logProvider.debugEnabled) {
            $logProvider.debugEnabled(true);
        }
    }]);
    
    //#region Configure the common services via commonConfig
    app.config(['commonConfigProvider', function (cfg) {
        cfg.config.controllerActivateSuccessEvent = config.events.controllerActivateSuccess;
        cfg.config.spinnerToggleEvent = config.events.spinnerToggle;
    }]);
    //#endregion

})();