angular.module('employeeApp').controller('TaskController', function ($scope, $http, $sanitize, AuthService) {
    $scope.description = '';
    $scope.time = '';
    $scope.tasks = [];
    $scope.totalTime = '';
    $scope.filteredTasks = [];
    $scope.searchText = '';
    $scope.error = ''; 
    $scope.taskSuccess = '';

    $scope.dateCompleted = new Date().toLocaleDateString('ru-RU', {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric'
    });
    $scope.employee = AuthService.getEmployeeData();

    $scope.isAuth = function () {
        return AuthService.isAuthenticated();
    };

    $scope.isTaskDataValid = function () {
        return $scope.taskForm && $scope.taskForm.description.$valid && $scope.taskForm.time.$valid;
    };

    // Загружаем список задач
    $scope.loadTasks = function () {
        if (!$scope.isAuth()) {
            $scope.error = 'Необходимо войти в систему';
            return;
        }


        $http.get('http://localhost:8080/EmployeeTasksService/GetList').then(function (response) {
            if (!response || !response.data) {
                $scope.error = 'Пришел пустой ответ';
                $scope.filteredTasks = [];
                $scope.tasks = [];
                $scope.totalTime = '';
                return;
            }
            if (response.data.Success) {
                $scope.filteredTasks = response.data.Data.EmployeeTasks;
                $scope.tasks = response.data.Data.EmployeeTasks;
                $scope.totalTime = response.data.Data.TotalTimeSpent;
                $scope.error = '';
            } else {
                $scope.error = response.data.ErrorMessage;
            }            
        }).catch(function (error) {
            // Ошибка
            console.error('Ошибка загрузки:', error);
            $scope.error = 'Не удалось загрузить данные';
        }).finally(function () {
            // Выполнится в любом случае
            console.log('Запрос завершен');
        });
    };

    // Фильтруем по тексту поиска
    $scope.$watch('searchText', function (newVal) {
        if (newVal) {
            let newValLower = newVal.toLowerCase();
            $scope.filteredTasks = $scope.tasks.filter(function (task) {
                return task.Description.toLowerCase().includes(newValLower)
                    || task.FIO_Employee.toLowerCase().includes(newValLower);
            });
        } else {
            $scope.filteredTasks = $scope.tasks;
        }
    });

    // Создаем новую задачу
    $scope.createNewTask = function () {
        if (!$scope.isAuth()) {
            $scope.error = 'Необходимо войти в систему';
            return;
        }

        $scope.description = $sanitize($scope.description);
        $scope.time = $sanitize($scope.time);

        let formData = {
            id_Employee: $scope.employee.id,
            DateCompleted: $scope.dateCompleted,
            Description: $scope.description,
            TimeSpent: $scope.time
        };

        $http({
            method: 'POST',
            url: 'http://localhost:8080/EmployeeTasksService/CreateNewTask',
            data: formData,
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            }
        }).then(function (response) {
            if (!response || !response.data) {
                $scope.error = 'Пришел пустой ответ';
                $scope.taskSuccess = '';
                return;
            }

            if (response.data.Success) {
                $scope.taskSuccess = response.data.Data;
                $scope.error = '';
                $scope.loadTasks();
            } else {
                $scope.error = response.data.ErrorMessage;
                $scope.taskSuccess = '';
            }
            
        }).catch(function (error) {
            $scope.error = 'Ошибка при создании задачи: ' + error.status + ' ' + error.data;
            $scope.taskSuccess = '';
        });
    }

    $scope.logout = function () {
        AuthService.clearEmployeeData()
        window.location.href = 'Login.html';
    }

    $scope.loadTasks();
});

