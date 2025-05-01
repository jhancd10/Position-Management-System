# PositionManagement

## Project Overview

**PositionManagement** is a Clean Architecture solution built with .NET 8 that provides a modular, maintainable system for managing job positions with full CRUD operations. It incorporates:

- **Generic Repository Pattern** with EF Core for reusable data access, including optional includes on `GetByIdAsync`.
- **Application Services** for business logic and validations (e.g., uniqueness constraints).
- **CQRS** via MediatR for commands and queries.
- **FluentValidation** for request validation.
- **EF Core In-Memory** provider for persistence in development and testing.
- **Global Exception Handling** and **API Key Security** via custom middleware.
- **Pipeline Behaviors** for logging and performance metrics.
- **Dependency Injection Extensions** for modular service registration.

## Repository Structure

```
Backend/                            ⬅ Root folder
│
├── PositionManagement.sln         ⬅ .NET solution file
│
├── Source/                        ⬅ Application source code
│   ├── PositionManagement.Api/         ⬅ Web API project
│   │   • Controllers and endpoints
│   │   • Startup orchestration via DefaultWebApplication
│   │
│   ├── PositionManagement.Application/ ⬅ Application layer
│   │   • Interfaces (IBaseRepository, IPositionService, IRecruiterService, IDepartmentService)
│   │   • Implementations (GenericRepository, BaseRepository)
│   │   • Application Services with business validations
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
│       • Shared exceptions and helpers (NotFoundException, ExceptionHelper)
│       • DI extensions for WebApi, CORS, Logging
│
└── Tests/                         ⬅ Unit and integration tests

```

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed
- An IDE or editor (Visual Studio, VS Code, etc.)

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

## Configuration

- **appsettings.json**:
  - `ApiKey` section with `Header` and `Key` values.
  - Logging levels for console output.
  - `AllowedHosts`.

- **Automatic GUIDs**: primary keys generated via `Guid.NewGuid()`.
- **String Constraints**: max length enforced in `OnModelCreating` of `DbContext`.
- **Seed Data**: optionally add seed entries via `modelBuilder.Entity<...>().HasData(...)`.

## Conventions

- Namespaces and projects follow `PositionManagement.{Layer}`.
- Entity interfaces under `Domain.Entities`, repository interfaces under `Application.Repositories`, services under `Application.Services`, middleware under `Infrastructure.Middleware`.
- DI extension methods under `DependencyInjection.Extensions` in each layer.

---

© 2025 Jhan Carlos del Rio — Senior Full Stack Developer
