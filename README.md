# Inventory Control System

Sistema de controle de estoque.
Inclui backend em .NET 8 com SQLite e frontend em React + Material UI.

---

## 📦 Tecnologias Utilizadas

- ASP.NET Core 8 (Web API)
- Entity Framework Core + SQLite
- React 18 (SPA)
- Material UI
- Axios
- React Hook Form + Yup
- Docker + Docker Compose

---

## ▶️ Como Rodar com Docker Compose

Pré-requisitos: Docker e Docker Compose instalados.

```bash
docker-compose up --build
```

### Acessos:

- 🔹 API (Swagger): [http://localhost:5000/swagger](http://localhost:5000/swagger)
- 🔹 Frontend (SPA): [http://localhost:5173](http://localhost:5173)

---

## ✅ Funcionalidades

- Cadastro de movimentação de estoque
  - Valida se produto existe
  - Impede estoque negativo
- Relatório de estoque por data
  - Filtro por código de produto opcional
- Mensagens de feedback via `Snackbar`
- Testes unitários e de integração

---

## 📂 Estrutura de Pastas

```
InventoryControl/
├── BackEnd/
│   ├── InventoryControl.API/       # Web API
│   ├── InventoryControl.Domain/    # Entidades, enums
│   ├── InventoryControl.Infra/     # DbContext, Seed
│   ├── InventoryControl.Service/   # Lógica de negócio
│   └── InventoryControl.Test/      # Testes (xUnit)
│   └── Dockerfile                  # Build da API
├── FrontEnd/
│   ├── Dockerfile                  # Build do frontend
│   └── src/                        # React SPA
├── docker-compose.yml              # Orquestra tudo
```

---

## 🛠️ Como Rodar os Testes

```bash
cd BackEnd/InventoryControl.Test
dotnet test
```
