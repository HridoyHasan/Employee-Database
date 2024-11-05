# Employee Management System

This is a basic **Employee Management System** built with **ASP.NET Core MVC** and **PostgreSQL**. The application allows authenticated users to manage employees by performing CRUD operations (Create, Read, Update, Delete). It also includes a simple login system to restrict access to the management features.

## Features

- **Authentication**: Predefined login for secure access (`username: admin`, `password: admin`)
- **Employee Management**:
  - Add new employees
  - View a list of all employees
  - Update employee details
  - Soft-delete employees

## Technologies Used

- **ASP.NET Core MVC**: For building the web application
- **Entity Framework Core**: For ORM and database management
- **PostgreSQL**: As the database to store employee information
- **Bootstrap**: For styling and responsive design

## Prerequisites

Before you begin, ensure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or higher)
- [PostgreSQL](https://www.postgresql.org/download/) (version 12 or higher)
- [Visual Studio Code](https://code.visualstudio.com/) or any other preferred IDE

## Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/employee-management-system.git
cd employee-management-system

### 2. Configure the Database
Create a PostgreSQL database named EmployeeDB (or any name you prefer).
Update the connection string in appsettings.json:
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=your_host;Port=your_port;Database=EmployeeDB;Username=your_username;Password=your_password;SSL Mode=Require;Trust Server Certificate=true"
  }
}

### 3. Apply Migrations
Run the following commands to apply migrations and set up the database:
dotnet ef migrations add InitialCreate
dotnet ef database update

### 4. Run the Application
Start the application by running:
dotnet run
