# SurveyBasket API

A modern, enterprise-grade **Polling & Survey REST API** built with ASP.NET Core 9.0 following Clean Architecture principles. This API enables administrators to create and manage polls, collect user responses securely, and analyze results in real time.

---

## Features

### Core Functionality

- **Poll Management** - Create, update, delete, and publish polls
- **Dynamic Questions** - Support for multiple question types and configurations
- **Voting System** - Secure and validated voting for authenticated users
- **Results & Analytics** - Aggregated poll results with real-time insights
- **User Roles** - Role-based access for Admins and Users
- **Background Jobs** - Scheduled tasks and notifications using Hangfire

### Technical Features

- Clean Architecture with separation of concerns
- CQRS pattern (separation of read/write logic)
- JWT Authentication with Refresh Tokens
- FluentValidation for request validation
- Mapster for object mapping
- Serilog for structured logging
- Global exception handling
- OpenAPI & Scalar documentation

---

## Architecture

The project follows **Clean Architecture** with clear separation across layers:

```
SurveyBasket/
├── Core/
│   ├── Entities/            # Domain models and business rules
│   ├── Contracts/           # Interfaces and abstractions
│   └── Services/            # Business logic
├── Infrastructure/
│   ├── Persistence/         # Database access and EF Core
│   └── Services/            # External services (JWT, logging, etc.)
├── Application/             # Application logic, DTOs, validation
├── API/                     # Controllers and endpoints
└── SurveyBasket/            # Main API entry point
```

### Projects

| Project          | Purpose                                                 |
| ---------------- | ------------------------------------------------------- |
| `Core`           | Entities, business rules, and interfaces                |
| `Application`    | DTOs, validation, and application logic                 |
| `Infrastructure` | Database access, external services, and implementations |
| `API`            | Controllers and HTTP request handling                   |
| `SurveyBasket`   | Main application startup and configuration              |

---

## Technology Stack

### Framework & Runtime

- **ASP.NET Core 9.0**
- **.NET 9**

### Database

- **SQL Server**
- **Entity Framework Core**

### Background Processing

- **Hangfire** - Background jobs and scheduling

### Authentication & Security

- **JWT (JSON Web Tokens)** with Refresh Tokens
- **Role-Based Authorization (RBAC)**

### Logging & Monitoring

- **Serilog** - Structured logging

### Validation & Mapping

- **FluentValidation**
- **Mapster**

### API Documentation

- **OpenAPI (Swagger)**
- **Scalar UI**

---

## Prerequisites

- **.NET 9 SDK**
- **SQL Server**
- **Visual Studio 2022 / VS Code / Rider**

---

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/BadrYounis/Survey-Basket.git
cd SurveyBasket
```

### 2. Install Dependencies

```bash
dotnet restore
```

### 3. Update Connection String

Edit `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=SurveyBasketDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 4. Apply Migrations

```bash
dotnet ef database update
```

### 5. Run the Application

```bash
dotnet run --project SurveyBasket
```

The API will be available at:

```
https://localhost:7197
```

---

## Configuration

### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "SQL Server connection string"
  },
  "JwtOptions": {
    "Issuer": "Token issuer",
    "Audience": "Audience",
    "SecretKey": "Secret key",
    "ExpirationInDays": 30
  }
}
```

### Logging

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

---

## API Endpoints

### Polls

| Method | Endpoint          | Description      |
| ------ | ----------------- | ---------------- |
| GET    | `/api/polls`      | Get all polls    |
| GET    | `/api/polls/{id}` | Get poll details |
| POST   | `/api/polls`      | Create new poll  |
| PUT    | `/api/polls`      | Update poll      |
| DELETE | `/api/polls/{id}` | Delete poll      |

### Voting

| Method | Endpoint     | Description   |
| ------ | ------------ | ------------- |
| POST   | `/api/votes` | Submit a vote |

### Authentication

| Method | Endpoint             | Description   |
| ------ | -------------------- | ------------- |
| POST   | `/api/auth/login`    | User login    |
| POST   | `/api/auth/register` | Register user |

---

## Authentication

The API uses **JWT authentication**:

1. Login/Register to get a token
2. Include token in headers:

```
Authorization: Bearer <your_token>
```

---

## Database Schema

### Main Entities

- **Poll**
- **Question**
- **Answer**
- **Vote**
- **User**
- **Role**

---

## Background Jobs

Using Hangfire:

- Scheduled notifications
- Recurring jobs (e.g., daily reminders)

Dashboard:

```
/jobs
```

---

## Middleware Pipeline

1. Global Exception Handling
2. HTTPS Redirection
3. Authentication
4. Authorization
5. Logging (Serilog)
6. Swagger / Scalar UI

---

## Database Seeding

The database can be seeded on startup:

```csharp
await app.SeedDatabaseAsync();
```

---

## Troubleshooting

### Database Issues

- Ensure SQL Server is running
- Verify connection string

### Migration Issues

```bash
dotnet ef migrations list
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## Contributing

1. Create a branch
2. Commit changes
3. Push to repository
4. Open Pull Request

---

## Author

**Badr Younis**
GitHub: [https://github.com/BadrYounis](https://github.com/BadrYounis)
