//this is used to parse the profile
function url_base64_decode(str) {
    
}

(function () {
    'use strict';

    var serviceId = 'Base64';

    angular.module('app').factory(serviceId,
        [Base64]);

    function Base64() {
        var service = {
            decode: decode
        };

        return service;

        function decode(str) {
            var output = str.replace('-', '+').replace('_', '/');
            switch (output.length % 4) {
                case 0:
                    break;
                case 2:
                    output += '==';
                    break;
                case 3:
                    output += '=';
                    break;
                default:
                    throw 'Illegal base64url string!';
            }
            return window.atob(output); //polifyll https://github.com/davidchambers/Base64.js
        }
    }

})();
