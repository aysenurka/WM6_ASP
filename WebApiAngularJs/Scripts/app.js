/// <reference path="angular.js" />

var app = angular.module("myApp", []);
var host = "http://localhost:62629/";

app.controller("ProductCtrl", function ($scope) {
	$scope.urunler = [];
	$scope.sepetList = [];
	$scope.ekleMi = false;
	$scope.sepetToplam = 0;

	function init() {
		var data = JSON.parse(localStorage.getItem("urunler"));
		$scope.urunler = data === null ? [] : data;
		data = JSON.parse(localStorage.getItem("sepet"));
		$scope.sepetList = data === null ? [] : data;
		sepetHesapla();
	}
	$scope.ekle = function () {
		$scope.urunler.push({
			id: guid(),
			urunAdi: $scope.yeni.urunAdi,
			fiyat: $scope.yeni.fiyat,
			eklenmeZamani: new Date()
		});
		$scope.yeni.urunAdi = "";
		$scope.yeni.fiyat = "";
		localStorage.setItem("urunler", JSON.stringify($scope.urunler));
	};

	$scope.sil = function (id) {
		for (var i = 0; i < $scope.urunler.length; i++) {
			var data = $scope.urunler[i];
			if (id == data.id) {
				$scope.urunler.splice(i, 1);
				break;
			}
		}
		localStorage.setItem("urunler", JSON.stringify($scope.urunler));
	};

	$scope.sepeteekle = function (urun) {
		var varMi = false;
		for (var i = 0; i < $scope.sepetList.length; i++) {
			var data = $scope.sepetList[i];
			if (data.id === urun.id) {
				varMi = true;
				data.adet++;
				break;
			}
		}
		if (!varMi) {
			urun.adet = 1;
			$scope.sepetList.push(urun);
		}
		localStorage.setItem("sepet", JSON.stringify($scope.sepetList));
		sepetHesapla();
	};

	function sepetHesapla() {
		$scope.sepetToplam = 0;
		for (var i = 0; i < $scope.sepetList.length; i++) {
			var data = $scope.sepetList[i];
			$scope.sepetToplam += data.adet * data.fiyat;
		}
	}

	$scope.cikar = function (urun) {
		var index = $scope.sepetList.indexOf(urun);
		if (index > -1)
			$scope.sepetList.splice(index, 1);
		localStorage.setItem("sepet", JSON.stringify($scope.sepetList));
		sepetHesapla();
	};

	function guid() {
		function S4() {
			return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
		}

		// then to call it, plus stitch in '4' in the third group
		return (S4() + S4() + "-" + S4() + "-4" + S4().substr(0, 3) + "-" + S4() + "-" + S4() + S4() + S4()).toLowerCase();
	}

	init();
});

app.controller("CategoryCtrl", function ($scope, $http) {
	$scope.categoryList = [];
	$scope.istekVarMi = false;
	$scope.guncelleMi = false;

	function init() {
		$scope.istekVarMi = true;
		$http({
			method: 'GET',
			url: host + "api/category/getall"
		}).then(function successCallback(response) {
			$scope.istekVarMi = false;
			var r = response.data;
			if (r.success) {
				$scope.categoryList = r.data;
			} else {
				alert(r.message);
			}
		}, function errorCallback(response) {
			$scope.istekVarMi = false;
			console.log(response);
		});
	}

	$scope.ekle = function () {
		$scope.istekVarMi = true;
		$http({
			method: "POST",
			url: host + "api/category/add",
			data: $scope.yeni
		}).then(function (rs) {
			$scope.istekVarMi = false;
			if (rs.data.success) {
				alert(rs.data.message);
				init();
			} else {
				alert("bir hata oluştu " + rs.data.message);
			}
		},
			function (re) {
				$scope.istekVarMi = false;
			});
	};

	$scope.sil = function (id) {
		$scope.istekVarMi = true;
		$http({
			method: "DELETE",
			url: host + "api/category/delete/" + id
		}).then(function (rs) {
			$scope.istekVarMi = false;
			if (rs.data.success) {
				alert(rs.data.message);
				init();
			} else {
				alert("bir hata oluştu " + rs.data.message);
			}
		}, function (re) {
			$scope.istekVarMi = false;
			console.log(re);
			alert(re.data.Message);
		});
	};

	$scope.getir = function (category) {
		$scope.guncelleMi = true;
		$scope.yeni = category;
	};

	$scope.guncelle = function () {
		$scope.istekVarMi = true;
		$http({
			method: "PUT",
			url: host + "api/category/putcategory/" + $scope.yeni.CategoryID,
			data: $scope.yeni
		}).then(function (rs) {
			$scope.istekVarMi = false;
			if (rs.data.success) {
				alert(rs.data.message);
				$scope.yeni = null;
				$scope.guncelleMi = false;
				init();
			} else {
				alert("bir hata oluştu " + rs.data.message);
			}
		},
			function (re) {
				$scope.istekVarMi = false;
				console.log(re);
				alert(re.data.Message);
			});
	};
	init();
});

app.controller("ShipperCtrl", function ($scope, $http) {
	$scope.shipperList = [];
	$scope.ekleMi = false;
	$scope.guncelleMi = false;
	$scope.istekVarmi = false;

	function init() {
		$scope.istekVarmi = true;
		$http({
			method: 'GET',
			url: host + "api/shipper/getall"
		}).then(function successCallBack(response) {
			$scope.istekVarmi = false;
			var r = response.data;
			if (r.success) {
				$scope.shipperList = r.data;
			} else {
				alert(r.message);
			}
		}, function errorCallBack(response) {
			$scope.istekVarmi = false;
			console.log(response);
		});
	}

	$scope.ekle = function () {
		$scope.istekVarmi = true;
		$http({
			method: 'POST',
			url: host + "api/shipper/add",
			data: $scope.yeni
		}).then(function rs() {
			$scope.istekVarmi = false;
			if (rs.data.success) {
				alert(rs.data.message);
				init();
			} else {
				alert("bir hata oluştu " + rs.data.message);
			}
		},
			function re() {
				$scope.istekVarmi = false;
			});
	};

	$scope.sil = function (id) {
		$scope.istekVarmi = true;
		$scope.http({
			method: 'DELETE',
			url: host + "api/shipper/delete/" + id
		}).then(function rs(response) {
			$scope.istekVarmi = false;
			if (response.data.success) {
				alert(response.data.message);
				init();
			} else {
				alert("bir hata oluştu " + response.data.message);
			}
		},
			function (response) {
				$scope.istekVarmi = false;
				console.log(response.data.message);
				alert(response.data.message);
			});
	};

	$scope.guncelle = function (shipper) {
		$scope.istekVarmi = true;
		$scope.guncelleMi = true;
		$scope.http({
			method: 'PUT',
			url: "api/shipper/put/" + shipper.ShipperID,
			data: shipper
		}).then(function rs(response) {
			$scope.istekVarmi = false;
			if (response.data.success) {
				alert(response.data.message);
				shipper = null;
				$scope.guncelleMi = false;
			} else {
				alert("bir hata oluştu " + response.data.message);
			}
			}),
			function re(response) {
				$scope.istekVarmi = false;
				console.log(response.data.message);
				alert(response.data.message);
			};
	};

	init();
});
