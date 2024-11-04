# Smoke
Sandbox project, the idea:

An application, designed to execute and evaluate multiple API smoke tests on demand. 

Users can provide cURL, database requests, save them, add placeholders to be dynamically changed, and specify expected data. 

These requests can be chained and manipulated together to create complex scenarios where multiple APIs are called, and results are evaluated.

Planned Features:
- API Request Management: Create, update, and manage HTTP requests, including support for authorization and authentication.
- Scenario Building: Chain multiple requests into scenarios with ordered steps, dependencies, and data mappings.
- Authorization Support: Integrate with identity servers to obtain and manage tokens for authorized requests.
- Database Operations: Include database interactions (MongoDB, PostgreSQL) as part of your scenarios.
- On-Demand Execution: Execute scenarios on demand, particularly during CI/CD pipelines such as GitHub Actions.
- Result Evaluation: Validate actual responses against expected data and collect execution results.
- Integration with GitHub Actions: Easily integrate scenario execution into your deployment workflows by simply calling scenario execution url.

Challenges:
- Authorization requests (pipeline to retrieve tokens or reuse browser cookies)
- Security (request content encryption in case of password, connection string, api secrets or similar data)

The application follows the Clean Architecture pattern, promoting separation of concerns and scalability. It is divided into several layers:
- Domain Layer: Contains the core business logic and entities.
- Application Layer: Contains application-specific logic, use cases, and interfaces.
- Infrastructure Layer: Contains implementations of interfaces, data access, and external services.
- Presentation Layer (API): Contains the API controllers and models.
- Presentation Layer (UI): Contains control mechanisms and visual request and scenario building (TBA)
- Testing Layer: Contains unit and integration tests. (TBA)

Registration:
![image](https://github.com/user-attachments/assets/06cf6fca-4a61-4975-a034-a37a8cd7d648)

Execution:
![image](https://github.com/user-attachments/assets/906341d3-58b8-4009-b935-4d255a615ef2)

Structure:

![image](https://github.com/user-attachments/assets/8eb92d0f-65ce-4206-8ed6-b9ac1e75f8c9)
