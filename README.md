# 🚀 SPACE 2026 - Cosmic Navigation System

A robust C# .NET console application developed for the SPACE 2026 Mission Control. The system calculates the optimal and safest routes for stranded astronauts to return to the Space Station while navigating through hazardous cosmic environments.

## ✨ Features

### Primary Objectives Completed
- **Shortest Path Calculation:** Efficiently calculates the shortest path for up to 3 astronauts (S1, S2, S3) to the Space Station (F).
- **Hazard Avoidance:** Completely avoids impassable Asteroids (X).
- **Visual Output:** Displays a clear, step-by-step visual representation of each astronaut's safe route using *.
- **Result Sorting:** Automatically ranks astronauts by the shortest path length in ascending order.
- **Error Handling & Validation:** Robust input validation for grid dimensions and structures.

### Bonus Objectives Completed
- [x] **Space Debris Navigation:** Added support for Space Debris (D), which is passable but costs 2 steps instead of 1. The algorithm correctly calculates the shortest path based on *total cost*, not just the number of steps.
- [x] **Advanced OOP Principles:** Architected using the Strategy Pattern (IPathFinder) and Dependency Inversion (ISpaceMap). The pathfinding algorithm can be swapped entirely (e.g., to A*) without modifying the core application logic.
- [x] **Dynamic Map Generation:** Includes an interactive UI menu to randomly generate cosmic maps based on custom dimensions and obstacle density.
- [x] **Email Reporting:** Integrated an SMTP Email Service to send mission outcome reports directly to Mission Control.

## 🏗️ Architecture & Technical Details

- **Algorithm:** Uses Dijkstra's Algorithm paired with a .NET PriorityQueue. This ensures optimal performance even on large grids while accurately processing variable movement weights (Space Debris).
- **Design Patterns:** Strategy Pattern, Dependency Injection (SOLID principles).
- **Language/Framework:** C# 10+, .NET 6.0 (or newer).

## ⚙️ Getting Started

### Prerequisites
- .NET SDK (Version 6.0 or higher)
- Visual Studio 2022 / VS Code (Optional, for code review)

### How to Run
1. Clone the repository to your local machine.
2. Navigate to the project directory via the terminal/command prompt.
3. Build and run the application using the .NET CLI by typing `dotnet build` and then `dotnet run`.
Alternatively, open the .sln or .csproj file in Visual Studio 2022 and press Start (F5).

## 🎮 Usage Guide

Upon launching the application, you will be greeted by the interactive Mission Control Menu:

1. **Enter manual cosmic map:** Allows you to input custom grid dimensions and construct the cosmic map row by row.
2. **Generate random cosmic map:** Prompts you for grid size and obstacle percentage, then automatically generates a valid mission scenario.
3. **Exit:** Closes the application.

*Note on Email Feature: After a mission completes, you will be prompted to optionally send an email report. If using Gmail as the sender, ensure you use a generated "App Password" due to Google's SMTP security policies.*

## 🗺️ Legend

| Symbol | Description | Movement Cost |
| :---: | :--- | :---: |
| S1, S2, S3 | Astronauts (Starting Points) | 1 |
| F | Space Station (Destination) | - |
| O (or 0) | Open Space (Safe) | 1 |
| X | Asteroid (Impassable hazard) | ∞ |
| D | Space Debris (Slow movement) | 2 |
| * | Calculated Safe Path | - |

---
*Developed as an assessment task for the Tech Analyst Internship.*
