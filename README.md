
# Crime Database
For this task I have followed the standard Clean Architecture. I have four distinct projects: CrimeDatabase.Domain, CrimeDatabase.Application, CrimeDatabase.Infrastructure, and CrimeDatabase.Web. Each acts as a layer to abstract domain models, business logic, database persistence, and the UI, respectively. This practice allows for optimal separation of concerns, easier collaboration with teams (since projects can be worked on separately), and improved unit and integration testing.

# Project Structure
### CrimeDatabase.Domain
The Domain project defines the POCO (plain-old CLR object) definitions for Crime and NotesAudit, and the enum for the CrimeType. There is no business logic, validation, or UI information here. This is used simply to define the business models.

### CrimeDatabase.Application
The Application layer defines the repository interfaces, service interfaces, and their corresponding service implementations (CrimeService and NotesAuditService). Repository interfaces provide a persistence abstraction, while service interfaces define the core business operations such as updating a crime’s notes. The service implementations coordinate these operations by interacting with the repositories through their interfaces.

The key benefit of this structure is that we can define business logic independent of the underlying persistence method (e.g., Entity Framework). This separation also enables easy unit testing using mock repositories via Moq, without requiring integration with a real database.

### CrimeDatabase.Infrastructure
The Infrastructure layer holds the DbContext and also implements the repository interfaces from the Application layer to use the DbContext. In turn, when these repositories are provided to the services in the Application layer, the services will use Entity Framework and the DbContext for persistence.

### CrimeDatabase.Web
The Web project implements the UI. Since our models (entities) are defined in the Domain layer, I use ViewModels only, which expose attributes (mapped from the entities by the controller) with relevant data annotations for display and validation purposes.

# Testing
Testing can be run using the `dotnet test` command. The following sections detail the unit and integration tests.

### Unit Tests
Unit testing is performed to test the CrimeService and NotesAuditService on the Application layer using mock repositories via Moq. These tests ensure that the business logic (such as creating crimes, updating notes etc.) is robust.

### Integration Tests
Integration tests are performed with the Infrastructure layer using Testcontainers. Testcontainers provide throw-away MSSQL databases via Docker for Entity Framework, allowing a fresh database for each test while also offering CI/CD integration.

The integration tests cover key scenarios, including creating a crime, updating notes (and generating an audit entry), and retrieving all crimes.

# User Secrets
For the database connection details, the `DefaultConnection` string should be set using `dotnet user-secrets set` within the CrimeDatabase.Web project. The appsettings.json does not store the connection details for obvious security purposes.

The integration tests do not require user secrets since the database is created and disposed of for testing only.

# Sample Screenshots
### Creation of Crimes
<img width="2560" height="1528" alt="Screenshot 2025-11-30 212006" src="https://github.com/user-attachments/assets/11ad0219-ea1a-4ca2-a15c-bf53d2edc231" />

### Viewing All Crimes
<img width="2560" height="1528" alt="Screenshot 2025-11-30 212039" src="https://github.com/user-attachments/assets/cdded77c-c226-42e3-ad62-ae2497f600ed" />

### Editing a Crime's Notes
<img width="2560" height="1528" alt="Screenshot 2025-11-30 212129" src="https://github.com/user-attachments/assets/f5d6663c-4673-410f-9d43-b2fb327294e8" />

### Viewing Audits for a Specific Crime
<img width="2560" height="1528" alt="Screenshot 2025-11-30 212201" src="https://github.com/user-attachments/assets/b786bb2d-d70f-4c52-a038-77dfe1073a90" />

### Viewing All Audits
<img width="2560" height="1528" alt="Screenshot 2025-11-30 212252" src="https://github.com/user-attachments/assets/54480013-b973-4eaa-aeda-5692fecf1c0e" />
