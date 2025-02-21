# VotingSystem API - Local Development Guide

This guide will help you set up and run the VotingSystem API project on your local machine.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- [Docker](https://www.docker.com/products/docker-desktop/)
- [Docker Compose](https://docs.docker.com/compose/install/) (included with Docker Desktop)

## Technologies used on this project:
- RabbitMq
- .NET 8
- SQL Server
- Docker

## Setup Instructions

### 1. Clone the Repository (if you haven't already)

```bash
git clone https://github.com/Viilih/VSA-voting-system.git
cd VSA-voting-system
```

### 2. Restore .NET Packages

```bash
cd votingSystem.Api
dotnet restore
```

### 3. Start Required Services with Docker Compose

Navigate back to the root directory and start the required services:

```bash
cd ..
docker compose up -d
```

This will start:
- SQL Server database
- RabbitMQ message broker

### 4. Configure Application Settings (if needed)

Check `appsettings.json` and `appsettings.Development.json` in the VotingSystem.Api project directory to ensure connection strings match your Docker service configurations.

### 5. Run the API

```bash
cd votingSystem.Api
dotnet run
```

By default, the API should be accessible at: `https://localhost:7293`

## Verifying the Setup

1. The API should display a Swagger UI at: `https://localhost:7293/swagger/index.html`
2. You can test RabbitMQ connectivity at: `http://localhost:15672` (default credentials: guest/guest)


## Stopping the Application

1. Stop the .NET application with `Ctrl+C`
2. Stop and remove Docker containers:
   ```bash
   docker compose down
   ```
