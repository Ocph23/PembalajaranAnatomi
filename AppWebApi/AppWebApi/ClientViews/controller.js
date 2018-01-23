angular.module("app.controller", [])
    .controller("MateriController", function ($scope, MateriService, $http) {
        $scope.Items = [];
       
        $scope.Init = function () {
            MateriService.source().then(function (response) {
                $scope.Items = response;
            });
        }

        $scope.SelectedItem = function (item)
        {
            $scope.model = item;
            $scope.ModalTitle = "Edit Materi";
        }


        $scope.AddNewItem = function (item) {
            if (item.Id != undefined && item.Id>0)
            {
                MateriService.UpdateItem(item).then(function (response) {
                    $scope.model = {};
                    alert('Data Berhasil Diubah');
                    $scope.dismiss();
                })
            } else
            {
                MateriService.AddItem(item).then(function (response) {
                    $scope.model = {};
                    alert('Data Berhasil Ditambah');
                    $scope.dismiss();
                })
            }
           
        }

        $scope.DeleteMateri = function (item) {
            $http({
                method: 'DELETE',
                url: "/api/Materi/" + item.Id,
                data: item
            }).then(function (response) {
                var index = $scope.Items.indexOf(item);
                $scope.Items.splice(index, 1);
                alert("Materi Berhasil Dihapus");
            }, function (error) {
                alert("Materi Tidak Dapat Dihapus");
            });
        }

    })

    .controller("SubMateriController", function ($scope, MateriService, SubMateriService, $http, $routeParams) {
        $scope.Items = [];
        $scope.Materi = {};
        $scope.ModalTitle = "Tambah Sub Materi";
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
                alert("Sub Materi Berhasil Ditambah");
                $scope.dismiss();
            });
        }


        $scope.DeleteSubMateri = function (item) {
            $http({
                method: 'DELETE',
                url: "/api/SubMateri/" + item.Id,
                data: item
            }).then(function (response) {
                var index = $scope.Items.indexOf(item);
                $scope.Items.splice(index, 1);
                alert("SubMateri Berhasil Dihapus");
            }, function (error) {
                alert("Sub Materi Tidak Dapat Dihapus");
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
            });
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
                alert("Informasi Sub Materi Berhasil Diubah")
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
            $scope.ModalTitle = 'Edit Topik';
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
                    $scope.dismiss();
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
                    $scope.dismiss();
                }, function (error) {
                    alert(error.Message);
                    // deferred.reject(error);
                });
            }
            $scope.Topik = {};
        }
        $scope.DeleteTopik = function(item)
        {
            $http({
                method: 'DELETE',
                url: "/api/Topik/" + item.Id,
                data: item
            }).then(function (response) {
                var index = $scope.Item.Topiks.indexOf(item);
                $scope.Item.Topiks.splice(index, 1);
                alert("Topik Berhasil Dihapus");
            }, function (error) {
                alert(error.Message);
            });
        }

    })



    .controller("SoalController", function ($scope,$http, MateriService, SubMateriService, $routeParams, $location) {
        $scope.Items = [];
        $scope.SubMateri = {};
        $scope.Init = function () {
            $http({
                method: 'Get',
                url: "/api/" + $routeParams.submateri + "/soal"
            }).then(function (response) {
                $scope.Soals = response.data;
                model = {};
                $http({
                    method: 'Get',
                    url: "api/" + $routeParams.submateri+"/submateribyid"
                }).then(function (response) {
                    $scope.SubMateri = response.data;
                }, function (error) {
                    alert(error.Message);
                    // deferred.reject(error);
                });





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
                    $scope.dismiss();
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
                    $scope.dismiss();
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
            $scope.ModalTitle = "Edit Soal";
        }


        $scope.ChangeSelectAnswer = function (Choices, item) {
            if (item.IsTrueAnswer == true) {
                angular.forEach(Choices, function (value, key) {

                    if (value != item)
                        value.IsTrueAnswer = false;

                })
            }

        }

        $scope.DeleteSoal = function (item) {
            $http({
                method: 'DELETE',
                url: "/api/Soal/" + item.Id,
                data: item
            }).then(function (response) {
                var index = $scope.Soals.indexOf(item);
                $scope.Soals.splice(index, 1);
                alert("Soal Berhasil Dihapus");
            }, function (error) {
                alert(error.Message);
            });
        }


    })




    ;