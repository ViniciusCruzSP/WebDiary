# DiaryApp

Projeto de estudo com arquitetura separada entre Frontend (ASP.NET MVC) e Backend (ASP.NET Web API).

---

## 🧱 Arquitetura

```
Frontend (MVC)
  └── Consome API via HTTP
Backend (Web API)
  └── Responsável pelo banco de dados e regras de negócio
```

---

## ⚙️ Tecnologias

- .NET 9
- ASP.NET Core MVC
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server / LocalDB
- Swagger

---

## 📋 Pré-requisitos

- .NET 9 SDK
- SQL Server LocalDB **ou** SQL Server
- Git

---

## 🚀 Como rodar o projeto localmente

### 1️⃣ Clonar o repositório

```bash
git clone <URL_DO_REPOSITORIO>
cd <PASTA_DO_REPOSITORIO>
```

---

### 2️⃣ Configurar o banco de dados (API)

Abra o arquivo:

```
BackEnd/WebDiaryAPI/appsettings.json
```

Verifique a connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=Diary;Trusted_Connection=True;"
}
```

Ajuste se necessário.

---

### 3️⃣ Criar o banco de dados (EF Core Migrations)

No terminal, vá até a pasta da API:

```bash
cd BackEnd/WebDiaryAPI
```

Execute:

```bash
dotnet ef database update
```

Isso irá:
- criar o banco
- criar as tabelas
- aplicar o schema inicial

---

### 4️⃣ Rodar a API

Ainda na pasta da API:

```bash
dotnet run
```

A API ficará disponível (ver URL no console), por exemplo:

```
https://localhost:7001/swagger
```

---

### 5️⃣ Rodar o Frontend (MVC)

Em **outro terminal**, vá até:

```bash
cd Frontend/DiaryApp
dotnet run
```

Acesse no navegador a URL exibida no console.

---

## ✅ Como validar que está funcionando

- Acesse o Frontend
- Crie um diário
- Liste os registros
- Edite e delete

Se tudo funcionar, a integração MVC → API está correta.

---

## 📌 Observações importantes

- O Frontend depende da API estar rodando
- O banco **não é versionado**
- As migrations ficam **somente no Backend**
