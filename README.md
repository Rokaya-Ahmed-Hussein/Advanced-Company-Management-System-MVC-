# Advanced Company Management System (MVC)

## 📋 Project Overview

A secure, high-performance ASP.NET Core MVC application designed for managing company profiles and pharmaceutical drug data. This enterprise-level system follows the **Database-First approach** and adheres to **clean architecture principles** with a robust **3-tier architecture** pattern.

### 🎯 Key Features

- **Company Management**: Full CRUD operations for company profiles with soft-delete functionality
- **Drug Data Management**: Browse and filter pharmaceutical drug information from consolidated database views
- **Role-Based Access Control**: Multi-level access system with User and Admin roles
- **Audit Logging**: Comprehensive audit trail for all critical operations
- **Advanced Filtering**: Real-time search with server-side pagination
- **Data Export**: Excel export functionality for company data
- **Secure Authentication**: Cookie-based authentication with password hashing

---

## 🏗️ Architecture

### Database-First Approach (EF Core)
The application uses **Entity Framework Core** with the Database-First approach, reverse-engineered from the **EDDB** database to generate the data layer.

### 3-Tier Architecture

```
┌─────────────────────────────────────────────────────────┐
│          Presentation Layer (MVC)                       │
│  - Controllers (Thin, handle routing only)              │
│  - Views (Razor)                                        │
│  - ViewModels                                           │
└─────────────────────────────────────────────────────────┘
                         ↓
┌─────────────────────────────────────────────────────────┐
│          Business Logic Layer (BL)                      │
│  - Managers (Business logic implementation)             │
│  - ViewModels (DTOs for UI)                            │
│  - AutoMapper Profiles                                  │
└─────────────────────────────────────────────────────────┘
                         ↓
┌─────────────────────────────────────────────────────────┐
│          Data Access Layer (DTO)                        │
│  - Repositories (Data access)                           │
│  - Entity Models (Database entities)                    │
│  - DbContext                                            │
└─────────────────────────────────────────────────────────┘
```

### Dependency Injection
- All services and repositories are accessed via **Interfaces**
- **Constructor injection** is used throughout the application
- Follows **Inversion of Control (IoC)** principles

---

## 🔐 Security & Authentication

### User Roles & Access Control

#### 🔵 User Role (Standard Access)
- ✅ **Read Access**: View all companies and drugs
- ✅ **Create**: Add new companies
- ✅ **Update**: Edit existing companies
- ❌ **Delete**: Cannot delete companies (restricted to Admin only)
- ✅ **Export**: Download Excel reports
- ✅ **Audit Review**: View all audit logs


#### 🔴 Admin Role (Full Access)
- ✅ **Full CRUD**: Complete Create, Read, Update, Delete operations
- ✅ **Delete Companies**: Soft-delete with audit trail
- ✅ **System Management**: Access to all system features
- ✅ **Audit Review**: View all audit logs

## 📊 Database Schema

### Custom Tables (Added for the project)

#### 1. **Users** Table
Stores user account information:
- `UserId` (Primary Key)
- `Username` (Unique)
- `PasswordHash`
- `FullName`
- `Email`
- `IsActive`
- `CreatedDate`
- `CreatedBy`
- `RoleId` (Foreign Key to Roles)

#### 2. **Roles** Table
Defines user roles:
- `RoleId` (Primary Key)
- `RoleName` (e.g., "User", "Admin")
- `Description`

#### 3. **UserRoles** Table
Many-to-many relationship between Users and Roles:
- `UserRoleId` (Primary Key)
- `UserId` (Foreign Key to Users)
- `RoleId` (Foreign Key to Roles)
- `AssignedDate`
- `AssignedBy`

#### 4. **CompanyAuditLog** Table
Tracks all company-related operations:
- `AuditId` (Primary Key)
- `CompanyId` (Foreign Key to CompanyDet)
- `OperationType` (Create/Update/Delete)
- `PerformedBy` (Username)
- `PerformedDate`
- `OldValues`
- `NewValues`

### Existing Tables (From EDDB Database)

#### **CompanyDet** Table
Core company information with soft-delete support:
- `CompanyDetId` (Primary Key)
- `CompName` (Company Name)
- `CompTypeId` (Company Type)
- `CommRegNo` (Commercial Registration Number)
- `CommRegIssuDate`, `CommRegExpireDate`
- `TaxCardNo`, `TaxCardIssuDate`, `TaxCardExpireDate`
- `IsDeleted` (Soft delete flag) ✨ **Added**
- `DeletedBy`, `DeletedDate` ✨ **Added**

#### **GetDrugsData** View
Consolidated drug information view:
- Drug details (TradeCode, Trade_name, Strength, etc.)
- Company information (Applicant)
- Drug type (DrugTypeName: "Human Pharmaceutical" or "Bio")
- License and registration status
- Generics information

---

## 🚀 Core Functionality

### 1. Company Management Dashboard

#### Features:
- **Dynamic Filtering**: Filter by expiry status (All/Expired/Active/Expiring Soon)
- **Real-time Search**: Server-side search as you type (300ms debounce)
- **Server-Side Pagination**: Efficient data loading (10/25/50/100 per page)
- **Soft Delete**: Non-destructive deletion with audit trail
- **Excel Export**: Export filtered data to .xlsx format

#### Access Control:
- **View**: All authenticated users
- **Add/Edit**: All authenticated users
- **Delete**: Admin only (with confirmation modal)

### 2. Drug Data Management

#### Features:
- **Filter by Drug Type**: Human Pharmaceutical (default) or Bio
- **Real-time Search**: Search by drug name with instant results
- **Server-Side Processing**: Only fetches required page data
- **View Integration**: Consumes data from `GetDrugsData` view

### 3. Audit Logging System

#### Tracked Operations:
- ✅ Company Creation (who created, when, what data)
- ✅ Company Updates (who updated, when, changes made)
- ✅ Company Deletions (who deleted, when, soft-delete flag set)

#### Audit Log Details:
- **Username**: Who performed the action
- **Timestamp**: When the action occurred
- **Operation Type**: Create/Update/Delete
- **Company ID & Name**: Which company was affected
- **Changes**: Old and new values (for updates)

---
