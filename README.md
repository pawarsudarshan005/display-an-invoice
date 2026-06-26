# Invoice App

A small ASP.NET Core (.NET 8) web app that displays an invoice. The backend
exposes a JSON API (documented with Swagger), data is stored in **SQLite** via
Entity Framework Core, and a static HTML/CSS/JS front-end renders the invoice.

This repository started as a deliberately broken codebase. All bugs have been
fixed (see [Fixes applied](#fixes-applied)) without rewriting the project.

---

## Run locally

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
  (on Windows: `winget install Microsoft.DotNet.SDK.8`)

### Commands
```bash
# from the project root
dotnet restore
dotnet run
```

The app prints the URL it is listening on (e.g. `http://localhost:5000`).
The SQLite database (`invoice.db`) is created and seeded automatically on first run.

To run on a specific port:
```bash
# bash / PowerShell
dotnet run --urls "http://localhost:5099"
```

### URLs
| What | URL |
|------|-----|
| **UI** (invoice) | `http://localhost:5099/` |
| **API** – invoice items | `http://localhost:5099/api/invoice` |
| **API** – data sample | `http://localhost:5099/api/data` |
| **Swagger docs** | `http://localhost:5099/swagger` |

---

## Project structure
```
InvoiceApp.csproj          # project + NuGet packages
Program.cs                 # startup: EF Core, Swagger, static files, CORS
Controllers/
  InvoiceController.cs      # GET /api/invoice  -> invoice items from DB
  APIController.cs          # GET /api/data     -> sample data
Data/AppDbContext.cs       # EF Core context + seed data
Models/                    # Invoice, InvoiceItem entities
wwwroot/                   # index.html, script.js, styles.css (front-end)
init.sql                   # reference SQL schema (same data EF seeds)
Dockerfile                 # container build for deployment
```

---

## Fixes applied

**Backend — `Controllers/APIController.cs`**
- `string result = null; result.Length` → guaranteed `NullReferenceException`.
  Now returns real data and null-checks safely.

**Backend — `Controllers/InvoiceController.cs`**
- `List<Item> items = null; items.Count` → `NullReferenceException`.
  Now loads invoice items from the database via EF Core.

**Front-end — `wwwroot/index.html`**
- `<title>Invoice Viewer<title>` → unclosed tag, fixed to `</title>`.
  Added `<meta charset>` / viewport.

**Front-end — `wwwroot/script.js`**
- `DOMContentLoade` → `DOMContentLoaded`
- `fetc(...)` → `fetch(...)`
- `resp.jsoon()` → `resp.json()`
- `item.prce` → `item.price`
- `console.eror` → `console.error`
- Added HTTP error handling + invoice total.

**Front-end — `wwwroot/styles.css`**
- `background-colour` → `background-color`
- `font-famly` → `font-family`
- `paddding` → `padding`
- `margn` → `margin`
- `cetner` → `center`

**Database — `init.sql`**
- `CREATE TAABLE` → `CREATE TABLE`
- `DECIML(10,2)` → `DECIMAL(10,2)`
- `REFRENCES` → `REFERENCES`

**Added to make it run** (scaffolding the original lacked): `InvoiceApp.csproj`,
`Program.cs`, EF Core + SQLite, Swagger, and the `wwwroot` static-file pipeline.

---

## Deploy

The included `Dockerfile` builds a production image and honours the `PORT`
environment variable that hosts like Render/Railway inject.

```bash
docker build -t invoice-app .
docker run -p 8080:8080 invoice-app
# open http://localhost:8080  and  http://localhost:8080/swagger
```
