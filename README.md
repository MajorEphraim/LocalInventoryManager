# Local Inventory Analytics Manager

A lightweight, high-performance desktop application built using **WPF (Windows Presentation Foundation)** and **C#** to track local product inventory, monitor warehouse stock metrics, and manage asset values in real-time. 

The application utilizes **Entity Framework Core** paired with a local **SQLite** instance for zero-configuration data persistence.

---

## 🚀 Features

* **Real-Time Analytics Metrics:** Instantly monitors Total Asset Value (ZAR/R), Total Stock Units, and running Low Stock Alerts.
* **Product Profile Management:** Full CRUD (Create, Read, Update, Delete) lifecycle handling via a dedicated inline management panel.
* **Local Database Auto-Provisioning:** Seamless zero-setup environment infrastructure using an auto-generating SQLite database engine.

---

## 🛠️ Architecture & Tech Stack

* **Framework:** .NET 8.0 (Windows Desktop WPF)
* **Pattern:** MVVM (Model-View-ViewModel) Architecture 
* **Database ORM:** Entity Framework Core 8.0
* **Database Engine:** SQLite (Local Isolated Sandbox)
* **Development IDE:** Visual Studio 2026 / 2022

### Project Structure
```text
LocalInventoryManager/
│
├── Models/
│   ├── InventoryDbContext.cs    # EF Core database context configuration
│   └── Product.cs               # Data Model implementing INotifyPropertyChanged
│
├── ViewModels/
│   └── MainViewModel.cs         # Core UI logic, metrics generation, and state metrics
│
├── Views/
│   ├── MainWindow.xaml          # Main application user interface
│   └── MainWindow.xaml.cs       # View layout initialization handlers
│
├── App.xaml / App.xaml.cs       # Global application life-cycle management
└── LocalInventoryManager.csproj # Modern .NET PackageReference configuration
