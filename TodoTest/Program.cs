using TodoTest.Models;
using TodoTest.Services;

    var builder = WebApplication.CreateBuilder(args);
    var app = builder.Build();

    app.UseDefaultFiles();
    app.UseStaticFiles();

    // Database helper with your connection string
    string connStr = builder.Configuration.GetConnectionString("DefaultConnection");
    var db = new DatabaseHelper(connStr);
    var logger = new ErrorLogger();

    // POST /api/todos
    app.MapPost("/api/todos", async (TodoItem item) =>
    {
        try
        {
            int newId = db.AddTodoItem(item); // Assuming AddTodoItem returns new ID
            item.Id = newId; // Set the generated ID so the frontend can use it

            return Results.Ok(item); // ✅ Return full object
        }
        catch (Exception ex)
        {
            logger.Log(ex);
            return Results.Problem("Error adding item");
        }
    });

    // DELETE /api/todos/{id}
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
