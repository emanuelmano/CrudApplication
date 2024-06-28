Introduction

CrudApplication is a web API project designed to manage company, country, and contact information. 
It demonstrates the use of ASP.NET Core and Entity Framework Core to perform CRUD (Create, Read, Update, Delete) operations,
 utilizing dependency injection and logging for robust application development.
 
Technologies Used

.NET Core

SQL Server

Entity Framework Core

Swagger 

ILogger

Dependency Injection 

1. Getting Started
   
.NET 8.0 Core SDK

2.SQL Server instance

Visual Studio or another C# compatible IDE


1.Update the connection string in appsettings.json to point to your SQL Server database.

2. SQL Server Database
   
The application utilizes a SQL Server database for data storage and management. SQL Server is known for its robust performance and scalability.


Setup:


Ensure the connection string in the application settings is correctly configured for your SQL Server database.

3. Code-First Approach and Migrations
   
This project follows a code-first approach, where the database schema is generated from the application's domain models. 
Migrations are used to keep the database schema in sync with model changes.

4. Logging and Exception Handling
   
The application includes comprehensive logging to capture runtime information and manage errors effectively. 
Proper error handling ensures that exceptions are gracefully handled and meaningful feedback is provided to users.

5. Dependency Injection (DI)
   
Dependency Injection is utilized to manage component dependencies, enhancing testability and promoting loose coupling within the application.

Usage:

Register services in the DI container (typically in Program.cs or Startup.cs).
Inject dependencies into your controllers, services, and other components as needed.

6. Swagger for API Documentation
   
Swagger (OpenAPI) is integrated into the project to generate an interactive API documentation interface. It allows developers to explore and test API endpoints directly from the browser.

Usage:

Access the Swagger UI by navigating to /swagger after running the application.


7. Onion Architecture
   
The application is structured following the Onion Architecture pattern, which organizes the codebase into concentric layers. This approach ensures separation of concerns and enhances maintainability. 
