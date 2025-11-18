# TODO List Application --- Angular 20 & .NET 10 (Clean Architecture)

This project is a simple TODO application built for a pre-interview
technical assignment.\
It demonstrates clean architecture, SOLID principles, unit testing, and
modern Angular development.\
Users can view their TODO items, add new tasks, delete tasks, and toggle
their completion status.

------------------------------------------------------------------------

## 🚀 Tech Stack

### **Frontend**

-   Angular **20.3**
-   TypeScript
-   CSS (no UI frameworks)
-   Angular HttpClient for API communication

### **Backend**

-   .NET **10** Web API
-   Clean Architecture (Domain, Application, Infrastructure, Api)
-   In-memory repository (no database)
-   Serilog for structured logging
-   CORS enabled
-   Unit tests with **xUnit** and **Moq**

------------------------------------------------------------------------

## 📁 Project Structure

    Jl.Todo/
    │
    ├── backend/
    │   ├── Api/
    │   │   ├── Controllers/
    │   │   └── Middleware/
    │   │
    │   ├── Api.Tests/
    │   │
    │   ├── Application/
    │   │   ├── DTOs/
    │   │   └── Interfaces/
    │   │
    │   ├── Domain/
    │   │   └── Entities/
    │   │
    │   └── Infrastructure/
    │       └── Persistence/
    │
    └── frontend/
        └── todo-frontend/
            ├── src/
                ├── app/
                │   ├── core/
                │   │   └── configs/
                │   ├── features/
                │   │   └── todo/
                │   │       ├── components/
                │   │       │   └── todo-list/
                │   │       ├── models/
                │   │       └── services/
                │   └── shared/
                │       ├── components/
                │       ├── pipes/
                │       └── directives/
                ├── assets/
                └── environments/

------------------------------------------------------------------------

## 🌐 Backend API Overview

### **Base URL**

    http://localhost:5000/api/todos

### **Endpoints**

  --------------------------------------------------------------------------
  Method   Endpoint                   Description
  -------- -------------------------- --------------------------------------
  GET      `/api/todos`               Retrieve all TODO items

  POST     `/api/todos`               Create a new TODO item

  DELETE   `/api/todos/{id}`          Delete a TODO item

  PATCH   `/api/todos/{id}/toggle`     Toggle completion status of TODO item

  --------------------------------------------------------------------------

### **Backend Features**

-   Clean Architecture separation
-   Dependency injection for repository and services
-   Custom middleware for error and validation handling
-   Serilog logging pipeline
-   xUnit + Moq unit testing setup

------------------------------------------------------------------------

## 🎨 Frontend Overview

-   Angular **20.3**
-   A modular structure using feature-based folders
-   `todo-list` component handles listing, adding, toggling, and
    deleting tasks
-   A dedicated service manages communication with the API
-   Clean and simple CSS styling

------------------------------------------------------------------------

## ▶️ Getting Started

### **1. Backend Setup**

From the `/backend/Api` folder:

``` bash
dotnet restore
dotnet run
```

API will be available at:

    http://localhost:5000

------------------------------------------------------------------------

### **2. Frontend Setup**

From the `/frontend/todo-frontend` folder:

``` bash
npm install
npm start
```

Frontend will run at:

    http://localhost:4200

CORS is already configured on the backend to allow communication between
ports.

------------------------------------------------------------------------

## 🧪 Testing

### **Backend Tests**

Run tests from `/backend/Api.Tests`:

``` bash
dotnet test
```

Includes: - xUnit test suite\
- Moq-based mock repository tests\
- Service-level behavioral tests


------------------------------------------------------------------------

## ✨ Completed Requirements

-   View list of TODOs\
-   Add a new TODO\
-   Delete a TODO\
-   Toggle completion state\
-   In-memory backend (per assignment requirement)\
-   Clean Architecture with DI\
-   Validation, custom middleware, and logging\
-   Frontend & backend fully integrated

------------------------------------------------------------------------

## 📌 Notes

-   No database is required. Data resets on each backend restart.\
-   Only standard commands are needed: `npm install` and
    `dotnet restore`.

