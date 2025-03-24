Task Management Application

Description

This is a simple Task Management Application built using ASP.NET Core MVC with Razor Views. It allows users to create, edit, view, and delete tasks while managing their completion status. The application follows the Model-View-Controller (MVC) architecture and utilizes Microsoft SQL Server as the database.

Features

Manage Tasks: Users can create tasks with a title, description, and due date.

Task Status: Mark tasks as completed or not completed.

Edit Tasks: Modify existing tasks.

View Task Details: View all details of a task.

Delete Tasks: Remove tasks from the system.

User Authentication: Secure login and authentication using ASP.NET Core Identity.

Razor Views: UI built with Razor templates.

Security Measures: HTTPS enforcement and SQL Server security best practices.

Technologies Used

Backend: ASP.NET Core MVC (C#)

Frontend: Razor Views, HTML, CSS, Bootstrap

Database: Microsoft SQL Server

Security: ASP.NET Identity for authentication, HTTPS enforcement

Development Tools: Visual Studio, SQL Server Management Studio (SSMS)

Security Features

Authentication: Uses ASP.NET Core Identity for user authentication and authorization.

Authorization: Users can only access their tasks and cannot modify others' tasks.

HTTPS Security: The application enforces HTTPS using port 7132.

Database Security: SQL Server Trusted Connection and MultipleActiveResultSets enabled for efficient database operations.

CSRF Protection: Built-in ASP.NET MVC anti-forgery token protection.

Razor View Templates

Special Razor View Templates

_ViewStart.cshtml: Sets the layout for all views.

_ViewImports.cshtml: Defines commonly used namespaces and tag helpers.

_LoginPartial.cshtml: Manages the login/logout UI elements.

Dependency Injection (DI)

ASP.NET Core utilizes Dependency Injection (DI) for managing services like database connections and authentication.

How to Set Up and Run the Application

Prerequisites

Install .NET SDK (Latest version)

Install Microsoft SQL Server and SQL Server Management Studio (SSMS)

Install Visual Studio (with ASP.NET and web development workload)

Setup Instructions

Clone the Repository

git clone https://github.com/stephanfdo/TaskManager.git
cd TaskManager

Update Connection String
Modify the appsettings.json file with your SQL Server details:

"ConnectionStrings": {
  "DefaultConnection": "Server=DESKTOP-G72PKF0;Database=TaskDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}

Apply Database Migrations
Open the terminal in the project directory and run:

dotnet ef database update

Run the Application

dotnet run

The application will be available at https://localhost:7132/.

Additional Notes

The application does not use an API; it directly interacts with the database using Entity Framework Core.

Uses GET for retrieving data and POST for modifying data to ensure proper HTTP request handling.

The https_port is explicitly set to 7132 in appsettings.json.
