# Course Management System (ASP.NET Core MVC)

## 📌 Overview
A structured **ASP.NET Core MVC** web application for managing **Courses, Sessions, Users, and Grades**, built with **Three-Tier Architecture** (Presentation Layer, Business Logic Layer, Data Access Layer) and the **Repository Pattern**.  

The project ensures **clean separation of concerns**, **reusability**, and **maintainability**.

---

## ⚙️ Requirements

### 1. Three-Tier Architecture
- **Presentation Layer (UI)**: MVC Controllers + Views (Razor with Tag Helpers).  
- **Business Logic Layer (BLL)**: Services with business rules & validations.  
- **Data Access Layer (DAL)**: Repositories for CRUD operations.  

### 2. Repository Pattern
All data access is abstracted via repositories.  
Examples:  
- `ICourseRepository`  
- `ISessionRepository`  
- `IUserRepository`

### 3. Validation
- ✅ **Data Annotations** for standard validation.  
- ✅ **Custom Validation** (e.g., `NoNumberAttribute` for Course Name).  
- ✅ **Remote Validation** (check uniqueness of Course Name or Email without page reload).  
- ✅ **Client-Side Validation** enabled via unobtrusive validation.  

### 4. Partial Views & Reusability
- Shared partial views for repeated forms.  
  - `_UserForm.cshtml`  
  - `_CourseForm.cshtml`  
- Partial views used in **Create** & **Edit** pages.  

### 5. Pagination & Searching
- Implemented for **Courses, Sessions, Users, and Grades**.  
- Search and filter by **name, category, role, etc.**  
- Example: `Courses?search=math&pageNumber=1&pageSize=10`  

### 6. User Feedback
- Success & error messages shown after actions (Create, Update, Delete).  
- Examples:  
  - `"Course created successfully"`  
  - `"Email already exists"`  
- Implemented with **Bootstrap alerts** or **TempData-based notifications**.  

---

## 🚀 Core Features

### 1. Course Management
- **CRUD**: Add, edit, delete courses.  
- Assign an instructor to a course.  
- Search & paginate courses by name or category.  
- **Validation**:  
  - Name: Required, 3–50 chars, unique (Remote Validation).  
  - Category: Required.  
  - Custom: Name must not contain numbers (`NoNumberAttribute`).  

---

### 2. Session Management
- **CRUD**: Manage sessions for a course.  
- Define **StartDate** and **EndDate**.  
- Search sessions by course name (with pagination).  
- **Validation**:  
  - StartDate: Required, cannot be in the past.  
  - EndDate: Required, must be after StartDate.  
  - CourseId: Required.  

---

### 3. User Management
- **CRUD**: Add, edit, delete users.  
- User fields: **Name, Email, Role (Admin, Instructor, Trainee)**.  
- Search & paginate users by **role/name**.  
- **Validation**:  
  - Name: Required, 3–50 chars.  
  - Email: Required, valid, unique (Remote Validation).  
  - Role: Required.  

---

### 4. Grades Management
- Record a grade for each trainee in a session.  
- View grades per trainee (with pagination).  
- **Validation**:  
  - Value: Required, between 0–100.  
  - SessionId & TraineeId: Required.  

---

## 🛠️ Tech Stack
- **ASP.NET Core MVC**  
- **Entity Framework Core**  
- **Repository Pattern**
- **N-Tier Architecture**
- **Bootstrap 5**  
- **SQL Server**  

--- 
