# Human Capital Management System (HCM)

## Project Description
Human Capital Management System is a web application designed to manage the essential human resources tasks and data for your organization. It includes modules for managing Employees, Departments, Salaries, and Users Authentication.

## Features
- **Employee Management**: Create, update, view, and delete employee records.
- **Department Management**: Manage departments within the organization.
- **Salary Management**: Assign salaries to employees, including details about bonuses and deductions.
- **User Authentication**: Handle user login, registration new users with role admin or HR

## Technology Stack
- **Frontend**: ASP.NET MVC
- **Backend**: .NET Core 3.1, Entity Framework Core
- **Database**: SQL Server

## Getting Started

### Prerequisites
- .NET Core SDK
- SQL Server
- Visual Studio or suitable .NET IDE
- Git
- Docker


### Installation

1. Clone the repository:
2. Navigate to the project directory:
cd HumanCapitalManagement
3. Restore the NuGet packages to install all dependencies:
dotnet restore
4. Update the connection string in both appsettings.json
5. Apply database migrations to set up your SQL database schema:
dotnet ef database update


### Running the Application without docker

1. Build the project to compile the source code:

dotnet build

2. Run the application: both in client and api folder

dotnet run

api
Now listening on: https://localhost:44308
Now listening on: http://localhost:50187
client
Now listening on: https://localhost:44396
Now listening on: http://localhost:37360

3. Access the web application by navigating to `https://localhost:44396` or the URL provided by the hosting environment.
a user is created by defult username : admin , password: 12345

## Usage

After running the application, you will be greeted with a login page. Default credentials are provided for the admin user to explore the system's functionalities.