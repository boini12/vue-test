# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

A "find a train journey that avoids football fans" app: given an origin, destination, and
travel time, the backend is meant to combine a train-journey API with a Bundesliga
fixtures/venues API so the frontend can warn the user if fans will be on their route
(`avoid-fans` is the app's working name — see the leftover config key in
`vue-project/backend/appsettings.json`). Currently only the train-journey lookup path is
actually wired up end to end; the soccer side exists as services/clients but nothing
calls into it yet (see Known gaps below).

Two independent projects live under `vue-project/`:
- `vue-project/frontend` — Vue 3 SPA (Vue CLI, not Vite)
- `vue-project/backend` — ASP.NET Core Web API (.NET 10, minimal hosting model)

They are not wired together by a shared build; run them as two separate processes during
development and the frontend calls the backend over HTTP.

## Commands

### Frontend (`vue-project/frontend`)
```
npm install       # install deps
npm run serve     # dev server with hot reload
npm run build     # production build
npm run lint      # eslint (vue3-essential + eslint:recommended), auto-fixes
```
There is no test script configured.

### Backend (`vue-project/backend`)
```
dotnet run                 # runs on http://localhost:5025 (see Properties/launchSettings.json)
dotnet build
```
There is no `.sln` file and no test project — `dotnet run`/`dotnet build` operate directly
on `backend.csproj`. There are no automated tests in the repo currently.

The frontend's `JourneysView.vue` calls the backend at a hardcoded
`http://localhost:5025/api/train/journeys` — the backend must be running on that exact
port for the app to work end to end.

### Running from VS Code

`.vscode/launch.json` and `.vscode/tasks.json` at the repo root provide a one-click way to
run both processes together:
- Open the Run and Debug panel and pick **"Full Stack (backend + frontend)"**, then hit F5.
  This runs the `Backend (.NET)` config (builds `backend.csproj` via the `backend: build`
  task, then launches it with the debugger attached — breakpoints in C# work) and the
  `Frontend (npm serve)` config (runs `npm run serve` in an integrated terminal) together
  as a compound launch.
- The backend debug config requires the **C# Dev Kit** extension (listed in
  `.vscode/extensions.json`) for the `coreclr` debug type to be available.

## Backend architecture

Minimal-hosting style: `Startup.cs` (despite the name, this is the `Program.cs`-equivalent
entry point with top-level statements) does all DI registration and request pipeline setup
— there is no separate `Startup` class with `ConfigureServices`/`Configure`.

Layering, thin-to-thick:
- **Controllers/** — thin HTTP endpoints, delegate directly to a service, no logic.
- **Services/** — orchestration/business logic. E.g. `TrainService` resolves station
  names to IDs before fetching journeys, combining two client calls into one operation.
- **Clients/** — typed `HttpClient` wrappers around external REST APIs
  (`TrainApiClient` → v6.db.transport.rest, `SoccerApiClient` → thesportsdb.com). These
  own JSON deserialization and API-specific query-param building, and are registered via
  `AddHttpClient<TInterface, TImpl>` with `BaseAddress` bound from config
  (`TrainApi:BaseUrl`, `SoccerApi:BaseUrl` in `appsettings.json`).
- **Models/** — DTOs/records for both API responses and controller inputs. Note the
  namespace is `backend.Model` (singular) even though the folder is `Models` (plural) —
  this is intentional/existing, not a mistake to "fix" when adding new files; match
  whichever files a given feature lives next to.
- **Settings/** — `IOptions<T>`-style settings classes bound from config sections.

External APIs called by the clients have no API key configured in this repo; requests are
made anonymously against public endpoints.

### Known gaps (don't be surprised by these)
- `ISoccerApiService`/`SoccerApiService` are fully implemented but **not registered** in
  DI in `Startup.cs` (only `ITrainService` is), and there is no `SoccerController`. If you
  build out the "avoid fans" feature, you'll need to wire this up.
- `Models/UserInput.cs` isn't referenced by any controller/service yet.
- Error handling in the clients is "log and return empty/null" rather than surfacing
  failures — a failed upstream call currently looks identical to "no results" by the time
  it reaches the controller.

## Frontend architecture

Standard Vue CLI (webpack) app, Vue 3 `<script setup>` style, `vue-router` for navigation,
no state-management library (component-local `ref`s only).

- `views/HomeView.vue` — the search form (origin/destination/time/date), pushes the
  selections into the router as query params on `journeys`.
- `views/JourneysView.vue` — results page; reads the search params and calls the backend
  directly via `fetch` in `onMounted` (no shared API client module — if you add more
  backend calls, consider whether they belong in a shared fetch helper instead of
  duplicating this pattern).
- `router/index.js` — two routes: `/` (home) and `/journeys`.
- `components/` is currently empty — no shared/reusable components exist yet.

Uses `vuejs3-datepicker` for the date input. Note `vue-project/package.json` (one level
above `frontend/`) also lists datepicker deps but is not the app's real package.json —
the actual frontend dependencies live in `vue-project/frontend/package.json`.
