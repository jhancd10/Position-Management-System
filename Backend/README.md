# PositionManagement

## Project Overview

**PositionManagement** is a Clean Architecture solution built with .NET 8 that provides a modular, maintainable system for managing job positions with full CRUD operations for positions, departments, and recruiters. It incorporates:

- **Generic Repository Pattern** with EF Core for reusable data access, including optional includes on `GetByIdAsync`.
- **Application Services** for business logic and validations (e.g., uniqueness constraints).
- **CQRS** via MediatR for commands and queries.
- **FluentValidation** for request validation.
- **EF Core In-Memory** provider for persistence in development and testing.
- **Global Exception Handling** and **API Key Security** via custom middleware.
- **Pipeline Behaviors** for logging and performance metrics.
- **Dependency Injection Extensions** for modular service registration.

## Table of Contents

- [Architecture](#architecture)
- [Folder Structure](#folder-structure)
- [Key Features](#key-features)
- [Conventions](#conventions)
- [Requirements](#requirements)
- [Installation](#installation)
- [Configuration](#configuration)
- [Running the Application](#running-the-application)
- [Data Seeding](#data-seeding)
- [Testing](#testing)
- [API Endpoints](#api-endpoints)

## Architecture

This project adheres to the **Clean Architecture** pattern, separating concerns into distinct layers:

- **Presentation**: `PositionManagement.Api` (ASP.NET Core Web API)
- **Application**: Business logic, MediatR commands & queries, DTOs, validation behaviors
- **Domain**: Core entities, enums, and domain interfaces
- **Infrastructure**: EF Core `DbContext`, repositories, middleware, and service registrations
- **Shared**: Cross-cutting utilities, exceptions, and bootstrapping (`DefaultWebApplication`)

## Folder Structure

```
Backend/                            ⬅ Root folder
│
├── PositionManagement.sln         ⬅ .NET solution file
│
├── Source/                        ⬅ Application source code
│   ├── PositionManagement.Api/         ⬅ Presentation layer (Web API project)
│   │   • Controllers and endpoints
│   │   • Startup orchestration via DefaultWebApplication
│   │
│   ├── PositionManagement.Application/ ⬅ Application layer (MediatR, DTOs, services, UseCases)
│   │   • Interfaces (IBaseRepository, IPositionService, IRecruiterService, IDepartmentService)
│   │   • Implementations (GenericRepository, BaseRepository)
│   │   • Application Services with business validations
│   │   • UseCases: Queries / Commands / Validators
│   │   • Pipeline Behaviors (LoggingBehavior, TimingBehavior)
│   │   • DI extensions (MediatR, FluentValidation, Services)
│   │
│   ├── PositionManagement.Domain/      ⬅ Domain layer
│   │   • Core entities (Position, Recruiter, Department) implementing IBaseEntity<Guid>
│   │   • Enums (PositionStatusEnum)
│   │
│   ├── PositionManagement.Infrastructure/ ⬅ Infrastructure layer
│   │   • `PositionManagementDbContext` with EF Core mappings and seed support
│   │   • Generic `BaseRepository<T, TId>` implementing IRepository with includes
│   │   • Middleware (`ExceptionHandlingMiddleware`, `ApiKeyMiddleware`)
│   │   • Swagger filter (`ApiKeyHeader`)
│   │   • DI extensions (DbContext, Repositories, Services, Swagger, CORS, Logging, Middleware)
│   │   • `DefaultWebApplication` helper
│   │
│   └── PositionManagement.Shared/      ⬅ Shared layer
│       • Configuration models (ApiKey)
│       • Shared exceptions and helpers (NotFoundException, ConflictException, ExceptionHelper)
│       • DI extensions for WebApi, CORS, Logging
│
└── Tests/                         ⬅ Unit and integration tests

```

## Key Features

- **Generic Repository**: reusable CRUD methods with optional navigation includes and built‑in persistence.
- **Application Services**: encapsulate business rules (e.g., no duplicate position per recruiter, unique recruiter email/cellphone, unique department names).
- **CQRS & MediatR**: clear separation of commands and queries, easily testable.
- **FluentValidation**: robust request validation with detailed error messages.
- **Global Exception Handling**: centralized middleware mapping exceptions to HTTP responses (400, 401, 403, 404, 500).  
- **API Key Security**: custom middleware enforcing `X-API-KEY` header.  
- **EF Core In-Memory**: in-memory database for fast development and testing.  
- **Pipeline Behaviors**: `LoggingBehavior` logs request flow; `TimingBehavior` measures execution time.  
- **Modular DI**: extension methods for registering DbContext, repositories, services, MediatR, FluentValidation, Swagger, CORS, logging, and middleware.
- **Clean Architecture**: strict layering ensures maintainability and testability.

## Conventions

- Namespaces and projects follow `PositionManagement.{Layer}`.
- Entity interfaces under `Domain.Entities`, services under `Application.Services`, middleware under `Infrastructure.Middleware`.
- DI extension methods under `DependencyInjection.Extensions` in each layer.

## Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- IDE: Visual Studio 2022 / Visual Studio Code
- (Optional) SQL Server or another relational database

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/your-repo/PositionManagement.git
   cd PositionManagement/Source/Backend/PositionManagement.Api
   ```
2. Restore dependencies:
   ```bash
   dotnet restore
   ```

## Configuration

Edit `appsettings.json` in the **PositionManagement.Api** project:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=PositionManagement;Trusted_Connection=True;"
  },
  "ApiKey": {
    "Header": "X-API-KEY",
    "Key": "your-secure-api-key"
  }
}
```

> **Note**: The `ApiKey` header and value are used by the `ApiKeyMiddleware` to secure all endpoints, `ConnectionStrings:DefaultConnection` is Optional, Logging levels for console output, finally `AllowedHosts`.

## Running the Application

```bash
cd Source/Backend/PositionManagement.Api
dotnet run --launch-profile https
```

Once running, navigate to:

```
https://localhost:7257/swagger/index.html
```

Provide your `X-API-KEY` before testing endpoints.

## Data Seeding

On startup, the application automatically seeds the In-Memory database with:

- **5 Departments**
- **5 Recruiters**
- **10 Positions** (randomly assigned to departments and recruiters)

Seeding logic is implemented in:

```
Infrastructure/Data/Seed/PositionManagementDbContextSeed.cs
```

## Testing

Run all unit and integration tests:

```bash
cd Source/Backend/
dotnet test
```

## API Endpoints

All endpoints require an `X-API-KEY` header.

| Resource     | Method | URL                         | Description                          |
|--------------|--------|-----------------------------|--------------------------------------|
| Departments  | GET    | `/api/departments`          | List all departments                 |
| Departments  | POST   | `/api/departments`          | Create a new department              |
| Departments  | PUT    | `/api/departments/{id}`     | Update existing department           |
| Departments  | DELETE | `/api/departments/{id}`     | Delete department                    |
| Recruiters   | GET    | `/api/recruiters`           | List all recruiters                  |
| Recruiters   | POST   | `/api/recruiters`           | Create a new recruiter               |
| Recruiters   | PUT    | `/api/recruiters/{id}`      | Update existing recruiter            |
| Recruiters   | DELETE | `/api/recruiters/{id}`      | Delete recruiter                     |
| Positions    | GET    | `/api/positions`            | List all positions                   |
| Positions    | GET    | `/api/positions/{id}`       | Get position by GUID                 |
| Positions    | POST   | `/api/positions`            | Create a new position                |
| Positions    | PUT    | `/api/positions/{id}`       | Update existing position             |
| Positions    | DELETE | `/api/positions/{id}`       | Delete position                      |

---

© 2025 Jhan Carlos del Rio — Senior Full Stack Developer