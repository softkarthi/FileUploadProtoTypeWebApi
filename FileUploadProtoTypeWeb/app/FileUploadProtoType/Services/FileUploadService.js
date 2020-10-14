(function (module) {
    module.service('FileUploadService', [
        '$http','$rootScope',
        function ($http, $rootScope) {

            function uploadFile(formFile) {
                return $http({
                    url: 'https://localhost:5001/api/FileUpload',
                    method: 'POST',
                    data: formFile,
                    headers: { 'Content-Type': undefined }, //this is important
                    transformRequest: angular.identity //also important
                });
            }

            function getFiles()
            {
                return $http({
                    url: 'https://localhost:5001/api/FileUpload',
                    method: 'GET',
                    headers: { 'Content-Type': undefined },
                    transformRequest: angular.identity 
                });
            }

            return {
                uploadFile: uploadFile,
                getFiles: getFiles
            };
        }]);
})(angular.module('FileUploadProtoType.FileUpload'));