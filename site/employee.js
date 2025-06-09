angular.module('employeeApp').controller('EmployeeController', function ($scope, $http, $sanitize, AuthService) {
    $scope.email = '';
    $scope.password = '';
    $scope.secondpassword = '';
    $scope.firstname = '';
    $scope.lastname = '';
    $scope.error = '';
    $scope.regSuccess = '';

    $scope.isLoginDataValid = function () {
        return $scope.loginForm && $scope.loginForm.email.$valid && $scope.loginForm.password.$valid;
    };

    $scope.isRegDataValid = function () {
        return $scope.regForm &&
            $scope.regForm.email.$valid &&
            $scope.regForm.firstname.$valid &&
            $scope.regForm.lastname.$valid &&
            $scope.regForm.password.$valid &&
            $scope.password === $scope.secondpassword;
    };

    $scope.isPasswordValid = function () {
        return $scope.password === $scope.secondpassword;
    };

    $scope.login = function () {
        $scope.email = $sanitize($scope.email);
        $scope.password = $sanitize($scope.password);

        let formData = {
            Email: $scope.email,
            Password: $scope.password
        };

        $http({
            method: 'POST',
            url: 'http://localhost:8081/EmployeeService/Login',
            data: formData,
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(function (response) {
            if (!response || !response.data) {
                $scope.error = 'Пришел пустой ответ';
                return;
            }

            if (response.data.Success) {
                AuthService.setEmployeeData(response.data.Data.id_Employee, response.data.Data.FIO_Employee);
                // Успешная авторизация - перенаправляем
                window.location.href = 'EmployeeTasks.html';
                $scope.error = '';                
            } else {
                $scope.error = response.data.ErrorMessage;
            }                        
        }).catch(function (error) {
            // Ошибка
            console.error('Ошибка загрузки:', error);
            $scope.error = 'Не удалось осуществить вход в систему: ' + error.status + ' ' + error.data;
        }).finally(function () {
            // Выполнится в любом случае
            $scope.loading = false;
            console.log('Запрос завершен');
        });
    }

    $scope.register = function () {
        $scope.email = $sanitize($scope.email);
        $scope.password = $sanitize($scope.password);
        $scope.firstname = $sanitize($scope.firstname);
        $scope.lastname = $sanitize($scope.lastname);

        let formData = {
            Email: $scope.email,
            Password: $scope.password,
            FirstName: $scope.firstname,
            LastName: $scope.lastname
        };

        $http({
            method: 'POST',
            url: 'http://localhost:8081/EmployeeService/Register',
            data: formData,
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(function (response) {
            if (!response || !response.data) {
                $scope.error = 'Пришел пустой ответ';
                return;
            }

            if (response.data.Success) {
                $scope.regSuccess = response.data.Data;
                $scope.error = '';
            } else {
                $scope.error = response.data.ErrorMessage;
                $scope.regSuccess = '';
            }
        }).catch(function (error) {
            $scope.error = 'Ошибка при регистрации:' + error.status + ' ' + error.data;
        });
    }
});

