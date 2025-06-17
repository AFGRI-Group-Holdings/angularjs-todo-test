using TodoTest.Models;
using TodoTest.Services;

    var builder = WebApplication.CreateBuilder(args);
    var app = builder.Build();

    app.UseDefaultFiles();
    app.UseStaticFiles();

    string connStr = builder.Configuration.GetConnectionString("DefaultConnection");
    var db = new DatabaseHelper(connStr);
    var logger = new ErrorLogger(connStr);

    // $http.post('http://localhost:5217/api/todos', todo)
    app.MapPost("/api/todos", async (TodoItem item) =>
    {
        try
        {
            int newId = db.AddTodoItem(item); 
            item.Id = newId; //set ID for front end to use.

            return Results.Ok(item); 
        }
        catch (Exception ex)
        {
            logger.Log(ex);
            return Results.Problem("Error adding item");
        }
    });

    // $http.delete('http://localhost:5217/api/todos/' + todo.id)
    app.MapDelete("/api/todos/{id:int}", (int id) =>
    {
        try
        {
            db.DeleteTodoItem(id);
            return Results.Ok("Item deleted");
        }
        catch (Exception ex)
        {
            logger.Log(ex);
            return Results.Problem("Error deleting item");
        }
    });

    app.Run();
