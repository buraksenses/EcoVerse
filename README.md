# EcoVerse

**EcoVerse** is a modern e-commerce platform built using .NET 8 and a microservices architecture. The platform is designed to efficiently manage product information, shopping carts, and inventory, ensuring a scalable and maintainable system.

## Technologies and Architectures Used

- **.NET 8**: The core framework for developing microservices.
- **Microservices Architecture**: Decoupled services for better scalability and maintainability.
- **RabbitMQ**: Asynchronous messaging between microservices.
- **SQL and NoSQL Databases**: Different storage solutions for varied data needs.
- **Docker**: Containerization for consistent deployment.
- **IdentityServer**: Authentication and authorization management.

## Services and Their Responsibilities

1. **Product Service**:
   - **API Endpoints**:
     - `GET /api/products`: Retrieve all products.
     - `GET /api/products/{id}`: Retrieve a specific product by ID.
     - `POST /api/products`: Create a new product.
     - `PUT /api/products/{id}`: Update an existing product.
     - `DELETE /api/products/{id}`: Delete a product by ID.
   - **Responsibilities**: Manage product data including creation, retrieval, updating, and deletion of products.

2. **Cart Service**:
   - **API Endpoints**:
     - `GET /api/carts/{userId}`: Retrieve the shopping cart for a user.
     - `POST /api/carts/{userId}/items`: Add an item to a user's cart.
     - `PUT /api/carts/{userId}/items/{itemId}`: Update an item in a user's cart.
     - `DELETE /api/carts/{userId}/items/{itemId}`: Remove an item from a user's cart.
   - **Responsibilities**: Handle shopping cart functionalities, including adding, updating, and removing items.

3. **Inventory Management Service**:

   **Description**: This service is responsible for managing stock levels and inventory status. It processes inventory commands and queries, ensuring that inventory data is up-to-date and accessible.
   
   **Technologies and Architecture**:
   - **CQRS (Command Query Responsibility Segregation)**: Separates the read and write operations to optimize performance and scalability.
   - **Event Sourcing**: Captures all changes to the inventory as events, which are stored in MongoDB.
   - **MongoDB**: Used for storing events in the command service.
   - **RabbitMQ**: Used for asynchronous communication between microservices.
   - **SQL Database**: Utilized for persistent storage of inventory data in the query service.
   - **MediatR**: Used to handle commands and queries within the application.
   
   **API Endpoints**:
   
   #### Command API
   - `POST /api/v1/inventory`: Add a new inventory item.
     - **Description**: Adds a new item to the inventory.
     - **Request Body**: JSON object containing item details (e.g., productId, quantity, price).
     - **Response**: `201 Created` on success.
     - **Event Published**: `InventoryItemAdded` event to RabbitMQ.
     - **Database**: Stores events in MongoDB using Event Sourcing.
   
   - `PUT /api/v1/inventory/{id}/quantity`: Update inventory quantity for a product.
     - **Description**: Updates the inventory quantity for a specific product.
     - **Request Body**: JSON object containing the updated quantity.
     - **Response**: `200 OK` on success.
     - **Event Published**: `InventoryQuantityUpdated` event to RabbitMQ.
   
   - `PUT /api/v1/inventory/{id}/price`: Update inventory price for a product.
     - **Description**: Updates the inventory price for a specific product.
     - **Request Body**: JSON object containing the updated price.
     - **Response**: `200 OK` on success.
     - **Event Published**: `InventoryPriceUpdated` event to RabbitMQ.
   
   - `DELETE /api/v1/inventory/{id}`: Remove an inventory item.
     - **Description**: Removes an item from the inventory.
     - **Response**: `204 No Content` on success.
     - **Event Published**: `InventoryItemRemoved` event to RabbitMQ.
   
   #### Query API
   - `GET /api/v1/inventory`: Retrieve inventory status.
     - **Description**: Returns the current inventory status.
     - **Response**: `200 OK` with the inventory details.
   
   - `GET /api/v1/inventory/{productId}`: Retrieve inventory status for a specific product.
     - **Description**: Returns the inventory status of a specific product identified by its ID.
     - **Response**: `200 OK` with the product's inventory details.
   
   **Directory Structure and Responsibilities**:
   
   - **EcoVerse.StockManagement.Command.API**: Contains the API project for handling inventory commands, exposing the HTTP endpoints for adding, updating, and deleting inventory items.
   - **EcoVerse.StockManagement.Command.Application**: Houses the application logic for command handling, including command handlers, business rules, and integration with RabbitMQ using MediatR.
   - **EcoVerse.StockManagement.Command.Domain**: Defines the core domain entities and value objects, encapsulating business logic related to inventory commands.
   - **EcoVerse.StockManagement.Command.Infrastructure**: Implements the data access logic for command operations, integrating with MongoDB for Event Sourcing and RabbitMQ for event publishing.
   
   - **EcoVerse.StockManagement.Query.API**: Contains the API project for handling inventory queries, exposing the HTTP endpoints for retrieving inventory data.
   - **EcoVerse.StockManagement.Query.Application**: Houses the application logic for query handling, including query handlers and business rules, using MediatR.
   - **EcoVerse.StockManagement.Query.Domain**: Defines the core domain entities and value objects, encapsulating business logic related to inventory queries.
   - **EcoVerse.StockManagement.Query.Infrastructure**: Implements the data access logic for query operations, integrating with the SQL database.
   
   - **EcoVerse.StockManagement.Common/Events**: Defines the events used in the inventory service, such as `InventoryItemAdded`, `InventoryQuantityUpdated`, `InventoryPriceUpdated`, and `InventoryItemRemoved`.
   - **EcoVerse.StockManagement.CQRS.Core**: Contains the core infrastructure and utilities supporting the CQRS pattern in the inventory service.
4. **Identity Service**:
   - **API Endpoints**:
     - `POST /api/identity/login`: Authenticate a user.
     - `POST /api/identity/register`: Register a new user.
   - **Responsibilities**: Handle user authentication and authorization.
  
   - I understand now. I'll provide a comprehensive README for the Inventory Management service, including all relevant details such as Mediator usage, MongoDB, Event Sourcing, and all API endpoints.




