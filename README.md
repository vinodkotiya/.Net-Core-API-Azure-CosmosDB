# User Management API with Azure Cosmos DB

This project implements a **REST API** for managing users using **ASP.NET Core** and **Azure Cosmos DB**. It provides endpoints to perform CRUD operations on user data, including handling API keys. The application is built with scalability, modularity, and best practices in mind.

---

## **Features**
- Add a new user with optional API keys.
- Retrieve a single user by ID.
- Retrieve all users.
- Update user details (name, email, API keys).
- Delete a user by ID.
- Partitioned storage in Cosmos DB for efficient querying.

---

## **Technologies Used**
- **.NET Core** (Minimal APIs)
- **Azure Cosmos DB** (NoSQL database)
- **Newtonsoft.Json** (JSON serialization)
- **Swagger** (API documentation and testing)

---

## **Prerequisites**
1. **Azure Account**:
   - A Cosmos DB account with a container configured.
   - Example Partition Key: `/userId`.
2. **Development Environment**:
   - **Visual Studio Code** or **Visual Studio**.
   - **.NET SDK** (6.0 or higher).
3. **Git**:
   - Installed for version control.

---

## **Setup Instructions**

### 1. **Clone the Repository**
```bash
git clone https://github.com/your-username/your-repository-name.git
cd your-repository-name


2. Update Configuration
Update the appsettings.json file with your Cosmos DB credentials:

json
Copy code
{
  "CosmosDb": {
    "Account": "https://your-cosmosdb-account.documents.azure.com:443/",
    "Key": "your-primary-key",
    "DatabaseName": "your-database-name",
    "ContainerName": "your-container-name"
  }
}

3. Restore Dependencies
Run the following command to restore NuGet packages:

bash
Copy code
dotnet restore
4. Run the Application
Use the following command to start the application:

bash
Copy code
dotnet run
The API will be available at:

Base URL: http://localhost:5000
Swagger Documentation: http://localhost:5000/swagger
Endpoints
1. Add User
POST /api/user

Payload Example:

json
Copy code
{
    "id": "user123",
    "userId": "user123",
    "name": "John Doe",
    "email": "john.doe@example.com",
    "apiKeys": [
        {
            "key": "apikey123",
            "createdAt": "2024-12-14T10:00:00Z",
            "expiresAt": null
        }
    ]
}
2. Get All Users
GET /api/users

Response:

json
Copy code
[
    {
        "id": "user123",
        "userId": "user123",
        "name": "John Doe",
        "email": "john.doe@example.com",
        "apiKeys": [
            {
                "key": "apikey123",
                "createdAt": "2024-12-14T10:00:00Z",
                "expiresAt": null
            }
        ]
    }
]
3. Get User by ID
GET /api/user/{id}

Response:

json
Copy code
{
    "id": "user123",
    "userId": "user123",
    "name": "John Doe",
    "email": "john.doe@example.com",
    "apiKeys": [
        {
            "key": "apikey123",
            "createdAt": "2024-12-14T10:00:00Z",
            "expiresAt": null
        }
    ]
}
4. Update User
PUT /api/user/{id}

Payload Example:

json
Copy code
{
    "name": "Jane Doe",
    "email": "jane.doe@example.com",
    "apiKeys": [
        {
            "key": "apikey456",
            "createdAt": "2024-12-15T12:00:00Z",
            "expiresAt": null
        }
    ]
}
5. Delete User
DELETE /api/user/{id}

Project Structure
graphql
Copy code
├── Controllers
│   └── UserController.cs        # Handles API endpoints
├── Contracts
│   └── CreateUserDto.cs         # DTO for creating a user
│   └── UpdateUserDto.cs         # DTO for updating a user
│   └── ApiKeyDto.cs             # DTO for API keys
├── Models
│   └── karmaUser.cs             # User model
│   └── ApiKey.cs                # API key model
├── Services
│   └── UserService.cs           # Cosmos DB operations for users
├── Program.cs                   # Application entry point
└── appsettings.json             # Configuration for Cosmos DB
Future Enhancements
Role-based authentication.
Pagination for retrieving users.
Additional user-related metadata.
Deployment to Azure App Service.
Contributing
Contributions are welcome! Please follow these steps:

Fork the repository.
Create a feature branch: git checkout -b feature-name.
Commit your changes: git commit -m "Add feature-name".
Push to the branch: git push origin feature-name.
Open a pull request.
License
This project is licensed under the MIT License. See LICENSE for details.

Contact
If you have any questions or suggestions, feel free to contact:

Email: vinodktoiya@gmail.com
GitHub: vinodkotiya
