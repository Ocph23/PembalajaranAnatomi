angular.module("app", ["ngRoute", "app.controller"])
.directive('myModal', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            scope.dismiss = function () {
                element.modal('hide');
            };
        }
    }
})
    .directive('fileModel', ['$parse', function ($parse) {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                var model = $parse(attrs.fileModel);
                var modelSetter = model.assign;

                element.bind('change', function () {
                    scope.$apply(function () {
                        modelSetter(scope, element[0].files[0]);
                        var canvas = element.parent().find('img');
                        if (canvas.length == 0)
                        {
                            canvas = element.parent().find('audio');
                            if (canvas.length == 0) 
                            {
                                canvas = element.parent().find('video');
                            }
                        }
                            
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            canvas.attr('src', e.target.result)

                        };
                        reader.readAsDataURL(element[0].files[0]);
                    });
                });
            }
        };
    }])

    .directive('fileCanvas', ['$parse', function ($parse) {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                var model = $parse(attrs.fileCanvas);
                element.bind('click', function () {
                    var input = element[0].querySelector("input");
                    input.click();
                });


            }
        };
    }])

    .config(function ($routeProvider) {
        
        $routeProvider
            .when("/", {
                templateUrl: "../ClientViews/main.html",
            })
            .when("/materi", {
                templateUrl: "../ClientViews/materi.html",
                controller: "MateriController"
            })

            .when("/submateri/:id", {
                templateUrl: "../ClientViews/submateri.html",
                controller: "SubMateriController"
            })

            .when("/detail/:materi/:submateri", {
                templateUrl: "../ClientViews/detail.html",
                controller: "DetailController"
            })

            .when("/soal/:submateri", {
                templateUrl: "../ClientViews/soal.html",
                controller: "SoalController"
            })


            ;
    })

    .factory("MateriService", function ($http,$q) {
        var service = {};
        var isInstance = false;
        var collection = [];

        service.source = function () {
            deferred = $q.defer();
            if (!isInstance) {
                $http({
                    method: 'GET',
                    url:"/api/materi",
                }).then(function (response) {
                    // With the data succesfully returned, we can resolve promise and we can access it in controller
                    collection = [];
                    angular.forEach(response.data, function (value, key) {
                       collection.push(value);
                    })
                    deferred.resolve(collection);
                    isInstance = true;
                }, function (error) {
                    alert(error.Message);
                    // deferred.reject(error);
                });

            } else {
                deferred.resolve(collection);
            }

            return deferred.promise;
        }

        service.GetItem = function (id) {
            deferred = $q.defer();
            if (!isInstance) {
                $http({
                    method: 'GET',
                    url: "/api/materi?id=" + id,
                }).then(function (response) {
                    // With the data succesfully returned, we can resolve promise and we can access it in controller

                    deferred.resolve(response.data);
                  
                }, function (error) {
                    alert(error.Message);
                    // deferred.reject(error);
                });

            } else {
                var data = {};
                angular.forEach(collection, function (value, key) {
                    if (value.Id == id)
                        data = value;
                })
                deferred.resolve(data);
            }

            return deferred.promise;
        }


        service.AddItem = function(item)
        {
            deferred = $q.defer();
            $http({
                method: 'POST',
                url:"/api/materi",
                data: item
            }).then(function (response) {
                collection.push(response.data);
                deferred.resolve(response.data);
            }, function (error) {
                alert(error.Message);
                // deferred.reject(error);
            });
            return deferred.promise;
        }




        service.UpdateItem = function (item) {
            deferred = $q.defer();
            $http({
                method: 'PUT',
                url:"/api/materi?Id="+item.Id,
                data: item
            }).then(function (response) {
                var data = response.data;
                angular.forEach(collection, function (value, key) {
                    if (value.Id == item.Id)
                    {
                        value.KodeMateri = data.KodeMateri;
                        value.Judul = data.Judul;
                    }
                });
                deferred.resolve(response.data);
            }, function (error) {
                alert(error.Message);
                // deferred.reject(error);
            });
            return deferred.promise;
        }


        service.DeleteItem = function (item) {
            deferred = $q.defer();
            $http({
                method: 'Delete',
                url:"/api/materi?Id="+item.Id,
                data: item
            }).then(function (response) {
                var data = response.data;
                angular.forEach(collection, function (value, key) {
                    if (value.Id == item.Id) {
                        collection.splice(value, 1);
                    }
                });
                deferred.resolve(response.data);
            }, function (error) {
                alert(error.Message);
                // deferred.reject(error);
            });
            return deferred.promise;
        }

        return service;
    })


    .factory("SubMateriService", function ($http, $q) {
        var service = {};
        var isInstance = false;
        var collection = [];

        service.source = function (id) {
            deferred = $q.defer();
            $http({
                method: 'GET',
                url: "api/" + id + "/submateri"
            }).then(function (response) {
                collection = [];
                // With the data succesfully returned, we can resolve promise and we can access it in controller
                angular.forEach(response.data, function (value, key) {
                    collection.push(value);
                })
                deferred.resolve(collection);
            }, function (error) {
                alert(error.Message);
                // deferred.reject(error);
            });

            return deferred.promise;
        }

        service.GetItem = function (id) {
            deferred = $q.defer();
            if (!isInstance) {
                $http({
                    method: 'GET',
                    url: "/api/submateri/" + id,
                }).then(function (response) {
                    // With the data succesfully returned, we can resolve promise and we can access it in controller

                    deferred.resolve(response.data);

                }, function (error) {
                    alert(error.data.Message);
                    // deferred.reject(error);
                });

            } else {
                var data = {};
                angular.forEach(collection, function (value, key) {
                    if (value.Id == id)
                        data = value;
                })
                deferred.resolve(data);
            }

            return deferred.promise;
        }


        service.AddItem = function (item) {
            deferred = $q.defer();
            $http({
                method: 'POST',
                url: "/api/submateri",
                data: item
            }).then(function (response) {
                collection.push(response.data);
                deferred.resolve(response.data);
            }, function (error) {
                alert(error.Message);
                // deferred.reject(error);
            });
            return deferred.promise;
        }


        service.UpdateItem = function (item) {
            deferred = $q.defer();
            $http({
                method: 'PUT',
                url: "/api/submateri?Id="+item.Id,
                data: item
            }).then(function (response) {
                var data = response.data;
                angular.forEach(collection, function (value, key) {
                    if (value.Id == item.Id) {
                        value.KodeMateri = data.KodeMateri;
                        value.Judul = data.Judul;
                    }
                });
                deferred.resolve(response.data);
            }, function (error) {
                alert(error.Message);
                // deferred.reject(error);
            });
            return deferred.promise;
        }


        service.DeleteItem = function (item) {
            deferred = $q.defer();
            $http({
                method: 'Delete',
                url: "/api/submateri?Id=" + item.Id,
                data: item
            }).then(function (response) {
                var data = response.data;
                angular.forEach(collection, function (value, key) {
                    if (value.Id == item.Id) {
                        collection.splice(value, 1);
                    }
                });
                deferred.resolve(response.data);
            }, function (error) {
                alert(error.Message);
                // deferred.reject(error);
            });
            return deferred.promise;
        }

        return service;
    })
    ;