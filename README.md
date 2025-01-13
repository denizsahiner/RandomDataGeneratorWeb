# Random Data Generator

Random Data Generator is a web-based application built with ASP.NET Core MVC to generate random datasets based on user-defined fields and types. The application allows exporting the generated data in various formats such as CSV, Excel, SQL, and JSON.

---

## Features

- Add custom fields with specific data types (e.g., String, Number, Boolean, Date, etc.).
- Generate random datasets with a user-defined number of rows.
- Export datasets in multiple formats:
  - CSV
  - Excel
  - SQL
  - JSON

---

## Requirements

- .NET 6 or later
- Visual Studio 2022 or another .NET-compatible IDE
- SQLite database

---

## Installation

1. Clone this repository:
   ```bash
   git clone https://github.com/denizsahiner/RandomDataGeneratorWeb.git
   ```
2. Navigate to the project directory:
   ```bash
   cd RandomDataGeneratorWeb
   ```
3. Open the solution file (`RandomDataGeneratorWeb.sln`) in Visual Studio.
4. Restore dependencies:
   ```bash
   dotnet restore
   ```
5. Update the connection string in `Program.cs` if needed:
   ```csharp
   options.UseSqlite("Data Source=RandomDataGeneratorDatabase.db");
   ```
6. Build and run the application:
   ```bash
   dotnet run
   ```

---

## Usage

1. Open the application in your browser (default: `https://localhost:5001`).
2. Add fields and their data types using the UI.
3. Enter the number of rows to generate.
4. Click "Generate Dataset" to view the data in a table.
5. Use the "Download Dataset" button to export the data in your chosen format.

---

## Project Structure

- **Controllers**: Handles HTTP requests and business logic.
- **Views**: Contains Razor files (`.cshtml`) for the user interface.
- **Services**: Includes logic for data generation and export.
- **wwwroot**: Contains static files (CSS, JS).

---

## Screenshots

### Main Page
![Field Creation]![screenshot_1]![ss1](https://github.com/user-attachments/assets/953b5c8e-cca2-42f6-86c0-4125d9eca06e)



### Data Creation
![Data Table]![screenshot_2]![ss2](https://github.com/user-attachments/assets/47c26212-ba54-40b1-aea2-5012f0a21066)



### Export Options
![Export Options]![screenshot_3]![ss3](https://github.com/user-attachments/assets/4f950fea-3120-414c-975b-2878db628e93)



---



## Contact

- **Author**: [Deniz Kaan Þahiner](https://github.com/denizsahiner)
- **Email**: dksahiner@gmail.com
- **Author**: [Erdem Diri](https://github.com/ErdemDiri)
- **Email**: erdem.diri@hotmail.com
