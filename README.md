# GardenManager-Assignment
Automated garden management system (assignment for In The Pocket)

# Garden Manager API

## Architecture & Project Structure
> **Note:** I am used to develop with in-house frameworks of Enterprise companies. This approach was chosen to demonstrate my ability to implement a project from scratch, without relying on pre-existing in-house frameworks or templates.

This project was built using a **Test-Driven Development (TDD) and Outside-In approach**, with the codebase implemented according to the principles of **Clean & Hexagonal Architecture**. This architecture was chosen to ensure smooth maintenance and effortless extensibility in the future.

The project was initiated from an empty solution using the following structure, with each project prefixed with `InThePocket.Assignment.GardenManager.`:

* **Ports:** The ASP.NET Core Web API that serves as the entry and exit point of the application.
* **Application:** The core of the application, containing the domain models and business logic.
* **Contracts:** A .NET Standard project containing the classes required for API integration. This defines the contracts and information exposed to end-users and external systems.
* **Test Projects:** The corresponding unit and integration tests for the various layers.

---

## Testing Approach & Reflection

The development process began Outside-In, starting with writing tests for the `Create` endpoint in the `GardenController`. Subsequently, the `Get`, `Update`, and `Delete` functionalities were implemented following a TDD workflow.

* **Mocking & Isolation Logic:** External dependencies were placed behind interfaces and mocked using **NSubstitute**.
* **Test Structure:** Shared test data and setups are centralized in a `TestBase` class (e.g., `GardenControllerTestBase`). To keep different logical scenarios strictly separated, a dedicated test class was created for each controller method.
* **Process Reflection:** Having consistently adhered to strict TDD methodologies in past projects, this was the intended approach for the entire assignment. Halfway through the project, however, the remaining time proved too limited to sustain this intensive workflow across the entire scope. To guarantee that the core logic was sufficiently covered, the decision was made to add unit tests for the **Plant** logic post-implementation. While I strongly believe all tests are vital, the **Garden** module best reflects my standard TDD workflow, whereas the **Plant** module was provided with the necessary test coverage immediately afterward.

---

## Validation & Error Handling

* **Validation:** The application utilizes **FluentValidation** for validating incoming request data. This keeps validation rules highly flexible and maintains a clean overview per request type.
* **Error Handling & Logging:** In the event of invalid data or other exceptions, all endpoints return a clear, functional message to the client. Simultaneously, an error log is generated in the console for debugging purposes.
* **Status Codes:** The HTTP status codes within this API are currently indicative and can be easily adjusted in the future. In a production environment, these would naturally be aligned with company-specific API guidelines.

---

## Data Infrastructure & Version Control

* **ORM & Database:** The data infrastructure is set up using **Entity Framework Core (EF Core)** in combination with an **MSSQL Server**.
* **Repository Pattern:** A generic repository (`Repository<T>`) was chosen for data read and write operations. Because this repository handles abstract CRUD operations independently of the specific entity type, it eliminated the need to write repetitive database implementations. This resulted in significant time savings during development, allowing full focus on feature delivery. Since the application is built on Clean Architecture principles and the application layer communicates solely through interfaces (`IRepository<T>`), this setup remains highly adaptable. Should the application require more complex query mechanisms in the future (such as CQRS), this generic layer can be refactored per entity with minimal impact the core business logic.
* **Security:** No credentials or secrets are stored within the `appsettings.json` file inside the repository to prevent the exposure of sensitive data in version control.
* **Version Control:** Git and GitHub were used for version control, maintaining small, isolated, and atomic commits as much as possible.

---

## Functional Scope (Business Logic)

> **Note:** The API currently supports a single saved user. Authentication and multi-user management are slated as potential future extensions.

### Garden Management
The user can Create, Read, Update, and Delete (CRUD) gardens. The `target humidity level` can be adjusted by updating the garden entity. Additionally, plants can be added to, retrieved from, updated within, and removed from a specific garden.

### Plant Management
Plants can be created independently and linked to existing gardens. The application strictly monitors and enforces the garden's capacity:
* If there is insufficient space available in the garden, the action will fail. This applies both when adding a new plant and when modifying an existing plant if the modification increases its size.
* In these scenarios, the API returns a response containing a clear error message detailing exactly why the operation failed. 

---

## Docker

Due to the exceptionally hot weather, the Docker setup was not fully implemented. I am used to working with Octopus Deploy in local, test and production environments, and I would have implemented a complete Docker setup if time had allowed. However, Docker can be the first thing I pick up as a course after work.