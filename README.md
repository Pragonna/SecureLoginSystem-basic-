
# Application.Solution

This is a .NET Core-based solution with a layered architecture implementing asynchronous communication principles and event-driven messaging using RabbitMQ.

---

## 📦 Project Structure

- **Web.Api** – Entry point for the REST API
- **Core.Domain** – Domain models and interfaces
- **Core.Application** – Business logic and service contracts
- **Core.Infrastructure** – Implementation of infrastructure dependencies (e.g., DB, SMTP, etc.)
- **EmailSender.Service** – Background service for sending emails
- **EventBus**, **EventBus.Factory**, **EventBus.RabbitMQ** – Custom implementation of event bus over RabbitMQ

---

## 🚀 Getting Started

### Prerequisites
- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [RabbitMQ](https://www.rabbitmq.com/download.html)
- SQL Server (or other DB if configured)

### Run the API
```bash
cd src/Web.Api
dotnet run
```

---

## 🔐 OTP Login Architecture

This project implements a basic architecture for OTP-based login. The user simply inputs their email address, an OTP code is sent to the provided email, and the correctness of the entered OTP is validated. Upon successful validation, an access token and a refresh token are returned to the user. The behavior of these tokens (e.g., expiration and refresh mechanism) is also demonstrated within the system.

---

## 🔧 Configuration

App settings can be adjusted in:

- `Web.Api/appsettings.json`
- `Web.Api/appsettings.Development.json`

Make sure RabbitMQ and your SMTP/Email settings are properly configured.

---

## 📬 Event Bus

Custom implementation of a distributed event bus pattern using RabbitMQ:
- `Subscribe<TEvent, THandler>()`
- `Publish<TEvent>()`

Used for services like email notification, logging, or further decoupled systems.

---

## ✉️ Email Sender

The `EmailSender.Service` is a hosted worker service which:
- Listens to events (e.g., OTP generated)
- Sends corresponding email via SMTP

---

## 🧪 Testing

Tests not found in the root structure, but you can add test projects alongside the solution:
```bash
dotnet new xunit -n ProjectName.Tests
dotnet add reference ../YourProject.csproj
```

---

## 🛠 Technologies Used

- ASP.NET Core Web API
- Entity Framework Core
- RabbitMQ
- SMTP (Email)
- DDD / Clean Architecture principles

---

## 📂 How to Extend

- Add new event types in `EventBus`
- Implement new integrations in `Infrastructure`
- Register new services in `Program.cs` and `DependencyInjection`

---

## 📬 Contact

Feel free to contribute or raise issues!
