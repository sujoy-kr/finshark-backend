# 📦 ASP.NET Core Web API

A **.NET 9 Web API** project following Clean Architecture principles. Built with **EF Core**, **ASP.NET Core Identity**, **JWT authentication**, and **Swagger**.

---

## 🚀 Features

-   Built with **.NET 9**
-   **ASP.NET Core Identity** for user and role management
-   **JWT-based Authentication** integrated with Identity
-   Clean Architecture (Services, Repositories, DTOs, Interfaces)
-   Auto-generated API docs via **Swagger**
-   **EF Core** with Identity DB context
-   RESTful API endpoints

---

## 🧱 Project Structure

```plaintext
│
├── Controllers/                      # API Controllers (entry points)
│   ├── AccountController.cs
│   ├── CommentController.cs
│   ├── PortfolioController.cs
│   └── StockController.cs
│
├── Data/                             # EF Core DB context
│   └── ApplicationDBContext.cs
│
├── Dtos/                             # Data Transfer Objects
│   ├── Account/
│   │   ├── LoginDto.cs
│   │   ├── LoginResponseDto.cs
│   │   ├── NewUserDto.cs
│   │   └── RegisterDto.cs
│   ├── Comment/
│   │   ├── CommentDto.cs
│   │   ├── CreateCommentRequestDto.cs
│   │   └── UpdateCommentRequestDto.cs
│   └── Stock/
│       ├── CreateStockRequestDto.cs
│       ├── StockDto.cs
│       ├── UpdateStockRequestDto.cs
│       └── UpdateStockResponseDto.cs
│
├── Extensions/                       # Extension methods
│   └── ClaimsExtensions.cs
│
├── Helpers/                          # Query helpers, utilities
│   └── StockQueryObject.cs
│
├── Interfaces/                       # Service and repository interfaces
│   ├── ICommentRepository.cs
│   ├── IPortfolioRepository.cs
│   ├── IStockRepository.cs
│   └── ITokenService.cs
│
├── Mappers/                          # Manual or AutoMapper profile mappings
│   ├── CommentMapper.cs
│   └── StockMapper.cs
│
├── Models/                           # Domain models / EF Core entities
│   ├── AppUser.cs
│   ├── Comment.cs
│   ├── Portfolio.cs
│   └── Stock.cs
│
├── Repository/                       # Repository implementations
│   ├── CommentRepository.cs
│   ├── PortfolioRepository.cs
│   └── StockRepository.cs
│
├── Service/                          # Business logic and services
│   └── TokenService.cs
│
├── Properties/                       # Project-level configs (launch settings)
│   └── launchSettings.json
│
├── appsettings.json                  # Configuration settings
├── Program.cs                        # Application entry point
├── api.http                          # HTTP request samples
├── api.csproj                        # Project file
└── api.sln                           # Solution file

```

---

## ⚙️ Setup & Run

### ✅ Prerequisites

-   .NET 9 SDK
-   SQL Server

### 🔧 Installation

```bash
# Clone the repo
git clone https://github.com/sujoy-kr/finshark.git
cd finshark
```

### ▶️ Run the Project

```bash
dotnet run
```

The API will be available at:
🌐 `http://localhost:5125`

Swagger UI:
📄 `http://localhost:5125/swagger/index.html`

---

## 🛠️ Configuration

Update your `appsettings.json` with the proper connection string and JWT settings:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=YOUR_SERVER_NAME;
    Initial Catalog=YOUR_DB_NAME;
    Integrated Security=True;
    Connect Timeout=30;
    Encrypt=False;
    TrustServerCertificate=False;
    ApplicationIntent=ReadWrite;
    MultiSubnetFailover=False"
  },
  ...
  "AllowedHosts": "*",
  "JWT": {
    "Issuer": "http://localhost:5125",
    "Audience": "http://localhost:5125",
    "SigningKey": "your-super-secret-jwt-key (must be long)"
  }
}

```

---

## 🗃️ Entity Framework Core

### 🔨 Migrations & DB Updates

```bash
# Create initial migration
dotnet ef migrations add InitialCreate

# Apply migration to the database
dotnet ef database update
```

---

## ✨ Technology Stack

-   **ASP.NET Core 9**
-   **Entity Framework Core** with **Identity**
-   **SQL Server**
-   **JWT Bearer Authentication** integrated with Identity
-   **Swagger (NSwag & Swashbuckle)**
-   **AutoMapper-style custom mappers**
-   **RESTful APIs**
