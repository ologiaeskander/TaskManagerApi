Here’s an updated, polished version of your README reflecting the **current state of TaskManagerApi**:

---

# TaskManagerApi

A **Task Management API** built with **ASP.NET Core (.NET 9)** using **Entity Framework Core** and **SQL Server**.
Supports **Projects** and **Jobs (Tasks)** management with **CRUD operations**, **search**, **filtering**, and **role-based security**.

---

## Features

* **CRUD operations** for Projects and Jobs
* **Filter Jobs** by assigned user, status, priority, and due dates
* **Search Projects and Jobs** by name/title
* **Role-based access control** (Administrator, Moderator, User)
* **Authentication & JWT** token issuance for secure endpoints
* **CQRS & MediatR** command/query handling for clean architecture
* **Dummy data seeding** with initial users, roles, projects, and jobs

---

## Running the API

1. Clone the repository:

```bash
git clone <repo_url>
cd TaskManagerApi
```

2. Restore NuGet packages:

```bash
dotnet restore
```

3. Update your connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "MainConnection": "Server=YOUR_SERVER_NAME;Database=TaskManagerDb;Trusted_Connection=True;"
}
```

4. Apply migrations and seed the database:

```bash
dotnet ef database update
```

5. Run the API:

```bash
dotnet run
```

6. Test endpoints with Postman, VS Code REST Client, or your preferred HTTP client at:

```
https://localhost:<port>/api/<controller>
```

---

## Authentication

All endpoints (except registration/login/health) require **JWT Bearer authentication**. 
Currently only mannually passed.

* **Register a new user:**

```
POST /api/user/register
```

* **Login to obtain JWT token:**

```
POST /api/user/login
```

Include the token in requests using the `Authorization: Bearer <token>` header.

---

## Example Requests

### Projects

* Get all projects:

```
GET /api/projects
```

* Get a project by ID:

```
GET /api/projects/{id}
```

* Search projects by name:

```
GET /api/projects/search?name=Website
```

* Create a project (CQRS command handled via MediatR):

```
POST /api/projects
Content-Type: application/json
{
  "name": "New Marketing Campaign",
  "description": "Launch a new social media campaign"
}
```

* Update a project:

```
PUT /api/projects/{id}
Content-Type: application/json
{
  "name": "Updated Marketing Campaign",
  "description": "Updated description"
}
```

* Delete a project:

```
DELETE /api/projects/{id}
```

---

### Jobs

* Get all jobs:

```
GET /api/jobs
```

* Get a job by ID:

```
GET /api/jobs/{id}
```

* Filter jobs:

```
GET /api/jobs?assignedToUserId=2&excludeDone=true&dueBefore=2025-10-31
```

* Search jobs by title:

```
GET /api/jobs/search?title=App
```

* Create a job:

```
POST /api/jobs
Content-Type: application/json
{
  "title": "New Job",
  "description": "Description",
  "status": "ToDo",
  "priority": "Medium",
  "assignedToUserId": 2,
  "projectId": 1,
  "dueDate": "2025-10-30T12:00:00Z"
}
```

* Update a job:

```
PUT /api/jobs/{id}
Content-Type: application/json
{
  "title": "Updated Job",
  "description": "Updated description",
  "status": "InProgress",
  "priority": "High"
}
```

* Delete a job:

```
DELETE /api/jobs/{id}
```

> **Filtering** supports optional query parameters: `assignedToUserId`, `status`, `priority`, `excludeDone`, `dueBefore`, `dueAfter`.
> **Search** supports partial matches (`Contains`) for Project name or Job title.

---

## Test Credentials (Seeded Users)

| Username | Full Name     | Password | Role          |
| -------- | ------------- | -------- | ------------- |
| user     | Default User  | Paw0rd.  | User          |
| admin    | Administrator | Admin123 | Administrator |

> Use the login endpoint to obtain a JWT token for these accounts.

---

## API Health Check

* **Check API status:**

```
GET /api/home
```

Returns: `"Task Manager API is running"`

* **Ping endpoint:**

```
GET /api/home/ping
```

Returns: `"pong"`

---

## Notes

* Only the Project Create command (e.g., project/job creation) is currently handled via **CQRS + MediatR**.
* Swagger UI includes authentication support for testing endpoints with manual JWT token passing.
