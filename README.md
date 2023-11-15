# Human Capital Management System (HCM)

## Project Description
Human Capital Management System is a web application designed to manage the essential human resources tasks and data for your organization. It includes modules for managing Employees, Departments, Salaries, and Users Authentication.

## Features

### User Authentication
Handle user login and registration, allowing new users to sign up with roles such as admin or HR.
![User Authentication](https://camo.githubusercontent.com/aa780fb5989523eae96b70d0d308e5e55d8f2fcc122d54ff5d122a8a526510b0/68747470733a2f2f692e6962622e636f2f4b445135344b442f696d6167652d372e706e67)

### Dashboard
An intuitive dashboard that welcomes users and provides quick navigation to various management features.
![Dashboard](https://camo.githubusercontent.com/05cfc5ec95dc612b065e353e1a0a90bfc7c6934651fcc6cdea153c20c843b787/68747470733a2f2f692e6962622e636f2f4d38714d4e36382f696d6167652d382e706e67)

### User Management
Create, edit, and manage user accounts, defining roles and access within the system.
![User Management](https://camo.githubusercontent.com/14107e1bc788342a14f8830fe44e655019f85e00d7779930ea19f74648ac06ae/68747470733a2f2f692e6962622e636f2f4846634b4a52712f696d6167652d392e706e67)

### Employee Management
Manage detailed employee information, including contact details, department assignments, salaries, and more.
![Employee Management](https://camo.githubusercontent.com/c1e1366a5591c72f91d8dbbe4b2bc41ab203483b14915d7fad5c7402b2badef5/68747470733a2f2f692e6962622e636f2f644764576347432f696d6167652d31332e706e67)

### Department Management
Add and manage department details to support the organizational structure.
![Department Management](https://camo.githubusercontent.com/3da6ead983fb13e44e8fa27f18e3811024c36e90b241f022055e380c9577db65/68747470733a2f2f692e6962622e636f2f485066377342522f696d6167652d31312e706e67)

### Salary Management
Assign and manage salaries, bonuses, and deductions for employees.
![Salary Management](https://camo.githubusercontent.com/9f4e1168837771df8d7272e5a72c24a27aecfe8c095997df48ed2958a6ae6c14/68747470733a2f2f692e6962622e636f2f44436a4c5350742f696d6167652d31352e706e67)


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