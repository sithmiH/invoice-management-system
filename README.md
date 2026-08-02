## Invoice Management System

A web-based Invoice Management System developed using ASP.NET Core Web API.  
The system provides user authentication and invoice management features, including creating, viewing, updating, and deleting invoices.

## Technologies Used

- ASP.NET Core Web API
- C# (.NET 8.0)
- Dapper ORM
- SQL Server
- JWT Authentication
  
# Setup Instructions

## 1. Clone the Repository
git clone https://github.com/sithmiH/invoice-management-system.git

Open the solution in Visual Studio 2022.

## 2. Configure SQL Server

Create a SQL Server database.

Update the connection string in InvoiceManagement.API/appsettings.json

Example:
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=InvoiceManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"
}

## 3. Configure API URL

Open InvoiceManagement.Web/appsettings.json

Set the API Base URL.

Example:
"ApiSettings": {
  "BaseUrl": "https://localhost:7118/"
}

## 4. Restore Packages

In Visual Studio 
Build → Restore NuGet Packages

## 5. Run the Database Script

Execute the provided SQL script to create the required tables.

## 6. Run the API

Set InvoiceManagement.API as the startup project.

Run the project.

## 7. Run the MVC Application

Set InvoiceManagement.Web as the startup project.

Run the project.

# How to Run the Project

1. Start the API project.
2. Start the MVC project.
3. Open https://localhost:7206
4. Login or register.
5. Access the Dashboard.

# API Endpoints

## Authentication

 POST   `/api/Auth/register`   -Register a new user 
 
 POST   `/api/Auth/login`      -User login 

## Invoices

 GET    `/api/Invoice`        Get invoices  
 
 GET    `/api/Invoice/{id}`   Get invoice by ID 
 
 POST   `/api/Invoice`        Create invoice 
 
 PUT    `/api/Invoice/{id}`   Update invoice
 
 DELETE `/api/Invoice/{id}`   Delete invoice 

## Users

GET   `/api/Users`   Retrieve all registered users

# Swagger Configuration

Swagger is enabled in the API project for testing endpoints.

Run the API project and navigate to:
https://localhost:7118/swagger

To authorize requests:

1. Login using:
   POST /api/Auth/login

2. Copy the JWT token.

3. Click the 'Authorize' button in Swagger.

4. Enter Bearer YOUR_TOKEN

5. Click 'Authorize'.

You can now access protected endpoints.


