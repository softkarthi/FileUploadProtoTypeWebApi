'use strict';
angular.module('FileUploadProtoType.FileUpload').controller('fileUploadController', [
    '$scope', 'FileUploadService',
    function ($scope, FileUploadService) {
        $scope.fileList = [];

        $scope.uploadFile = function () {
            var fileInput = document.getElementById('fileInput');
            if (fileInput.files.length === 0) return;

            var formFile = fileInput.files[0];

            var payload = new FormData();
            payload.append("formFile", formFile);

            FileUploadService.uploadFile(payload).then(function (response) {
                if (response.data === 'success' && response.status === 200) {
                    fileInput.value = '';
                    getFiles();
                }
            }).catch(function (response) {
                fileInput.value = '';
            });
        };

        this.$onInit = function () {
            getFiles();
        }

        function getFiles()
        {
            $scope.fileList = [];
            FileUploadService.getFiles().then(function (response) {
                if (response.status === 200) {
                    $scope.fileList = response.data;
                }
            }).catch(function (response) {
                fileInput.value = '';
            });
        }
    }
]);