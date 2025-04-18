[![Build Status](https://github.com/rafaelvictal/InventoryControl/actions/workflows/dotnet.yml/badge.svg)](https://github.com/rafaelvictal/InventoryControl/actions) [![Latest Release](https://img.shields.io/github/v/release/rafaelvictal/InventoryControl)](https://github.com/rafaelvictal/InventoryControl/releases)

# Inventory Control System

A simple inventory control system.  
Includes a .NET 8 backend with SQLite and a React frontend using Material UI.

---

## 📦 Technologies Used

- ASP.NET Core 8 (Web API)
- Entity Framework Core + SQLite
- React 18 (SPA)
- Material UI
- Axios
- React Hook Form + Yup
- Docker + Docker Compose

---

## ▶️ How to Run with Docker Compose

Prerequisites: Docker and Docker Compose installed.

- Navigate to the project root folder (where the `docker-compose.yml` file is located).
- Run a Docker Compose:
  
```bash
docker-compose up --build
```

### Access URLs:

🔹 API (Swagger): [http://localhost:5000/swagger](http://localhost:5000/swagger)
🔹 Frontend (SPA): [http://localhost:5173](http://localhost:5173)

---

## ✅ Features

- Register stock movements
  - Validates if the product exists
  - Prevents negative stock
- Stock report by date
  - Optional filter by product code
- User feedback via `Snackbar`
- Unit and integration tests

---

## 📂 Folder Structure

```
InventoryControl/
├── BackEnd/
│   ├── InventoryControl.API/       # Web API
│   ├── InventoryControl.Domain/    # Entities, enums
│   ├── InventoryControl.Infra/     # DbContext, Seeder
│   ├── InventoryControl.Service/   # Business logic
│   ├── InventoryControl.Test/      # Tests (xUnit)
│   └── Dockerfile                  # API Docker build
├── FrontEnd/
│   ├── Dockerfile                  # Frontend Docker build
│   └── src/                        # React SPA
├── docker-compose.yml              # Orchestration
```

---

## 🛠️ Running Tests

```bash
cd BackEnd/InventoryControl.Test
dotnet test
```
