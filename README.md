# 🚀 Generic Microservice Project in .NET 8

Welcome to the Generic Microservice Project! This is a project developed in .NET 8 that implements various patterns and popular technologies for building robust and scalable microservices.

## 🛠️ Features and Technologies

- **Entity Framework**: Used for object-relational mapping (ORM) and interaction with SQL Server database.
- **SQL Server**: Relational database used for storing data.
- **Repository Pattern**: Pattern used to abstract and isolate data access.
- **Clean Architecture**: Software architecture that promotes separation of concerns and framework independence.
- **Unity of Work**: Pattern used to manage transactions and actions across a set of repositories.
- **Fluent Validator**: Library used for fluent model validation.
- **JWT Authentication**: Authentication mechanism based on JSON Web Tokens.
- **Swagger**: Tool for API documentation and testing.
- **Services Pattern**: Pattern used to abstract business logic into independent services.

## ⚙️ Configuration and Execution

1. **Prerequisites**: Make sure you have the .NET 8 SDK installed on your machine. Additionally, you need access to a SQL Server database.
   
2. **Database Configuration**: Execute the provided SQL scripts in the `Scripts` folder to set up the database and necessary tables.
   
3. **Project Configuration**: Open the project in your preferred IDE and adjust the database connection settings in the `appsettings.json` file.
   
4. **Running the Project**: Compile and execute the project. Ensure that all dependencies are installed correctly.

## 📚 API Documentation

The API documentation is available through Swagger. To access it, run the project and open the following URL in your browser:

http://localhost:{port}/swagger
