angular.module("theApp", [])
.controller("ctrl", [
    "$scope",
    function($scope) {
        $scope.levels = OurGreatApp.enums.Level;
    }
]);