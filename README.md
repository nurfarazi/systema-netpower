# NetPower

Full-stack app with .NET backend and Angular frontend.

Video: https://youtu.be/6Q6SHs4QKsE

## Requirements

- .NET 8.0 SDK
- SQL Server (LocalDB works)
- Node.js 18 or higher

## Getting Started

### Backend

```bash
cd NetPower/src/NetPower.API
dotnet restore
dotnet ef database update
dotnet run
```

API runs at https://localhost:7115 (API docs at https://localhost:7115/scalar)

### Frontend

```bash
cd client
npm install
npm start
```

App runs at http://localhost:4200

For SSR (server-side rendering):
```bash
npm run serve:ssr:client
```

## Database Setup

The app uses LocalDB by default. Connection string is in `NetPower/src/NetPower.API/appsettings.json`:

```json
"Server=(localdb)\\mssqllocaldb;Database=NetPowerDb;Trusted_Connection=true"
```

Change this if you're using a different SQL Server.

If you don't have EF tools:
```bash
dotnet tool install --global dotnet-ef
```

## Project Structure

```
NetPower/
  src/
    NetPower.API           - Controllers, middleware
    NetPower.Application   - Business logic
    NetPower.Domain        - Entities, interfaces
    NetPower.Infrastructure - Database
  tests/                   - Unit and integration tests

client/
  src/app/
    home/                  - Home component
    user-list/             - User list
    services/              - API services
    models/                - TypeScript interfaces
```

## Running Tests

Backend:
```bash
cd NetPower
dotnet test
```

Frontend:
```bash
cd client
npm test
```

## Common Issues

**Database connection error?**
Check LocalDB is installed or update appsettings.json

**Port in use?**
Backend: Edit Properties/launchSettings.json  
Frontend: Run `ng serve --port 4300`

**npm fails?**
Try: `npm cache clean --force` then delete node_modules and reinstall

## Development

Hot reload:
```bash
# Backend
dotnet watch run

# Frontend already auto-reloads with npm start
```

Frontend environment files: `client/src/environments/`
