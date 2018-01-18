angular.module("app.controller", [])
    .controller("MateriController", function ($scope, MateriService) {
        $scope.Items = [];
       
        $scope.Init = function () {
            MateriService.source().then(function (response) {
                $scope.Items = response;
            });
        }

        $scope.AddNewItem = function (item) {
            MateriService.AddItem(item).then(function (response) {

            })
        }
    })

    .controller("SubMateriController", function ($scope, MateriService, SubMateriService, $routeParams,$location) {
        $scope.Items = [];
        $scope.Materi = {};
        $scope.Init = function () {
            MateriService.GetItem($routeParams.id).then(function (data) {
                $scope.Materi = data;
                SubMateriService.source($routeParams.id).then(function (response) {
                    $scope.Items = response;






                });
            })
        }

        $scope.AddSubMateri = function (item) {

            item.MateriId = $scope.Materi.Id;

            SubMateriService.AddItem(item).then(function (response) {
                $location.path('/detail/' + response.Id);
            });
        }


       
    })

    .controller("DetailController", function ($scope, MateriService, SubMateriService, $routeParams, $location,$http) {
        $scope.Item = {};
        $scope.Materi = {};
        $scope.Soals = [];
        $scope.Init = function () {
            MateriService.GetItem($routeParams.materi).then(function (data) {
                $scope.Materi = data;
                SubMateriService.GetItem($routeParams.submateri).then(function (response) {
                    $scope.Item = response;
                    $http({
                        method: 'Get',
                        url: "/api/"+ response.Id+"/soal"
                    }).then(function (response) {
                        $scope.Soals = response.data;
                        model = {};
                    }, function (error) {
                        alert(error.Message);
                        // deferred.reject(error);
                    });
                });
            })
        }

        $scope.UploadFoto = function (file) {
            if (file !== undefined) {
                var url = "/api/" + $scope.Item.Id + "/image";
                var form = new FormData();
                form.append("file", file);
                var settings = {
                    "async": true,
                    "crossDomain": true,
                    "url": url,
                    "method": "Post",
                    "headers": {
                        "cache-control": "no-cache",
                    },
                    "processData": false,
                    "contentType": false,
                    "mimeType": "multipart/form-data",
                    "data": form
                }

                $.ajax(settings).done(function (response, data) {
                    alert("Foto Berhasil Diubah");
                    var d = JSON.parse(response);
                    $scope.Item.DataGambar = d.DataGambar;
                    $scope.Item.Gambar = d.Gambar;
                }).error(function (response) {
                    alert(response.responseText);
                })

                    ;
            } else {
                alert("Anda Belum Memilih File Foto");
            }
        }

        $scope.UploadSound = function (file) {
            if (file !== undefined) {
                var url = "/api/" + $scope.Item.Id + "/sound";
                var form = new FormData();
                form.append("file", file);
                var settings = {
                    "async": true,
                    "crossDomain": true,
                    "url": url,
                    "method": "Post",
                    "headers": {
                        "cache-control": "no-cache",
                    },
                    "processData": false,
                    "contentType": false,
                    "mimeType": "multipart/form-data",
                    "data": form
                }

                $.ajax(settings).done(function (response, data) {
                    alert("Foto Berhasil Diubah");
                    var d = JSON.parse(response);
                    $scope.Item.DataSound = d.DataSound;
                    $scope.Item.Sound = d.Sound;
                }, function (request, status, error) {
                    alert(request.responseText);
                    });

            } else {
                alert("Anda Belum Memilih File Foto");
            }
        }

        $scope.UploadVideo = function (file) {
            if (file !== undefined) {
                var url = "/api/" + $scope.Item.Id + "/animation";
                var form = new FormData();
                form.append("file", file);
                var settings = {
                    "async": true,
                    "crossDomain": true,
                    "url": url,
                    "method": "Post",
                    "headers": {
                        "cache-control": "no-cache",
                    },
                    "processData": false,
                    "contentType": false,
                    "mimeType": "multipart/form-data",
                    "data": form
                }

                $.ajax(settings).done(function (response, data) {
                    alert("Foto Berhasil Diubah");
                    var d = JSON.parse(response);
                    $scope.Item.DataAnimasi = d.DataAnimasi;
                    $scope.Item.Animasi = d.Animasi;
                }).fail(function (jqXHR, textStatus) {
                    alert(jqXHR.responseText);
                    });

                    ;
            } else {
                alert("Anda Belum Memilih File Foto");
            }
        }


        $scope.CreateNewSoal = function ()
        {
            $scope.NewSoal = {};
            $scope.NewSoal.Value = "";
            $scope.NewSoal.Choices = [];
            for (var i = 0; i < 4;i++)
            {
                var Option = { "Value": "", "IsTrueAnswer": false };
                $scope.NewSoal.Choices.push(Option);
            }
        }

        $scope.ChangeSelectAnswer = function (Choices, item)
        {
            if (item.IsTrueAnswer == true)
            {
                angular.forEach(Choices, function (value, key) {

                    if (value != item) 
                        value.IsTrueAnswer = false;

                })
            }
         
        }


        $scope.SaveNewSoal = function (model)
        {
            model.SubMateriId = $scope.Item.Id;
            $http({
                method: 'POST',
                url: "/api/soal",
                data: model
            }).then(function (response) {
                $scope.Soals.push(response.data);
                model = {};
            }, function (error) {
                alert(error.Message);
                // deferred.reject(error);
            });
        }


    })






    ;