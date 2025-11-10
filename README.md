# Task Manager Api

A simple **Task Management API** built with **ASP.NET Core (.NET 8/9)** using **Entity Framework Core** and **SQL Server**.
Allows management of **Projects** and **Jobs (Tasks)** with filtering, search, and basic CRUD operations.

---

## Features

* CRUD operations for **Jobs** and **Projects**
* Filter Jobs by **assigned user, status, priority, due dates**
* Search Projects and Jobs by **name/title**
* Dummy data seeded via EF Core `HasData()`

---

### Running the API

1. Clone the repository:

```bash
git clone <repo_url>
cd TaskManagerApi
```

2. Restore packages:

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

6. Test endpoints using your preferred HTTP client (VS Code REST Client, Postman, etc.) at:

```
https://localhost:<port>/api/<controller>
```

---

## Example Requests

### Projects

* Get all projects:

```
GET /api/projects
```

* Get project by id:

```
GET /api/projects/id
```

* Search project by name:

```
GET /api/projects/search?name=Website
```

* Create a project:

```
POST /api/projects
Content-Type: application/json
{
  "name": "New Marketing Campaign",
  "description": "Launch a new social media campaign",
  "createdBy": 2
}
```

* Update a project:

```
PUT /api/projects/{id}
Content-Type: application/json
{
  "id": 3,
  "name": "Updated Marketing Campaign",
  "description": "Updated description",
  "createdBy": 2,
  "createdAt": "2025-10-23T12:00:00Z"
}
```

---

### Jobs

* Get all jobs:

```
GET /api/jobs
```
* Get job by id:

```
GET /api/jobs/id
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
{
  "title": "New Job",
  "description": "Description",
  "status": "ToDo",
  "priority": "Medium",
  "assignedToUserId": 2,
  "projectId": 1,
  "creatorId": 1,
  "createdAt": "2025-10-23T12:00:00Z",
  "dueDate": "2025-10-30T12:00:00Z"
}
```
---

> Note:
> * Filtering supports optional query parameters in Jobs: `assignedToUserId`, `status`, `priority`, `excludeDone`, `dueBefore`, `dueAfter`
> * Searching supports partial matches (`Contains`) for Project name or Job title

---

## Test Credentials (Dummy Data)

You can use the following **users for testing purposes** (passwords are dummy/hashed strings in the database):

| Username | Full Name     | Password  | Role     | Department |
| -------- | ------------- | --------- | -------- | ---------- |
| youssef  | Youssef Nabil | hashed123 | Manager  | Technical  |
| sara     | Sara Magued   | hashed456 | Employee | Marketing  |

> Note: Authentication/login endpoints are **not implemented yet**.
