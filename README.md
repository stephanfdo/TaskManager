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




Screenshots 


![c1](https://github.com/user-attachments/assets/933b435b-b672-4b97-8ad0-cec045302d26)

![c2](https://github.com/user-attachments/assets/1b42fc40-63b5-4229-82bd-d1e41ea6e47d)

![c3](https://github.com/user-attachments/assets/0f6d9735-88b3-48e4-8126-c94d238ff68f)

![c4](https://github.com/user-attachments/assets/79122e14-4e87-4cde-97f8-d2eb47a615bf)

![c5](https://github.com/user-attachments/assets/b724d971-3b08-458e-92e3-91b5e1cd2235)

![c6](https://github.com/user-attachments/assets/d7390949-b97d-4458-9ae3-3fcb46b1e1c7)

![c7](https://github.com/user-attachments/assets/00b7433c-7fda-4a41-ab80-083a4b62a10f)
![c8](https://github.com/user-attachments/assets/af327bc9-f3bc-45c2-a3ee-a0e162b2905c)

![c9](https://github.com/user-attachments/assets/84e4abb0-6a0e-46e8-b9f0-f60d50aea8fd)

![c10](https://github.com/user-attachments/assets/b7d91847-feeb-4f5c-accc-40c2f0e4e45b)











