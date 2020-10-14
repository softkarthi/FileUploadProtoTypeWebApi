var routerApp = angular.module('FileUploadProtoType', ['ui.router', 'FileUploadProtoType.FileUpload']).config(function ($stateProvider, $urlRouterProvider) {

    $urlRouterProvider.otherwise('/');

    $stateProvider
        .state('home', {
            url: '/',
            controller: 'fileUploadController',
            templateUrl: 'FileUploadProtoType/Views/FileUploadPage.html'
        });
});