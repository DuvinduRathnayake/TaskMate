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

### Setup

#### Clone the Repository

```bash
git clone https://github.com/yourusername/taskmate.git
cd taskmate
