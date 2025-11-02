# My Anime Collection

My Anime Collection is a personal anime management web application developed as part of the "Desenvolvimento de Aplicações Web" (DAW) course. The project provides an ASP.NET Core API and a Razor MVC frontend that let users browse anime, create personal lists, rate anime and other users' lists, and view other users' profiles.

Authors:
- Martinho José Novo Caeiro - 23917
- Paulo António Tavares Abade - 23919

Institution:
- Instituto Politécnico de Beja — Escola Superior de Tecnologia e Gestão

---

Table of contents
- Project overview
- Features
- Technology stack
- Data model (summary)
- API endpoints
- Running the project (development)
- Using Swagger
- Seed / test data (Jikan)
- Known issues
- Future improvements
- Contributing
- References

## Project overview
This project aims to help anime fans organize and share their collections. Users can:
- Search and view detailed anime information (synopsis, episodes, seasons).
- Create and manage personal lists (e.g., "To Watch", "Watching", "Completed", custom lists).
- Add and remove anime to/from lists.
- Rate individual anime (1–5 stars) and rate other users' lists.
- View other users' public profiles and their lists.
- See rankings of top-rated anime (with date filtering enhancement).

The API consumes data from the Jikan API to populate the anime table when necessary. Entity Framework Core (Code First) and LocalDB / SQL Server are used for persistence.

## Features
- Browse and search anime
- CRUD operations on anime (API)
- User registration, login (cookie-based authentication), and authorization
- Create and manage user lists (store anime IDs in each list)
- Rate anime and user lists
- View other users' profiles and public lists
- Swagger UI for API exploration and testing

## Technology stack
- Backend & Frontend: ASP.NET Core 9.0 (C#)
- ORM: Entity Framework Core (Code First + Migrations)
- Database: SQL Server / LocalDB
- Frontend views: Razor (HTML, CSS, Bootstrap)
- Client-side: JavaScript
- API documentation: Swagger (OpenAPI)
- Project documentation: LaTeX (reports in /Relatorios)

## Data model (summary)
Main entities used in the project (simplified):
- Users(user_id, email, password, name, age, biography)
- UserList(userlist_id, user_id, name, description, anime_ids)
- UserListAvaliation(userlistavaliation_id, user_id, user_list_id, avaliation, datacreated)
- UserAnimeAvaliation(useranimelistavaliation_id, user_id, anime_id, avaliation, datacreated)
- Anime(anime_id, synopsis, genres, n_episodes, n_seasons)

Note: The association between lists and anime is implemented by storing a list of anime IDs (List<int> AnimeIds) in the UserList entity, rather than a normalized join table. This was a pragmatic choice during implementation and is listed in "Future improvements".

## API — main endpoints
Examples of implemented endpoints (check Swagger for full list):
- GET /api/animes — return all animes
- GET /api/animes/{id} — return an anime by id
- POST /api/animes — add a new anime
- PUT /api/animes/{id} — update an anime
- DELETE /api/animes/{id} — delete an anime

Other controllers expose user, user list, and rating-related endpoints to support the frontend.

## Running the project (development)
Prerequisites:
- .NET 9 SDK
- SQL Server or LocalDB (LocalDB recommended for local development)
- (Optional) dotnet-ef tools or Visual Studio for running migrations

Quick start:
1. Clone the repository:
   git clone https://github.com/zDragonPaulo/MyAnimeCollection_DAW.git

2. Open the solution in Visual Studio or VS Code.

3. Configure the database connection:
   - Edit the connection string in appsettings.json (API and/or MVC project) to point to your LocalDB or SQL Server instance.
   - Example LocalDB connection string:
     "ConnectionStrings": { "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MyAnimeCollectionDB;Trusted_Connection=True;" }

4. Apply EF Core migrations (from project root or appropriate project folder):
   - dotnet ef database update
   - (Or use Visual Studio package manager console)

5. Run the API project first, then the MVC frontend project (they may be the same solution depending on how the solution is organized).

Important note: During development this project exhibited a behavior where the API sometimes needs to be started twice to initialize LocalDB files correctly (an issue tied to relative .mdf/.log file paths). If you encounter a startup error related to the database, try restarting the API.

## Using Swagger
Swagger UI is available when running the API. It provides interactive documentation and lets you test endpoints without the frontend:
- Open: http://localhost:{port}/swagger

## Seed / test data (Jikan)
The API includes a service that fetches anime data from the Jikan API and seeds the Anime table if it is empty. This seeding runs only the first time (or when the anime table is empty), which speeds up local testing.

## Known issues
- Sometimes the API startup requires a second run to properly connect/create LocalDB files (.mdf/.log). This is environment-specific and related to relative database file paths.
- UserList <-> Anime relation is stored as a list of anime IDs inside UserList (List<int> AnimeIds) instead of a normalized many-to-many join table. This simplifies some operations but limits querying efficiency and referential integrity.

## Future improvements
- Normalize the lists-anime relationship using a proper many-to-many join table.
- Reintroduce and manage a Genre table for richer filtering and queries.
- Improve database seed/initialization to avoid requiring a second start.
- Add image uploads for anime cover art and caching solutions for external API requests.
- Move the database to a hosted SQL Server instance and use environment variables for configuration.
- Add automated unit and integration tests.

## Contributing
Contributions are welcome:
1. Fork the repo
2. Create a branch with your feature/fix
3. Open a pull request with a clear description of changes

Before submitting PRs, avoid committing local .mdf/.log paths or other environment-specific files.

## References
- ASP.NET Core documentation
- Entity Framework Core documentation
- SQL Server / LocalDB docs
- Jikan API (MyAnimeList REST API wrapper)
- Swagger / OpenAPI
- Bootstrap
