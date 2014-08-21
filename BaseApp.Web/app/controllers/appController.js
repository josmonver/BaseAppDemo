(function () {
	'use strict';

	var controllerId = 'AppController';

	angular.module('app').controller(controllerId,
        ['$rootScope', '$scope', '$location', '$stateParams', '$state', 'AuthService', 'common', 'config', appController]);

	function appController($rootScope, $scope, $location, $stateParams, $state, AuthService, common, config) {
		var vm = this;
		var getLogFn = common.logger.getLogFn;
		var log = getLogFn(controllerId);
		var events = config.events;

		$rootScope.spinnerOptions = config.spinnerOptions;
		$rootScope.busyMessage = 'Cargando ...';
		$rootScope.isBusy = true;
		$rootScope.toggleSpinner = toggleSpinner;

		activate();

		function activate() {
			common.activateController([], controllerId)
                .then(function () {
                	//log(controllerId + ' activated');
                });
		}

		function toggleSpinner(on) {
			$rootScope.isBusy = on;
		}

		$rootScope.$on('$stateChangeStart',
            function (event, toState, toParams, fromState, fromParams) {
                // if next page requires log in and user isn't logged in, redirect login
            	if (toState.data.requireLogin && !AuthService.isLoggedIn()) {
            	    AuthService.saveAttemptState(toState); // Save state for redirect after log in
            	    $state.go('anon.login');
            	    log('You must be logged in!');
            	    event.preventDefault();
            	} else {
            	    toggleSpinner(true);
            	}
            }
        );

		$rootScope.$on('$stateChangeSuccess',
            function (event, toState, toParams, fromState, fromParams) {
                toggleSpinner(false);
            }
        );

		$rootScope.$on(events.spinnerToggle,
            function (data) { toggleSpinner(data.show); }
        );

	}

})();
