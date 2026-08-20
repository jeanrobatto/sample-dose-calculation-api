# sample-dose-calculation-api
Example ASP.NET Web API app

## Description

Small server that allows the creation/execution of Dose Calculations and perists them in a database for future retrieval.

## Get Started

### Setup CosmosDB 

#### Setup local emulator with Docker

Fetch the image: `docker pull mcr.microsoft.com/cosmosdb/linux/azure-cosmos-emulator:vnext-latest`

Run the container: `docker run --detach --publish 8081:8081 --publish 8080:8080 --publish 1234:1234 mcr.microsoft.com/cosmosdb/linux/azure-cosmos-emulator:vnext-latest --protocol http`

Access GUI at `http://localhost:1234`

Ref: https://learn.microsoft.com/en-us/azure/cosmos-db/emulator-linux

#### Alternative

Run a free-tier Azure CosmosDB instance.

Ref: https://azure.microsoft.com/en-us/products/cosmos-db

### Setup DOTNET

Make sure DOTNET SDK 10 is installed on your machine (https://dotnet.microsoft.com/en-us/download/dotnet/10.0).

### Setup server port

The default server port is `5158` for development.

This can be changed in `/DoseCalculationAPI/Properties/launchSettings.json`.

### Setup database container and key

The defaults are the containers and keys that ship with CosmosDB.

These values can be edited in `/DoseCalculationAPI/appsettings.Development.json`.

### Run the project

To build the project: `dotnet build`

To run the project: `dotnet run`

To see the server documentation, API routes, and to test functionality, navigate to: `http://localhost:<PORT>/scalar/v1`

## Project Architecture and Rationale

### Diagram

Please see attached the architecture diagram in `/architecture.png`.

### Architecture Overview

This simple demo project was separated in 3 core layers:

1. API

Handles the RESTful aspect of the app, interactions with external clients, request validation, input/output error handling, initial data guards and validation.

It then uses the Domain layer for business logic, and returns responses to the clients.

2. Domain

Responsible for calculations, formulas, domain-level validation and overall correctness.

Uses the Persistence layer for CRUD operations on the Database.

3. Persistence

Simply allows interfacing with whichever underlying storage is selected. Abstracted to facilitate storage migrations and scalability.

### Architecture Rationale

While at first glance this separation might seem a little complex, it is designed purposefully to allow a series of outcomes:

1. Independently testable modules

It is primordial to be able to test the various functionality in isolation. When writing tests for the dosing calculations, one should not be concerned with persistence and/or networking and vice versa. This design allows spoofing of all layers to accurately and completely test all parts of the app.

2. Abstractions for quick responses to market

Any dependency in the app, including third party libraries or data storage mechanisms, need to be easily and cheaply swappable to accomondate any future business requirement. Separating the persistence allows us to replace CosmosDB for any other (possibly cheaper or more efficient) alternative without impacting the rest of the project. Similarly, abstracting the business/domain logic allows us more flexibility if ever we need to move away from the DOTNET ecosystem by ensuring the code is raw C# and does not depend on any framework-specific construct.

3. Scalability

There are many considerations for scalability, including adding features, increasing the team size, increasing performance and supported number of users etc. This design will comfortably allow all of these expansions to happen over time with minimal design changes.

### Architecture Principles

1. Separation of concerns

Small, independent, reusable modules allow for quick centralized updates and easier bug resolution. It also helps developers quickly orient themselves and understand what each component is doing for quicker onboarding and better collaboration.

2. Programming to interfaces

Fundamental software principle - interfaces are used as contracts in the various components that can be quickly switched out with different implementation approaches with no other changes required in other modules.

3. Calculation correctness

The usage of decimal data type over doubles sacrifices some performance in order to minimize floating point arithmatic and rounding errors.

4. Clear dependency graph

This design prevents any situation that could lead to cyclical dependencies even as the server scales to a much bigger project. The dependency graph is clear: API -> Domain -> Persistence. Removes all ambiguity.

5. Data validation

Validation happens in two separate layers, independently, to minimize user input errors. The initial validation is on the API layer and will verify the data comforms to the REST API Contract; the real validation happens in the Domain layer to ensure the calculations are never performed on invalid data sets. The redundancy is by design.

### Other notes

1. The Domain services have been implemented using the Strategy Pattern to easily replace specific Formulas. The reason is, once again, to separate the concept of *what* a service does with *how* it is achieved. This separation greatly reduces the risk of software bugs.

2. The least visibility principle has been implemented, marking all classes as internal/sealed unless they have a good reason to be public.

3. The setting for code analysis at build time has been set to the strictest option, in order to raise flags whenever anything in the code might be a problem. A few error codes have been disabled intentionally for low-risk activities specific to DOTNET.

4. The packages used for this app are:

OpenAPI
Cosmos
Newtonsoft.Json (needed to serialize DTOs into CosmosDB Documents)
Scalar (used for a clear documentation/testing GUI auto compiled from the controllers)

5. The DoseCalculations are persisted in the database under the /medication key. This would be risky in a real app because, long term, the tables would get too big and thus inefficient to parse. I would have added a PatientID field instead, but chose not to alter the requirements.

6. The formulas are just my best guess at how the dose should be calculated. In reality, I would ask the experts exactly what the math behind the calculation should be. For instance error margins, tolerance, rounding, significant digits etc.

## Future Changes

This is a very simple application, and it is far from being production ready. Off the top of my head, here is a non-exhaustive list of things this app would need:

- Pagination for the GET list endpoint
- Security at all levels (DB, API)
- DevOps pipelines for CI/CD and more code analysis
- Cloud infrastructure for availability and reliability, such as load balancers, reverse proxies, automated backups, better logging, feature flags etc.
- Unit testing, at the minimum for the Domain layer
- A GUI to interact with the server

## Note on AI usage

In my daily workflow I use AI extensively. For this project, in order to maximize productivity, I haven't actually typed any of the code myself. Instead, I make the design decisions and I decide on the vision for the project, and let the AI write the classes before validating them and adding them to the code base.

In addition, I used AI for documentation access (alongside scanning websites), bug resolution pointers, DOTNET onboarding etc.

Of course, there is no code in this project I did not personally vet, analyze, and understand completely.

## Timesheet 

DOTNET familiarization and reading documentation: 1.5h
Architecture planning and diagram: 1h
Project implementation: 1.5h
Writing artifacts and submission: 30m

Total: ~4.5h of work logged.






