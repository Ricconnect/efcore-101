# EF Core 101

A Blazor Web App for managing medicines and intake schedules, built as the starting point for the **EF Core 101** workshop.

The app is fully functional out of the box using a file-based JSON repository. The workshop goal is to replace it with Entity Framework Core backed by a real SQL Server database.

---

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (for the SQL Server instance)

---

## Getting started

### 1. Start the database

Copy the environment template and start SQL Server:

```bash
cp .env.example .env
docker compose up -d
```

The container exposes SQL Server on **localhost:1433**. The `data/` volume persists across restarts.

> **Note:** You can change the SA password in `.env` — just make sure to update the connection string in `appsettings.Development.json` to match.

### 2. Run the app

```bash
cd MedicineManager/MedicineManager.App
dotnet run
```

The app opens at `http://localhost:5000` (or the port shown in the terminal).

---

## Project structure

```
MedicineManager/
└── MedicineManager.App/
    ├── Data/
    │   ├── Models/
    │   │   ├── IEntity.cs              # Guid Id contract
    │   │   ├── Medicine.cs
    │   │   └── IntakeSchedule.cs       # Has MedicineId FK (no nav property yet)
    │   └── Repositories/
    │       ├── IRepository.cs          # Generic CRUD interface
    │       └── JsonFileRepository.cs   # File-system implementation (pre-EF Core)
    └── Components/Pages/
        ├── Medicines/                  # Index, Create, Edit
        └── Schedules/                  # Index, Create, Edit
```

---

## Workshop: introducing EF Core

The app is intentionally wired to `JsonFileRepository<T>`. Your tasks:

1. Add the EF Core NuGet packages (`Microsoft.EntityFrameworkCore.SqlServer`, `Microsoft.EntityFrameworkCore.Tools`)
2. Create a `DbContext` with `DbSet<Medicine>` and `DbSet<IntakeSchedule>`
3. Configure the `MedicineId` FK as a navigation property on `IntakeSchedule`
4. Implement `IRepository<T>` using the `DbContext`
5. Swap the DI registration in `Program.cs` — the pages need zero changes

The connection string is already waiting in `appsettings.Development.json`.
