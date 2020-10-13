console.log('FileUploadService.js');
(function (module) {
    module.service('FileUploadService', [
        '$http', 
        function ($http ) {
            return {
                uploadFile: function (formFile) {
                    return $http({
                        url: 'https://localhost:5001/api/FileUpload',
                        method: 'POST',
                        data: formFile,
                        headers: { 'Content-Type': undefined }, //this is important
                        transformRequest: angular.identity //also important
                    });
                }
            };
        }]);
})(angular.module('FileUploadProtoType.FileUpload'));