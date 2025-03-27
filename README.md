# iStoq Backend – ASP.NET Core (C#)

API RESTful para o sistema de controle de estoque **iStoq**, desenvolvida com ASP.NET Core.

---

## 🚀 Funcionalidades

- CRUD de produtos
- Cadastro de categorias
- Registro de fornecedores
- Movimentações de estoque (entrada/saída)
- Integração com banco relacional (PostgreSQL ou SQL Server)
- Documentação Swagger/OpenAPI

---

## 🧱 Estrutura do Projeto

```
/iStoq-backend-csharp
├── Domain/
├── Application/
├── Infrastructure/
├── API/
└── Tests/
```

---

## 🔧 Tecnologias

- .NET 8+
- Entity Framework Core
- AutoMapper
- Swagger
- PostgreSQL / SQL Server

---

## 🚀 Como executar localmente

```bash
dotnet restore
dotnet build
dotnet run --project API
```

---