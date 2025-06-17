# angularjs-todo-test

To run this project

-Download and install the latest .net (net9.0).
-Create DB tables, TodoItems and ErrorLog using sql script \angularjs-todo-test\CreateTables.sql.
-Change connection string in appsettings.json to whichever DB you created the tables in.
-Run dotnet build
-Run dotnet run
-Check terminal for "Now listening on: http://localhost:5217" This was my default, might be different for you, if so, update post and delete URL's in app.js.
-Open the above URL to see the front end and test.


Notes

-Error logging in a real app should be more specific, specifying at which stage of the process the error occurred, eg; add, delete or other relevant info.
-Security in a real app would be essential, un and pwd or client secret and client key, also would be HTTPS with SSL cert.
-Encryption could be used, PGP.
-Async and await could be used so as not to hang up the UI while waiting for response from API.
-I tried to use OOP and not get carried away and over engineer, but stick to instructions, the other API calls could be implemented as well, fetch and filter etc.

