# Human Capital Management System (HCM)

## Project Description
Human Capital Management System is a web application designed to manage the essential human resources tasks and data for your organization. It includes modules for managing Employees, Departments, Salaries, and Users Authentication.

## Features

### User Authentication
Handle user login and registration, allowing new users to sign up with roles such as admin or HR.
![User Authentication](https://i.ibb.co/KDQ54KD/image-7.png)

### Dashboard
An intuitive dashboard that welcomes users and provides quick navigation to various management features.
![Dashboard](https://i.ibb.co/M8qMN68/image-8.png)

### User Management
Create, edit, and manage user accounts, defining roles and access within the system.
![User Management](https://i.ibb.co/HFcKJRq/image-9.png)

### Employee Management
Manage detailed employee information, including contact details, department assignments, salaries, and more.
![Employee Management](https://i.ibb.co/dGdWcGC/image-13.png)

### Department Management
Add and manage department details to support the organizational structure.
![Department Management](https://i.ibb.co/HPf7sBR/image-11.png)

### Salary Management
Assign and manage salaries, bonuses, and deductions for employees.
![Salary Management](https://i.ibb.co/DCjLSPt/image-15.png)


## Technology Stack
- **Frontend**: ASP.NET MVC
- **Backend**: .NET Core 3.1, Entity Framework Core
- **Database**: SQL Server

## Getting Started

### Prerequisites
- .NET Core SDK
- SQL Server
- VSCODE or Visual Studio
- Git
- Docker


### Installation

1. Clone the repository:

git clone [https://github.com/ahmedazizou/Immedis_internship_2022_hcm_Ahmed_AitAzizou]

2. Navigate to the project directory:

cd HumanCapitalManagement

3. Restore the NuGet packages to install all dependencies:

dotnet restore

4. Update the connection string in `appsettings.json`.

5. Apply database migrations:

dotnet ef database update

5. Apply database migrations:
In both api and client folder
dotnet Run

### Running the Application without docker

Running the application with Docker simplifies the setup process and ensures consistency across different environments. Here’s how to get started:

1. **Build the Project**
   Compile the source code to prepare for running the application.

dotnet build

2. **Start the Database Container**
Launch the SQL Server database using Docker.

docker-compose up -d db


3. **Update Database Schema**
Change into the API project directory and apply the database migrations.

cd .\HumanCapitalManagementAPI
dotnet ef database update

Note: Ensure the connection string is updated from `localhost,1433` to `db` to connect to the Docker SQL Server image.

4. **Run the Application**
Start all services defined in the `docker-compose` file.


docker-compose up

The API will be listening on `https://localhost:44308`, and the client will be available on `https://localhost:44396`.

5. **Access the Application**
Open your web browser and navigate to `https://localhost:44396` to access the web application. Default credentials for an admin user are provided:
- Username: admin
- Password: 12345

Please follow these steps to ensure that your application runs smoothly in a Dockerized environment.

## Usage

After running the application, you will be greeted with a login page. Default credentials are provided for the admin user to explore the system's functionalities.