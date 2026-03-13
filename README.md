# PioneerSolution - Employee Management System

A modern, responsive ASP.NET Core MVC application for managing employees with dynamic custom properties.

## 📋 Overview

PioneerSolution is an employee management system that allows you to:

- **Manage Employees**: Create, edit, and view employee records with unique codes and names
- **Dynamic Properties**: Define custom properties (String, Integer, Date, Dropdown) that appear on employee forms
- **Flexible Data**: Each employee can have different custom property values based on your definitions

## 🏗️ Architecture

This project follows a **Clean Architecture** pattern with three main layers:

```
PioneerSolution/
├── PioneerSolution.Core/           # Domain layer - Business entities and interfaces
│   ├── Models/
│   │   ├── Employee.cs             # Employee entity
│   │   ├── PropertyDefinition.cs   # Dynamic property definition
│   │   └── EmployeePropertyValue.cs # Property value for each employee
│   └── Interfaces/                 # Repository and service interfaces
│
├── PioneerSolution.Data/            # Data access layer
│   ├── Context/                     # Entity Framework DbContext
│   ├── Repositories/               # Generic repository implementation
│   └── Migrations/                  # Database migrations
│
├── PioneerSolution.Services/        # Business logic layer
│   ├── DTOs/                        # Data Transfer Objects
│   └── Services/                    # Business service implementations
│
└── PioneerSolution.Web.UI/          # Presentation layer (ASP.NET Core MVC)
    ├── Controllers/                 # MVC Controllers
    ├── Views/                        # Razor Views
    └── wwwroot/                     # Static files (CSS, JS)
```

## 🛠️ Technology Stack

| Component | Technology |
|-----------|------------|
| **Framework** | ASP.NET Core 8.0 |
| **Database** | Microsoft SQL Server |
| **ORM** | Entity Framework Core |
| **Frontend** | Bootstrap 5, Font Awesome 6 |
| **Architecture** | MVC (Model-View-Controller) |

## 🚀 Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- SQL Server (LocalDB or Express)
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd PioneerSolutionTask
   ```

2. **Update connection string**
   
   Edit `PioneerSolution.Web.UI/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=PioneerSolution;Trusted_Connection=True;TrustServerCertificate=True;"
     }
   }
   ```

3. **Run database migrations**
   ```bash
   cd PioneerSolution.Web.UI
   dotnet ef database update
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Open in browser**
   
   Navigate to: `https://localhost:7000` (or the port shown in console)

## 📖 Features

### Employee Management
- **View All Employees**: See a list of all employees with their custom property values
- **Add Employee**: Create new employee with code, name, and dynamic properties
- **Edit Employee**: Update employee information and custom property values

### Property Definitions
- **Create Properties**: Define custom properties with types:
  - `String` - Free text input
  - `Integer` - Numeric input
  - `Date` - Date picker
  - `Dropdown` - Select from predefined options
- **Required Fields**: Mark properties as required
- **Delete Properties**: Remove property definitions

## 🎨 UI/UX Features

The application features a modern, professional design with:

- **Responsive Design**: Works on desktop, tablet, and mobile devices
- **Modern Navigation**: Fixed navbar with gradient theme
- **Professional Cards**: Elevated cards with hover effects
- **Animated Transitions**: Smooth animations on page load
- **Custom Styling**: Branded color scheme with accent colors
- **Icons**: Font Awesome icons throughout the interface

## 📁 Project Structure

| File/Folder | Description |
|-------------|-------------|
| `Program.cs` | Application entry point with DI configuration |
| `_Layout.cshtml` | Main layout with navbar and footer |
| `site.css` | Custom professional styling |
| `EmployeeController.cs` | Employee CRUD operations |
| `PropertyController.cs` | Property definition management |
| `EmployeeService.cs` | Employee business logic |
| `PropertyDefinitionService.cs` | Property business logic |

## 🔧 Configuration

### Database Connection

Located in `PioneerSolution.Web.UI/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=PioneerSolution;Trusted_Connection=True;"
  }
}
```

### Environment

- Development: Detailed error pages enabled
- Production: Error handling middleware configured

## 📝 License

This project is for demonstration/educational purposes.

## 👤 Author

Developed as a demonstration of ASP.NET Core MVC with Entity Framework Core and dynamic entity design patterns.

---

**Note**: This project uses Entity Framework Core with Code-First migrations. Make sure to run `dotnet ef database update` after any changes to the model classes.
