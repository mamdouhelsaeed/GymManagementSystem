# 🏋 Gym Management System (ASP.NET Core MVC)

Final assessment project for the MVC course. Trainers can browse/filter classes and see
enrolled members; Admins manage Trainers, Classes, Members, and enroll members into classes.

## Tech stack
- ASP.NET Core MVC (.NET 8)
- Entity Framework Core (Code First) + SQL Server
- Repository Pattern (controllers depend on interfaces, not DbContext)
- Session-based login (Admin vs Trainer)
- AJAX + Partial Views (trainer filter on the Classes page)
- SweetAlert2 delete confirmations
- Bootstrap 5 UI

## Project structure
```
Controllers/     Account, Classes (trainer-facing), Trainers/GymClasses/Members/Enrollments (admin)
Models/          Trainer, GymClass, Member, Enrollment, User
ViewModels/      LoginViewModel, ClassesIndexViewModel, ClassDetailsViewModel, EnrollMemberViewModel
Data/            ApplicationDbContext, DbInitializer (seed data)
Repositories/    Interfaces/ + concrete implementations (Repository Pattern)
Filters/         AdminOnlyAttribute / LoggedInOnlyAttribute (server-side session checks)
Views/           Razor views for every page above, incl. _ClassesPartial for AJAX
wwwroot/         site.css, site.js (AJAX filter + SweetAlert2 confirm)
```

## Database schema
- **Trainer** (1) — (M) **GymClass** — a class belongs to exactly one trainer.
- **Member** (M) — (M) **GymClass** via **Enrollment** (join entity with `EnrollmentDate`).
- A unique index on `(MemberId, GymClassId)` in `Enrollment` prevents duplicate enrollment.

## Getting started

1. **Restore packages**
   ```bash
   dotnet restore
   ```

2. **Set your connection string** (already defaults to LocalDB) in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=GymManagementDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
   }
   ```
   Point this at a real SQL Server instance if you're not using LocalDB.

3. **Create the initial migration** (the repo ships without a `Migrations/` folder so you can
   generate one that matches your EF Core tooling version):
   ```bash
   dotnet tool install --global dotnet-ef   # if you don't have it already
   dotnet ef migrations add InitialCreate
   ```

4. **Run the app** — migrations apply and seed data loads automatically on startup:
   ```bash
   dotnet run
   ```
   (`Program.cs` calls `db.Database.Migrate()` and `DbInitializer.Seed(db)` at launch, so you
   don't need to run `dotnet ef database update` manually — though you can if you prefer.)

5. **Log in**

   | Role    | Email              | Password    |
   |---------|--------------------|-------------|
   | Admin   | admin@gym.com      | Admin123    |
   | Trainer | trainer@gym.com    | Trainer123  |

## Feature checklist (maps to the assignment)
- [x] Code First EF Core models with PKs/FKs/navigation properties (1:M Trainer→GymClass, M:M Member↔GymClass via Enrollment)
- [x] Login page with seeded Admin (`admin@gym.com` / `Admin123`); any other valid user = Trainer
- [x] Session stores `IsAdmin`; Layout shows/hides Admin nav links based on it
- [x] Every Admin controller re-checks `IsAdmin` server-side via `[AdminOnly]` (not just hidden links)
- [x] Trainer Classes page: list + Trainer dropdown filter
- [x] AJAX: dropdown change → `GET /Classes/FilterByTrainer` → returns `_ClassesPartial` → only the classes section updates
- [x] Class Details page: class info + enrolled members
- [x] Admin CRUD (List/Create/Edit/Delete/Details) for Trainers, Gym Classes, Members
- [x] Admin Enroll page: pick Member + Gym Class + Enrollment Date
- [x] Repository Pattern: `ITrainerRepository`, `IGymClassRepository`, `IMemberRepository`, `IEnrollmentRepository` + implementations; controllers depend only on the interfaces
- [x] ViewModels for Login, Classes filtering, Class details, Enrollment
- [x] Tag Helpers throughout (`asp-for`, `asp-action`, `asp-controller`, `asp-items`, validation tag helpers)
- [x] Nice-to-have: duplicate enrollment prevention (unique index + explicit check), SweetAlert delete confirmation
- [ ] Search Classes / Pagination / Service Layer — not implemented; straightforward to layer on top of the existing repositories if you want to extend it further

## Notes / simplifications
- Passwords are stored in plain text in the `User` seed table purely to keep the login flow
  simple for this course project. In a production system, use ASP.NET Core Identity with
  password hashing instead.
- No `Migrations/` folder is included since EF Core tooling versions vary by machine — run
  `dotnet ef migrations add InitialCreate` once after restoring packages (see step 3 above).
