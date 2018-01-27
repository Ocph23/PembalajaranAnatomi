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
            $scope.IsInsert = false;

        }


        $scope.AddNewItem = function (item, insert) {
            if (!insert)
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
                url: "/api/Materi/" + item.KodeMateri,
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
        $scope.Materis = [];
        $scope.ModalTitle = "Tambah Sub Materi";
        $scope.withCombo = false; 
        $scope.Init = function () {
            if ($routeParams.id=="null")
            {
                $scope.withCombo = true;
                MateriService.source().then(function (response) {
                    $scope.Materis = response;
                   
                });
            } else
            {
                MateriService.GetItem($routeParams.id).then(function (data) {
                    $scope.Materi = data;
                    SubMateriService.source($routeParams.id).then(function (response) {
                        $scope.Items = response;
                    });
                })
            }

            
        }
        $scope.OptionSelected = function ()
        {
            SubMateriService.source($scope.Materi.KodeMateri).then(function (response) {
                $scope.Items = response;
            });
        }

        $scope.AddSubMateri = function (item) {

            item.KodeMateri = $scope.Materi.KodeMateri

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
            var data = { "KodeMateri": item.KodeMateri, "KodeSubMateri": item.KodeSubMateri, "JudulSubMateri": item.JudulSubMateri, "Penjelasan": res };
           
            SubMateriService.UpdateItem(data).then(function(response){
                alert("Penjelasan Tersimpan");


            })
        }

        $scope.UploadFoto = function (file) {
            if (file !== undefined) {
                var url = "/api/" + $scope.Item.KodeSubMateri + "/image";
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
                var url = "/api/" + $scope.Item.KodeSubMateri + "/animation";
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
            $scope.NewSoal.KodeSubMateri;
            for (var i = 0; i < 4;i++)
            {
                var Option = { "Value": "", "IsTrueAnswer": false };
                $scope.NewSoal.Choices.push(Option);
            }
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


        $scope.AddOptionValue=function(soal, opt)
        {
            if (opt.IsTrueAnswer)
                soal.JawabanBenar = opt.Value;
        }

        function IsValidChoice(choices)
        {
            var result = false;
            angular.forEach(choices, function (value, key) {
                if (value.IsTrueAnswer)
                    result = true;
            });
            return result;
        }

        $scope.SaveNewSoal = function (model,isNew)
        {
            if (!IsValidChoice(model.Choices))
            {
                alert("Anda Belum Memilih Jawaban Benar");
            } else
            {
                var soal = {"KodeKuis":model.KodeKuis, "NoUrut": model.NoUrut, "KodeSubMateri": $scope.SubMateri.KodeSubMateri, "Pertanyaan": model.Pertanyaan };
                var ops1 = model.Choices[0];
                soal.JawabanA = ops1.Value;
                $scope.AddOptionValue(soal, ops1);

                var ops2 = model.Choices[1];
                soal.JawabanB = ops2.Value;;
                $scope.AddOptionValue(soal, ops2);

                var ops3 = model.Choices[2];
                soal.JawabanC = ops3.Value;;
                $scope.AddOptionValue(soal, ops3);

                var ops4 = model.Choices[3];
                soal.JawabanD = ops4.Value;;
                $scope.AddOptionValue(soal, ops4);
                if (isNew) {
                 
                    $http({
                        method: 'POST',
                        url: "/api/soal",
                        data: soal
                    }).then(function (response) {
                        $scope.Soals.push(response.data);
                        alert("Data Tersimpan");
                        model = {};
                        $scope.dismiss();
                    }, function (error) {
                        alert(error.Message);
                        // deferred.reject(error);
                    });
                } else {
                    $http({
                        method: 'PUT',
                        url: "/api/soal",
                        data: soal
                    }).then(function (response) {
                        alert("Soal Berhasil Diubah");
                        var index = $scope.Soals.indexOf(model);
                        $scope.Soals.splice(index, 1);
                        $scope.Soals.push(soal);
                        $scope.dismiss();
                    }, function (error) {
                        alert(error.Message);
                        // deferred.reject(error);
                    });
                }
            }
        }


        $scope.CreateNewSoal = function () {
            $scope.NewSoal = {};
            $scope.NewSoal.Pertanyaan = "";
            $scope.IsNew = true;
            $scope.NewSoal.Choices = [];
            for (var i = 0; i < 4; i++) {
                var Option = {"Number":i, "Value": "", "IsTrueAnswer": false };
                $scope.NewSoal.Choices.push(Option);
            }
        }

        function IsTrueAnswerChange(benar,option)
        {
            if (benar === option.Value)
                option.IsTrueAnswer = true;
        }

        $scope.SelectedItem = function (item)
        {
            $scope.NewSoal = item;
            $scope.NewSoal.Choices = [];
            var OptionA = { "Number": 1, "Value": $scope.NewSoal.JawabanA, "IsTrueAnswer": false };
            IsTrueAnswerChange($scope.NewSoal.JawabanBenar, OptionA);
            $scope.NewSoal.Choices.push(OptionA);

            var OptionB = { "Number": 2, "Value": $scope.NewSoal.JawabanB, "IsTrueAnswer": false };
            IsTrueAnswerChange($scope.NewSoal.JawabanBenar, OptionB);
            $scope.NewSoal.Choices.push(OptionB);

            var OptionC = { "Number": 3, "Value": $scope.NewSoal.JawabanC, "IsTrueAnswer": false };
            IsTrueAnswerChange($scope.NewSoal.JawabanBenar, OptionC);
            $scope.NewSoal.Choices.push(OptionC);

            var OptionD = { "Number": 4, "Value": $scope.NewSoal.JawabanD, "IsTrueAnswer": false };
            IsTrueAnswerChange($scope.NewSoal.JawabanBenar, OptionD);
            $scope.NewSoal.Choices.push(OptionD);

            $scope.IsNew = false;
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

    .controller("TopikController", function ($scope, $http, SubMateriService, $routeParams) {
        $scope.Items = [];
        $scope.SubMateri = {};
        $scope.Init = function () {
            SubMateriService.GetItem($routeParams.submateri).then(function (response) {
                $scope.Item = response;
            });
        }

        $scope.EditTopik = function (item) {
            $scope.Topik = item;
            $scope.IsInsert = false;
            $scope.ModalTitle = 'Edit Topik';
        }
       
        $scope.SaveNewTopik = function (item, isinsert) {
            if (isinsert) {
                item.KodeSubMateri = $scope.Item.KodeSubMateri;
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
            } else {
                $http({
                    method: 'PUT',
                    url: "/api/Topik/" + item.IdTopik,
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
        $scope.DeleteTopik = function (item) {
            $http({
                method: 'DELETE',
                url: "/api/Topik/" + item.KodeTopik,
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




    ;