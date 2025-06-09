angular.module('employeeApp').service('AuthService', function () {
    var employeeData = null;

    this.setEmployeeData = function (id, fio) {
        employeeData = { id: id, fio: fio };
        // Храним в sessionStorage (только на время сессии)
        sessionStorage.setItem('employeeData', JSON.stringify(employeeData));
    };

    this.getEmployeeData = function () {
        if (!employeeData) {
            // Пытаемся получить данные из sessionStorage
            var stored = sessionStorage.getItem('employeeData');
            employeeData = stored ? JSON.parse(stored) : null;
        }
        return employeeData;
    };

    this.clearEmployeeData = function () {
        // Очищаем данные
        employeeData = null;
        sessionStorage.removeItem('employeeData');
    };

    // Проверяем, авторизован ли пользователь
    this.isAuthenticated = function () {
        return this.getEmployeeData() !== null;
    };
});