# TaskMate - Task Management System

## Overview

TaskMate is a simple, web-based task management system designed to help users organize and track their tasks effectively. Built using **.NET 6.0** and **SQL Server**, TaskMate allows users to manage tasks, assign them to specific projects, and track their progress in an easy-to-use interface. This project is part of my personal learning journey and aims to showcase my ability to build full-stack applications with a strong focus on back-end development using **.NET**.

## Features

- **Create, Update, and Delete Tasks**: Manage your daily tasks with full CRUD (Create, Read, Update, Delete) functionality.
- **Role-based Access Control (RBAC)**: Users have different access levels to perform actions (Admin, User).
- **Task Prioritization**: Assign priority to tasks to keep track of urgent work.
- **Project Management**: Assign tasks to specific projects.
- **Authentication**: Users can sign up, log in, and manage their account.
- **Responsive Design**: Works well across devices using **Tailwind CSS** for styling.

## Tech Stack

- **Frontend**: React, Tailwind CSS
- **Backend**: .NET 6.0 (ASP.NET Core MVC)
- **Database**: SQL Server (raw SQL queries used)
- **Authentication**: JWT (JSON Web Token) for secure authentication
- **Hosting**: Local development environment for now, but can be hosted on any cloud provider (Azure, AWS)

## Getting Started

### Prerequisites

Make sure you have the following installed before you start:

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-editions)
- [Node.js](https://nodejs.org/en/) (for React development)
- [Git](https://git-scm.com/)

Setup
1. Clone the Repository
Start by cloning the repository to your local machine:

bash
Copy
git clone https://github.com/yourusername/taskmate.git
cd taskmate
2. Set Up the Backend
Open the TaskMate folder in your terminal.

Navigate to the backend project directory (where the .csproj file is located).

Run the following commands to restore dependencies and start the application:

bash
Copy
dotnet restore
dotnet run
3. Set Up the Frontend
Navigate to the frontend directory (where the React app is located).

Run the following commands to install the required dependencies and start the development server:

bash
Copy
cd client
npm install
npm start
This will open the application at http://localhost:3000 in your browser.

4. Set Up the Database
If you’re using SQL Server locally, make sure it’s running and accessible.

Create a new database called taskmate_db and configure the connection string in the backend’s appsettings.json file:

json
Copy
"ConnectionStrings": {
  "TaskMateDb": "Server=localhost;Database=taskmate_db;Trusted_Connection=True;"
}
Run the database migration to create the necessary tables:

bash
Copy
dotnet ef database update
5. Access the Application
After following the above steps, open the app in your browser:

Frontend: http://localhost:3000

Backend: http://localhost:5000 (if using the default port)

Features Demo
Create Task: Add new tasks with descriptions and deadlines.

Update Task: Edit task details or mark tasks as completed.

Delete Task: Remove tasks that are no longer needed.

Login/Signup: Users can register, log in, and manage their accounts.

Running Tests (Optional)
If you want to run unit tests for the backend, use the following command in the backend project directory:

bash
Copy
dotnet test
Contributions
Feel free to fork the repository, submit issues, or contribute pull requests. Your contributions are welcome!
