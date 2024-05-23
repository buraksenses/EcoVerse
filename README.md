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

3. **Inventory Service**:
   - **API Endpoints**:
     - `GET /api/inventory`: Retrieve inventory status.
     - `PUT /api/inventory/{productId}`: Update inventory for a product.
   - **Responsibilities**: Manage stock levels and inventory status.

4. **Identity Service**:
   - **API Endpoints**:
     - `POST /api/identity/login`: Authenticate a user.
     - `POST /api/identity/register`: Register a new user.
   - **Responsibilities**: Handle user authentication and authorization.

