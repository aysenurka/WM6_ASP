/// <reference path="angular.js" />

var app = angular.module("myApp", []);

app.controller("ProdyctCtrl", function($scope) {
	$scope.urunler = [];
	$scope.ekle = function() {
		$scope.urunler.push({
			id: guid(),
			urunAdi: $scope.yeni.urunAdi,
			fiyat: $scope.yeni.fiyat,
			eklenmeZamani: new Date()
		});
	};

	function guid() {

	}
});