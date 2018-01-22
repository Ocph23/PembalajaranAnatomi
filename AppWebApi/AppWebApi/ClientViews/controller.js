angular.module("app.controller", [])
    .controller("MateriController", function ($scope, MateriService) {
        $scope.Items = [];
       
        $scope.Init = function () {
            MateriService.source().then(function (response) {
                $scope.Items = response;
            });
        }

        $scope.SelectedItem = function (item)
        {
            $scope.model = item;
        }


        $scope.AddNewItem = function (item) {
            if (item.Id != undefined && item.Id>0)
            {
                MateriService.UpdateItem(item).then(function (response) {
                    $scope.model = {};
                    alert('Data Berhasil Diubah');
                })
            } else
            {
                MateriService.AddItem(item).then(function (response) {
                    $scope.model = {};
                    alert('Data Berhasil Ditambah');
                })
            }
           
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

    .controller("DetailController", function ($scope, MateriService, SubMateriService, $routeParams, $location, $http,$sce) {
      
        $scope.Item = {};
        $scope.Materi = {};
        $scope.Soals = [];
        $scope.Init = function () {
            MateriService.GetItem($routeParams.materi).then(function (data) {
                $scope.Materi = data;
                SubMateriService.GetItem($routeParams.submateri).then(function (response) {
                    $scope.Item = response;
                    CKEDITOR.instances.editor1.setData(response.Penjelasan);
                   
                });
            })
        }



        $scope.SavePenjelasan = function(item)
        {
            var res = CKEDITOR.instances.editor1.getData();
            var data = { "Id": item.Id, "JudulSubMateri": item.JudulSubMateri, "Penjelasan": res };
           
            SubMateriService.UpdateItem(data).then(function(response){
                alert("Penjelasan Tersimpan");

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
                    var d = JSON.parse(response);
                    $scope.Item.DataGambar = d.DataGambar;
                    $scope.Item.Gambar = d.Gambar;
                    alert("Gambar Tersimpan");
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
                    alert("Video Animasi Berhasil Diubah");
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

        $scope.UpdateSubMateri = function (item) {
            SubMateriService.UpdateItem(item).then(function (response) {
                alert("Infomrasi Sub Materi Berhasil Diubah")
            });
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
        $scope.EditTopik = function(item)
        {
            $scope.Topik = item;
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


        $scope.SaveNewTopik = function(item)
        {
            if (item.Id == undefined) {
                item.SubMateriId = $scope.Item.Id;
                $http({
                    method: 'POST',
                    url: "/api/Topik",
                    data: item
                }).then(function (response) {
                    alert("Topik Tersimpan");
                    $scope.Item.Topiks.push(response.data);
                
                }, function (error) {
                    alert(error.Message);
                    // deferred.reject(error);
                    });
            } else
            {
                $http({
                    method: 'PUT',
                    url: "/api/Topik/" + item.Id,
                    data: item
                }).then(function (response) {
                    alert("Topik Tersimpan");
                 
                }, function (error) {
                    alert(error.Message);
                    // deferred.reject(error);
                });
            }
            $scope.Topik = {};
        }


    })



    .controller("SoalController", function ($scope,$http, MateriService, SubMateriService, $routeParams, $location) {
        $scope.Items = [];
        $scope.Materi = {};
        $scope.Init = function () {
            $http({
                method: 'Get',
                url: "/api/" + $routeParams.submateri + "/soal"
            }).then(function (response) {
                $scope.Soals = response.data;
                model = {};
            }, function (error) {
                alert(error.Message);
                // deferred.reject(error);
            });
        }

        $scope.SaveNewSoal = function (model) {

            if (model.Id == undefined)
            {
                model.SubMateriId = $routeParams.submateri;
                $http({
                    method: 'POST',
                    url: "/api/soal",
                    data: model
                }).then(function (response) {
                    $scope.Soals.push(response.data);
                    alert("Data Tersimpan");
                    model = {};
                }, function (error) {
                    alert(error.Message);
                    // deferred.reject(error);
                    });
            } else
            {
                $http({
                    method: 'PUT',
                    url: "/api/soal",
                    data: model
                }).then(function (response) {
                    alert("Soal Berhasil Diubah");
                    model = {};
                }, function (error) {
                    alert(error.Message);
                    // deferred.reject(error);
                });
            }


          
        }


        $scope.CreateNewSoal = function () {
            $scope.NewSoal = {};
            $scope.NewSoal.Value = "";
            $scope.NewSoal.Choices = [];
            for (var i = 0; i < 4; i++) {
                var Option = { "Value": "", "IsTrueAnswer": false };
                $scope.NewSoal.Choices.push(Option);
            }
        }

        $scope.SelectedItem = function (item)
        {
            $scope.NewSoal = item;
        }


        $scope.ChangeSelectAnswer = function (Choices, item) {
            if (item.IsTrueAnswer == true) {
                angular.forEach(Choices, function (value, key) {

                    if (value != item)
                        value.IsTrueAnswer = false;

                })
            }

        }



    })




    ;