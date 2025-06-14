// AngularJS Todo Application
angular.module('todoApp', [])
.controller('TodoController', ['$scope', '$http', function($scope, $http) {
    
    // Initialize the todos array
    $scope.todos = [];
    
    // Initialize the new todo input
    $scope.newTodo = '';
    
    // Function to add a new todo
    $scope.addTodo = function () {
        if (!$scope.newTodo || $scope.newTodo.trim() === '') return;

        const todo = { text: $scope.newTodo };

        $http.post('http://localhost:5217/api/todos', todo)
            .then(function (response) {
                console.log('Todo saved:', response.data);
                $scope.newTodo = ''; // Clear input field

                // Optional: Add the new todo to your local list if you're maintaining one
                if (!$scope.todos) $scope.todos = [];
                $scope.todos.push(response.data);
            })
            .catch(function (error) {
                console.error('Error saving todo:', error);
            });
    };

    
    // Function to remove a todo by index
    $scope.removeTodo = function(index) {
        if (!$scope.todos || !$scope.todos[index]) {
            console.error("Todo list not found or index invalid");
            return;
        }

        const todo = $scope.todos[index];

        if (!todo.id) {
            console.error("Todo item has no ID");
            return;
        }

        $http.delete('http://localhost:5217/api/todos/' + todo.id)
            .then(function(response) {
                console.log('Todo deleted:', response.data);
                $scope.todos.splice(index, 1);
            })
            .catch(function(error) {
                console.error('Error deleting todo:', error);
            });
    };

    
    // Function to get the count of completed todos
    $scope.getCompletedCount = function() {
        return $scope.todos.filter(function(todo) {
            return todo.completed;
        }).length;
    };
    
    // Function to get the count of remaining (incomplete) todos
    $scope.getRemainingCount = function() {
        return $scope.todos.filter(function(todo) {
            return !todo.completed;
        }).length;
    };
    
    // Function to toggle all todos completion status
    $scope.toggleAll = function() {
        var allCompleted = $scope.todos.every(function(todo) {
            return todo.completed;
        });
        
        $scope.todos.forEach(function(todo) {
            todo.completed = !allCompleted;
        });
    };
    
    // Function to clear all completed todos
    $scope.clearCompleted = function() {
        $scope.todos = $scope.todos.filter(function(todo) {
            return !todo.completed;
        });
    };
}]);
