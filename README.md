# 🧾 Invoice Web Application - ASP.NET Core MVC (.NET 7)

A professional Invoice Management System built using **ASP.NET Core MVC**, **Dapper**, and **SQL Server**. The project follows clean architecture principles and provides full **multilingual support** (English 🇬🇧 & Arabic 🇸🇦).

> Developed as part of the **IT Roots .NET Junior Task**.

---

## 🚀 Features

- 🔐 **Authentication**
  - User Registration, Login, Logout
  - Google reCAPTCHA (on Login)
  - Simulated Email Verification via Token

- 🧾 **Invoice Management**
  - Create, Edit, and Delete Invoices
  - Add multiple items per invoice
  - View invoice details
  - Pagination and Search by Invoice ID

- 🌍 **Multilingual Support**
  - Full support for **English 🇬🇧** and **Arabic 🇸🇦**
  - Dynamic RTL/LTR switching
  - Localization via `.resx` files and `IStringLocalizer`

- ✅ **Validation**
  - Client-side and server-side validation
  - Custom error messages and input checks

- 🧱 **Clean Architecture (3-Tier)**
  - Controller → Service → Repository
  - Dapper for data access
  - Separation of ViewModels and Domain Models

---

## 🛠️ Tech Stack

| Layer      | Technology                      |
|------------|----------------------------------|
| Frontend   | Razor Views, Bootstrap 5         |
| Backend    | ASP.NET Core MVC (.NET 7)        |
| ORM        | Dapper                           |
| Database   | SQL Server                       |
| Security   | Cookie Authentication, reCAPTCHA |
| Localization | IStringLocalizer, .resx files  |

---

## 📥 How to Clone and Run the Project

Follow these steps to get the project up and running locally on your machine.

### 🔁 Step 0: Clone the Repository

```bash
git clone https://github.com/AhmedHakim0/InvoiceApp-MVC-Dapper.git
cd InvoiceApp-MVC-Dapper

## ✅ Step 1: Restore the Database

The project uses a pre-configured SQL Server database backup (`.bak` file). Here's how to restore it:

1. Open **SQL Server Management Studio (SSMS)**
2. Right-click on **Databases** → **Restore Database**
3. Choose:
   - **Source**: Select **Device**
   - Click **Add** and locate the backup file:
     ```
     InvoiceWebApp/DatabaseBackup/InvoiceWebAppDB.bak
     ```
   - Set the **Database name** to: `InvoiceWebAppDB`
4. In the **Files** tab, confirm MDF/LDF paths are valid
5. Click **OK** to complete the restore

> 💡 Make sure SQL Server has permission to access the backup file location.

---

## 🧱 Step 2: Open the Project

1. Open the solution folder in **Visual Studio 2022 or later**
2. Open the file `InvoiceWebApp.csproj`
3. Wait for dependencies to restore automatically

### 🔧 Update the Connection String

Before running the project, make sure to update the **connection string** in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=InvoiceWebAppDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
---

## ▶️ Step 3: Run the Application

1. Make sure the restored database is up and running
2. Press `Ctrl + F5` to run the project without Debug
3. The browser will launch automatically at:


---

## 🌍 Optional: Language Switching

- Available in the navigation bar
- Toggle between:
  - **English 🇬🇧**
  - **Arabic ar**

- RTL layout is applied dynamically when switching to Arabic

---

## 📈 Project Status

| Feature                      | Status |
|------------------------------|--------|
| Register/Login/Logout        | ✅     |
| CAPTCHA + Email Verification | ✅     |
| Invoice CRUD Operations      | ✅     |
| Search & Pagination          | ✅     |
| Multilingual Support         | ✅     |
| Clean Architecture           | ✅     |

---

## 🖼️ Screenshots

![Home Page](https://raw.githubusercontent.com/AhmedHakim0/InvoiceApp-MVC-Dapper/main/screenshots/HomePageEN.png)

### 🧾 Login/Registration
![Login](https://github.com/AhmedHakim0/InvoiceApp-MVC-Dapper/blob/main/screenshots/LoginEN.png?raw=true)
![Registration](https://github.com/AhmedHakim0/InvoiceApp-MVC-Dapper/blob/main/screenshots/Registration.png?raw=true)

### 🧾 Create Invoice
![Create Invoice](https://github.com/AhmedHakim0/InvoiceApp-MVC-Dapper/blob/main/screenshots/AddNewInvoice.png?raw=true)

### 📃 Invoice List
![Invoice List](https://github.com/AhmedHakim0/InvoiceApp-MVC-Dapper/blob/main/screenshots/InvoicesEN.png?raw=true)

### 🌐 RTL Arabic View
![Home Page Arabic](https://raw.githubusercontent.com/AhmedHakim0/InvoiceApp-MVC-Dapper/main/screenshots/HomePageAR.png)
![Login Arabic](https://github.com/AhmedHakim0/InvoiceApp-MVC-Dapper/blob/main/screenshots/LoginAR.png?raw=true)
![Invoice List Arabic](https://github.com/AhmedHakim0/InvoiceApp-MVC-Dapper/blob/main/screenshots/InvoicesAR.png?raw=true)



