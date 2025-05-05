# Frontend: PositionManagement

## Project Overview

**PositionManagement-Frontend** is a modern **Angular 19** application complementing the backend. It enables full CRUD management of **Departments**, **Recruiters**, and **Positions** with a clean, reactive UI.

Key technologies and patterns:

- **Standalone Components** in Angular 19 for self-contained modules.
- **Native Signals** for reactive state management.
- **ngExtensions (`signalSlice`)** to model loading, success, and error slices.
- **Angular Material** for a consistent UI (toolbar, sidenav, cards, grid-list, snack-bar, etc.).
- **Global HTTP Interceptor** with a **SnackBar queue** (`UIService`) to handle errors.
- **`BaseHttpService`** for generic `get<T>`, `post<T>`, `put<T>`, `delete<T>` with built-in `catchError`.
- **Modular folder structure** by feature: `home`, `departments`, `recruiters`, `positions`.

## Repository Structure

```
Frontend/
└── src/
    ├── app/
    │   ├── app.component.html        # Main layout (header + sidenav + outlet)
    │   ├── app.component.scss        # Component-specific styles
    │   ├── app.component.ts          # Root component logic
    │   ├── app.component.spec.ts     # Root component tests
    │   ├── app.config.ts             # Standalone providers (HTTP, Router, Zone)
    │   ├── app.routes.ts             # Route definitions
    │   ├── departments/              # Feature: Departments
    │   │   ├── services/             # HTTP service & state service
    │   │   ├── interfaces/           # Dept interfaces
    │   ├── recruiters/               # Feature: Recruiters (same structure)
    │   │   ├── interfaces/
    │   │   └── services/
    │   ├── positions/                # Feature: Positions (same structure)
    │   │   ├── enums/                # StateStatusEnum
    │   │   ├── interfaces/
    │   │   └── services/
    │   ├── home/                     # Feature: Home (Dashboard)
    │   │   ├── features/             # Routed components (e.g. detail)
    │   │   ├── interfaces/           # Category config interface
    │   │   ├── services/             
    │   │   └── ui/                   # Reusable UI (CategoryCard)
    │   ├── shared/                   # Reusable modules, types, services
    │   │   ├── enums/                # Global enums
    │   │   ├── interceptors/         # Error interceptor
    │   │   ├── interfaces/           # Shared interfaces (State, SnackEntry)
    │   │   ├── services/             # BaseHttpService, UIService, SidenavService
    │   │   └── ui/                   # Header, SideNav components
    ├── environments/                 # Default environment
    │   ├── environment.ts            
    │   └── environment.development.ts
    ├── main.ts                       # Bootstraps the app
    ├── index.html                    # Main index
    └── styles.scss                   # Global styles and theme

```

## Prerequisites

- **Node.js** v22+ and **npm**, **yarn**, **bun**, etc.  installed
- **Angular CLI** v19

## Key Features

- **Standalone Components**: each declares its Angular Material imports.
- **Reactive State** with native **Signals** and `signalSlice`.
- **Global Error Handling**: HTTP interceptor + queued SnackBars.
- **`BaseHttpService`**: encapsulates HTTP calls with error handling.
- **Feature Modules**: clear separation by domain.
- **Responsive Dashboard**: dynamic grid layout.

## Configuration

- **`app.config.ts`**:
  - `provideHttpClient(withInterceptors([errorInterceptor]))`
  - `provideRouter(routes)`
- **`environment.ts`**: set `API_URL` for your backend.

## Conventions

- **Standalone Components** (`standalone: true`) with explicit imports.
- **Feature Folders**: `data-access/`, `interfaces/`, `services/`, `ui/` per domain.
- **Core & Shared**: global interceptors, services, UI pieces.
- **SCSS**: global variables and theming in `styles.scss`.
- **State Management**: Signals + `signalSlice` for loading/success/error.

---

© 2025 Jhan Carlos del Rio — Senior Full Stack Developer