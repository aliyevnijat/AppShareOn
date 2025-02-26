# AppShareOn - README

Welcome to the **AppShareOn** project! This application is a modular .NET Core solution that consists of several projects working together, including backend services, APIs, and a client-side application built with Blazor Hybrid and Web technologies.

The following README provides detailed instructions on how to set up and run the AppShareOn application locally on your development machine.

---

## Table of Contents

- [Prerequisites](#prerequisites)
- [Project Structure](#project-structure)
- [Setup Instructions](#setup-instructions)
  - [1. Install .NET 9](#1-install-net-9)
  - [2. Install EF Core Tools](#2-install-ef-core-tools)
  - [3. Install SQLite](#3-install-sqlite)
  - [4. Create Database File](#4-create-database-file)
  - [5. Run Migrations](#5-run-migrations)
- [Running the Application](#running-the-application)
  - [1. Running the API](#1-running-the-api)
  - [2. Running the Client](#2-running-the-client)
- [Conclusion](#conclusion)

---

## Prerequisites

Before running the application, make sure your development environment is set up correctly:

- **.NET 9 SDK**  
  Download and install .NET 9 SDK from the official website:  
  [https://dotnet.microsoft.com/en-us/download/dotnet/9.0](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

- **Entity Framework Core CLI Tools**  
  Install the EF Core tools globally to enable database migrations and other EF-related commands:
  ```bash
  dotnet tool install --global dotnet-ef
  ```

- **SQLite**  
  Install SQLite on your machine. Follow the official guide to get the latest version for your platform:  
  [https://www.sqlite.org/download.html](https://www.sqlite.org/download.html)

---

## Project Structure

The AppShareOn solution consists of several projects organized into different layers:

- **AppShareOn.Core**: Contains domain models, interfaces, and shared business logic.
- **AppShareOn.Application**: Contains application services and logic for managing domain models and workflows.
- **AppShareOn.Infrastructure**: Contains implementations for database access, file storage, and other external integrations.
- **AppShareOn.Api**: The Web API project that exposes the application services to external clients (REST API).
- **AppShareOn.Client**: A Blazor Hybrid app with Web support. Includes three subprojects:
  - **AppShareOn.Client.App**: Core Blazor Hybrid app project.
  - **AppShareOn.Client.Shared**: Shared components and resources for both the Hybrid and Web client.
  - **AppShareOn.Client.Web**: Blazor Web app for browser-based clients.

---

## Setup Instructions

### 1. Install .NET 9

Make sure you have the **.NET 9 SDK** installed on your machine. You can verify the installation by running:
```bash
dotnet --version
```
If it returns a version number that starts with "9", you’re good to go.

---

### 2. Install EF Core Tools

To run database migrations, you will need to install the **EF Core CLI tools**. Run the following command to install the EF Core tools globally:
```bash
dotnet tool install --global dotnet-ef
```

You can verify the installation by running:
```bash
dotnet ef --version
```

---

### 3. Install SQLite

If you haven't already, download and install **SQLite** from the official website. SQLite is required for the application's database, and you'll need to have the SQLite command-line tools installed to create and manage the database file.

Follow the official SQLite download page:  
[https://www.sqlite.org/download.html](https://www.sqlite.org/download.html)

---

### 4. Create Database File

At the root of the solution directory (where the `.sln` file is located), create an empty SQLite database file named `AppShareOn.db`.

You can do this manually or by using the SQLite CLI:
```bash
sqlite3 AppShareOn.db
```
This will create an empty database file, and you can exit by typing `.exit`.

Alternatively, you can use any database management tool like DB Browser for SQLite to create the database file.

---

### 5. Run Migrations

Now, it’s time to apply the database migrations to set up the schema. Follow these steps:

1. Open a terminal/command prompt and navigate to the **AppShareOn.Infrastructure** project directory:
   ```bash
   cd AppShareOn.Infrastructure
   ```

2. Run the EF Core migration command to apply the migrations and update the SQLite database:
   ```bash
   dotnet ef database update --startup-project ../AppShareOn.Api
   ```

This command will apply any pending migrations to the `AppShareOn.db` database, creating the necessary tables and schema.

---

## Running the Application

### 1. Running the API

The **AppShareOn.Api** project is the backend Web API. To run the API locally:

1. Navigate to the **AppShareOn.Api** directory:
   ```bash
   cd AppShareOn.Api
   ```

2. Run the API with the following command:
   ```bash
   dotnet run -lp https
   ```

   This will start the API on HTTPS. By default, the API will be accessible on `https://localhost:7140` (or a similar URL depending on your system configuration).

---

### 2. Running the Client

The **AppShareOn.Client** project contains the Blazor Hybrid application, including both the **Web** and **Hybrid** client versions.

To run the Web client (Blazor Web app):

1. Navigate to the **AppShareOn.Client/Web** directory:
   ```bash
   cd AppShareOn.Client.Web
   ```

2. Run the Web client:
   ```bash
   dotnet run -lp https
   ```

   The Web app should now be accessible on `https://localhost:7241` (or a similar URL depending on your system configuration).

---

## Conclusion

You have now set up and run the **AppShareOn** application locally. The backend API is running, and the client is accessible in your browser. You can start interacting with the application and extend it based on your requirements.

If you have any issues or need further assistance, please consult the project documentation or contact the development team.

Happy coding! 😊

---

### Additional Resources

- **.NET Documentation**: [https://docs.microsoft.com/en-us/dotnet/](https://docs.microsoft.com/en-us/dotnet/)
- **Entity Framework Core Documentation**: [https://docs.microsoft.com/en-us/ef/core/](https://docs.microsoft.com/en-us/ef/core/)
- **Blazor Documentation**: [https://docs.microsoft.com/en-us/aspnet/core/blazor/](https://docs.microsoft.com/en-us/aspnet/core/blazor/)
