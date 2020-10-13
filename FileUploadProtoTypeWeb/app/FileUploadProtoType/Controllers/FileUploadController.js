'use strict';
console.log('FileUploadController.js');
angular.module('FileUploadProtoType.FileUpload').controller('fileUploadController', [
    '$scope', 'FileUploadService', 
    function ($scope, FileUploadService) {

        $scope.uploadFile = function () {
            var fileInput = document.getElementById('fileInput');
            if (fileInput.files.length === 0) return;

            var formFile = fileInput.files[0];

            var payload = new FormData();
            payload.append("formFile", formFile);

            FileUploadService.uploadFile(payload).then(function (response) {
                if (response.data === 'success' && response.status === 200) {
                    fileInput.value= '';
                }
            }).catch(function (response) {
                fileInput.value= '';
            });
        };
    }
]);