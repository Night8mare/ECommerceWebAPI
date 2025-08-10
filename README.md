# README.md

# CleanArchEcommerce.API

A modular, clean architecture-based ASP.NET Core Web API for e-commerce applications, built with .NET 9 and modern best practices.

---

## Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Architecture](#architecture)
- [Getting Started](#getting-started)
- [API Documentation](#api-documentation)
- [Folder Structure](#folder-structure)

---

## Features

- Clean Architecture separation (API, Application, Infrastructure, Domain)
- JWT Authentication and Authorization
- Swagger/OpenAPI documentation
- Dependency Injection throughout
- Serilog logging (console, file, debug)
- Entity Framework Core with SQL Server
- Modular service registration

---

## Tech Stack

- **.NET 9**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **Serilog**
- **Swagger (Swashbuckle)**
- **JWT Authentication**
- **AutoMapper**
- **MediatR**
- **FluentValidation**

---

## Architecture

This project follows the Clean Architecture pattern:

- **API Layer**: Entry point, HTTP endpoints, Swagger, authentication setup.
- **Application Layer**: Business logic, use cases, service interfaces.
- **Infrastructure Layer**: Data access, external services, concrete implementations.
- **Domain Layer**: Entities (Rich Model), Interfaces.

Each layer is in a separate project for clear separation of concerns and testability.

---

## Getting Started

> **Note:** The `appsettings.json` file (containing sensitive configuration like connection strings and secrets) is **not included** in the repository. 
You must provide your own before running the application.

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- SQL Server (or update connection string for your DB)
- [Visual Studio 2022](https://visualstudio.microsoft.com/)

### Setup

1. **Clone the repository:**
- git clone <repo-url> cd CleanArchEcommerce.API

2. **Restore dependencies:**
- dotnet restore


3. **Add your `appsettings.json`:**
- Copy the sample from documentation or create your own.
- Set up your connection string and JWT settings.

4. **Apply database migrations:**
- dotnet ef database update --project CleanArchEcommerce.Infrastructure
- or
- ECommerce.bak file provided to be restored in sql server management

5. **Run the application:**
- dotnet run --project CleanArchEcommerce.API


6. **Access Swagger UI:**
- Navigate to `https://localhost:<port>/` in your browser.

---

## API Documentation

- **Swagger UI** is enabled in development mode.
- All endpoints are documented and support JWT authentication.
- To authorize, use the "Authorize" button in Swagger and provide a valid JWT token.

---
## Folder Structure

CleanArchEcommerce/
│
├── CleanArchEcommerce.API/           # API Layer: HTTP endpoints, controllers, Swagger, logging
│   ├── Program.cs
│   ├── appsettings.json              # (Not included in repo; user must provide this)
│   └── ...                           # Other API-related files (controllers, configs, middleware)
│
├── CleanArchEcommerce.Application/   # Application Layer: business logic, use cases, service interfaces
│   └── ...                           # DTOs, validators, commands/queries, services
│
├── CleanArchEcommerce.Infrastructure/ # Infrastructure Layer: data access & external service implementations
│   └── ...                            # EF Core DbContext, repositories, service implementations
│
├── README.md                         # Project documentation (you’re reading it!)
│
└── ...                               # Solution files, migrations, and other root-level configs
