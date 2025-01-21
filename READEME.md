# Otkupni Blokovi Management System

## Overview
This project is a simple management system for handling "Otkupni Blokovi" using **ASP.NET Core MVC** and **Entity Framework Core**. The system is designed to enable multiple companies (tenants) to independently manage their "otkupni blokovi."

---

## Business Requirements

### 1. Multi-Tenancy
- Each company's data is isolated and not visible to others.

### 2. Forms
- Basic frontend built with **Bootstrap** and **jQuery** focusing on functionality.
- Features:
    - **List View** of "otkupni blokovi" (search by date, pagination).
    - "Otkupni Blok" form with full CRUD functionality.

### 3. Models

#### OtkupniBlok (Main Entity)
- **Fields:**
    - `Broj`: Incremental number unique to the year and company.
    - `Datum`: Date of creation.
    - `Prodavatelj` (Seller): Includes `Ime`, `Prezime`, and `OIB`.
    - `Iznos`: Total amount.
    - `Status`: One of `Nije isplaćeno`, `Pripremljen`, or `Isplaćeno`.
    - `DatumUnosa`, `DatumPromjene`: Timestamps.
    - `Napomena`: Notes.

#### Stavka (Item)
- **Fields:**
    - `Proizvod`: Product name.
    - `Količina`: Quantity.
    - `Jedinica količine`: Unit of quantity.
    - `Cijena po jedinici količine`: Price per unit.
    - `DatumUnosa`, `DatumPromjene`: Timestamps.

---

## Technical Requirements

### 1. Data Access
- **Entity Framework Core**
- **SQL Server** as the database.
- Proper database schema with relationships.

### 2. Architecture
- Follows **Dependency Injection** and **Separation of Concerns**.
- Implements **Best Practices** for maintainability and scalability.

### 3. Validation
- Data validation on both frontend and backend.
- Implements business rules, e.g., generating unique `Broj` for "otkupni blok."
- Exception handling to avoid yellow screen of death.

### 4. Multi-Tenancy
- Data isolation implemented at the database level (database per tenant).
- Mock login allows users to select a company, stored in session, and used for data isolation.

---

## Future Enhancements
Given more time, the following features could be implemented:
1. **Authentication and Authorization**:
    - Secure login system with role-based access control.
2. **Enhanced UI/UX**:
    - Modernize the interface with advanced frameworks like React or Angular.
3. **Reporting and Analytics**:
    - Add dashboards for data visualization and reporting.
4. **API Integration**:
    - Expose APIs for integration with other systems.
5. **Unit and Integration Testing**:
    - Improve test coverage for all modules.
6. **Performance Optimization**:
    - Optimize database queries and implement caching mechanisms.

---

## Setup Instructions

1. **Clone the Repository**:
   ```bash
   git clone git@github.com:imchergar/zelene-teh.git
   ```

2. **Set Up the Database**:
    - Use SQL Server to create separate databases for each tenant.

3. **Run Migrations**:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

4. **Run the Application**:
   ```bash
   dotnet run
   ```

5. **Access the Application**:
    - Navigate to `http://localhost:5234/` in your browser.

---

## Contact
For any questions or issues, please contact [imchergar](mailto:ivanmihael.cergar@gmail.com).
