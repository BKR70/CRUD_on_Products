# ProductTrackr 🚀
**ProductTrackr** is a web-based product inventory management system built using **ASP.NET Core MVC**, **Entity Framework Core**, and **SQL Server**. It allows users to perform essential **CRUD operations** on product data with a clean UI, form validation, and role-based access.

## 🚀 Features

### 🗃️ Product Management  
- Add, edit, delete, and view product information.  
- Manage product details like **name**, **category**, **price**, and **stock level**.  
- Each product includes descriptions and optional image links.

### 🔍 Search & Filter  
- Search products by **name**, **category**, or **price range**.  
- Sort product listings by **price**, **availability**, or **alphabetical order**.  

### ✅ Form Validation  
- Input validation for all fields during product creation or update.  
- Prevents incomplete or incorrect entries using both client and server-side checks.

### 👤 User Authentication & Roles  
- Basic login system using **ASP.NET Identity**.  
- Two roles:  
  - **Admin**: Full access to manage products.  
  - **User**: Read-only access to browse product listings.

## ⚡ Installation
1. **Clone the repository**
   ```bash
   git clone https://github.com/BKR70/CRUD_on_Products.git
   cd CRUD_on_Products

2. **Open the project in Visual Studio**
   - Open PropertyManagementSystem.sln
    
3. **Set up the database**
   - Edit `appsettings.json` with your SQL Server connection string.
   - Run the following in **Package Manager Console**:
     ```bash
     Add-Migration InitialCreate
     Update-Database
     ```
   - This will apply Entity Framework migrations and create the required database schema.
     
4. **Run the application**
   - click the **Start** button in Visual Studio.

## 🛠️ Technologies Used
   -    ASP.NET Core MVC
   -    Entity Framework Core
   -    MS SQL Server
