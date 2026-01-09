# SurveyBasket 🗳️

[![.NET](https://img.shields.io/badge/.NET-9.0-512bd4?style=flat&logo=dotnet)](https://dotnet.microsoft.com/)
[![Status](https://img.shields.io/badge/Status-Active-success?style=flat)]()
[![License](https://img.shields.io/badge/License-MIT-blue?style=flat)](LICENSE)

**SurveyBasket** is a robust, full-featured polling and survey application built with modern .NET technologies. It validates user feedback through a secure, scalable, and clean architecture, enabling administrators to create polls, manage questions, and analyze real-time results.

---

## ✨ Features

### 🔐 Authentication & Authorization
- **Robust Security**: Implements JWT (JSON Web Token) authentication with Refresh Token support.
- **Role-Based Access Control (RBAC)**: Granular permissions for Administrators and Users.
- **Secure Handling**: Protected endpoints ensuring data integrity and user privacy.

### 📝 Poll & Survey Management
- **CRUD Operations**: Comprehensive creation, reading, updating, and deletion of polls.
- **Dynamic Questions**: Support for various question types and configurations.
- **Publishing Workflow**: Toggle poll visibility and manage active/inactive states.

### 📊 Voting & Analytics
- **Real-time Voting**: Seamless voting experience for authenticated members.
- **Vote Validation**: Logic to prevent duplicate votes and ensure data accuracy.
- **Results Analysis**: Aggregated views of poll results for quick insights.

### ⚙️ Background Processing
- **Hangfire Integration**: Reliable background job processing for notifications and scheduled tasks.
- **Cron Jobs**: Automated recurring tasks (e.g., daily notifications).

### 🛠️ Architecture & Best Practices
- **Clean Architecture**: Separation of concerns (API, Contracts, Infrastructure, Application, Core).
- **CQRS Pattern**: (Implied by structure) Clean separation of read and write operations.
- **Validation**: FluentValidation for strong, type-safe rules.
- **Mapping**: Mapster for high-performance object mapping.
- **Logging**: Serilog for structured logging and diagnostics.

---

## 🚀 Technology Stack

- **Framework**: [.NET 9 Web API](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- **Database**: [SQL Server](https://www.microsoft.com/en-us/sql-server) with [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- **Documentation**: [OpenAPI](https://www.openapis.org/) & [Scalar](https://github.com/scalar/scalar) for beautiful API references
- **Background Jobs**: [Hangfire](https://www.hangfire.io/)
- **Validation**: [FluentValidation](https://docs.fluentvalidation.net/)
- **Logging**: [Serilog](https://serilog.net/)
- **Object Mapping**: [Mapster](https://github.com/MapsterMapper/Mapster)

---

## 🛠️ Getting Started

Follow these steps to set up the project locally.

### Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express or Developer)
- Your favorite IDE (Visual Studio 2022, VS Code, or Rider)

### Installation

1.  **Clone the repository**
    ```bash
    git clone https://github.com/badryounis14/SurveyBasket.git
    cd SurveyBasket
    ```

2.  **Configure Database**
    Update your connection string in `appsettings.json` or use User Secrets (recommended).
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=YOUR_SERVER;Database=SurveyBasketDb;Trusted_Connection=True;TrustServerCertificate=True;"
    }
    ```

3.  **Apply Migrations**
    Navigate to the project directory and run:
    ```bash
    dotnet ef database update
    ```

4.  **Run the Application**
    ```bash
    dotnet run --project SurveyBasket
    ```

5.  **Explore the API**
    Open your browser and navigate to:
    - **Scalar UI**: `https://localhost:7197/scalar/v1` (Check launch logs for exact port)
    - **Health Checks**: `https://localhost:7197/health`
    - **Hangfire Dashboard**: `https://localhost:7197/jobs`

---

## 👤 Author

**Badr Younis**

- GitHub: [@badryounis14](https://github.com/badryounis14)

---

Copyright © 2026 Badr Younis. All Rights Reserved.
