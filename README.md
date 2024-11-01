# Smoke
Sandbox project, the ideas:

Smoke an application, designed to execute and evaluate multiple API smoke tests on demand. 
Users can provide cURL requests, save them, and specify expected data. 
These requests are saved in persistence layer and can be used with HTTP clients. 
Requests can be chained together to create complex scenarios where multiple APIs are called, and results are evaluated.

Planned Features:
- API Request Management: Create, update, and manage HTTP requests, including support for authorization and authentication.
- Scenario Building: Chain multiple requests into scenarios with ordered steps, dependencies, and data mappings.
- Authorization Support: Integrate with identity servers to obtain and manage tokens for authorized requests.
- Database Operations: Include database interactions (MongoDB, PostgreSQL) as part of your scenarios.
- On-Demand Execution: Execute scenarios on demand, particularly during CI/CD pipelines such as GitHub Actions.
- Result Evaluation: Validate actual responses against expected data and collect execution results.
- Integration with GitHub Actions: Easily integrate scenario execution into your deployment workflows.

The application follows the Clean Architecture pattern, promoting separation of concerns and scalability. It is divided into several layers:
- Domain Layer: Contains the core business logic and entities.
- Application Layer: Contains application-specific logic, use cases, and interfaces.
- Infrastructure Layer: Contains implementations of interfaces, data access, and external services.
- Presentation Layer (API): Contains the API controllers and models.
- Testing Layer: Contains unit and integration tests. (TBA)
