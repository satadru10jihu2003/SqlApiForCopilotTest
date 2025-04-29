# SqlApiForCopilotTest

This project is an ASP.NET Core Web API that exposes an endpoint to receive a table name and a list of columns, connects to a SQL Server database, selects those columns from the specified table, and returns the result as a list. Dapper is used for database access.

## Features
- POST endpoint at `/api/query`.
- Receives JSON with `tableName` and `columns`.
- Validates table and column names to prevent SQL injection.
- Returns a list of results as JSON.

## Setup
1. Update the `DefaultConnection` string in `appsettings.json` with your SQL Server details.
2. Build and run the project:
   ```sh
   dotnet build
   dotnet run
   ```
3. Test the API using a tool like Postman or curl.

## Example Request
```
POST /api/query
Content-Type: application/json

{
  "tableName": "YourTable",
  "columns": ["Column1", "Column2"]
}
```

## Security Note
- Only alphanumeric and underscore characters are allowed in table and column names.
- No user data is directly interpolated into SQL without validation.
