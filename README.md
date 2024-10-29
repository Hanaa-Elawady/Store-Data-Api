# Api for E-commerce

## Overview

This Api project is designed as a 4-tier architecture solution for small e-commerce. It manages products, brands, types, and Orders, providing a comprehensive system for business operations.

## Key Features

- **Architecture**: 4-tier architecture for modularity and scalability.
- **Technologies**: 
  - **Language**: C#
  - **Framework**: ASP.NET Api with Entity Framework
  - **Database**: Microsoft SQL Server
- **Authentication**: Secure Identity-based authentication system for data integrity and user access control.

## Project Structure

- **Store.data**: Data Access Layer, handling database operations.
- **Store.Repository**: Implements repository patterns to abstract and encapsulate data access logic
- **Store.Service**: Business Logic Layer, containing business rules and logic.
- **Store.Web**: API layer or Presentation Layer in a web application architecture.

## Environment Variables
For security purposes, sensitive data such as connection strings, email credentials, and API keys should be stored in environment variables, not directly in the appsettings.json file. Replace the placeholders in the appsettings.json file with environment variables in your local or server environment.

## Getting Started

### Prerequisites

- .NET SDK
- SQL Server


## Usage

- **Authentication & Authorization**: Ensure secure access using tokens (like JWT)for API requests && Users have roles and the API will restrict access based on these roles.
- **Product Management**: operations for Product management.
- **Cart Management**: CRUD operations for Cart management.
- **Order Management**: CRUD operations for Order management.
- **Payment Integration**: Handle payment for an order, integrating with Stripe service.


