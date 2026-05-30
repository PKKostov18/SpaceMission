using SpaceMission.Core;
using SpaceMission.Interfaces;
using SpaceMission.Models;

namespace SpaceMission
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=====================================");
                Console.WriteLine("      SPACE 2026 MISSION CONTROL     ");
                Console.WriteLine("=====================================\n");
                Console.WriteLine("1. Enter manual cosmic map");
                Console.WriteLine("2. Generate random cosmic map (Bonus)");
                Console.WriteLine("3. Exit");
                Console.Write("\nSelect an option: ");

                string? choice = Console.ReadLine();

                try
                {
                    if (choice == "1")
                    {
                        ISpaceMap map = ReadMapFromInput();
                        ExecuteMission(map);
                    }
                    else if (choice == "2")
                    {
                        ISpaceMap map = GenerateMapInteractively();
                        ExecuteMission(map);
                    }
                    else if (choice == "3")
                    {
                        Console.WriteLine("Exiting Mission Control...");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Press any key to try again.");
                        Console.ReadKey();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[ERROR] {ex.Message}");
                    Console.WriteLine("Press any key to return to menu...");
                    Console.ReadKey();
                }
            }
        }

        static void ExecuteMission(ISpaceMap map)
        {
            Console.WriteLine("\n--- INITIAL MAP ---");
            PrintGrid(map.Grid);

            IPathFinder pathFinder = new DijkstraPathFinder();
            var astronauts = map.GetAstronauts();
            var results = new List<MissionResult>();

            foreach (var (id, position) in astronauts)
            {
                var result = pathFinder.FindPath(map, id, position);
                results.Add(result);
            }

            DisplayResults(results);

            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }

        static ISpaceMap GenerateMapInteractively()
        {
            Console.Clear();
            Console.WriteLine("--- GENERATE RANDOM MAP ---");

            Console.Write("Enter rows: ");
            int rows = int.Parse(Console.ReadLine() ?? "10");

            Console.Write("Enter columns: ");
            int cols = int.Parse(Console.ReadLine() ?? "10");

            Console.Write("Enter obstacle percentage (1-99): ");
            int percentage = int.Parse(Console.ReadLine() ?? "30");

            return MapGenerator.GenerateRandomMap(rows, cols, percentage);
        }

        static ISpaceMap ReadMapFromInput()
        {
            Console.Clear();
            Console.WriteLine("--- MANUAL MAP ENTRY ---");
            Console.Write("Map rows (M): ");
            if (!int.TryParse(Console.ReadLine(), out int m) || m < 2 || m > 100)
                throw new ArgumentException("Rows must be between 2 and 100.");

            Console.Write("Map columns (N): ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n < 2 || n > 100)
                throw new ArgumentException("Columns must be between 2 and 100.");

            Console.WriteLine("Enter cosmic map (row by row):");
            string[,] grid = new string[m, n];

            for (int r = 0; r < m; r++)
            {
                string[] rowData = Console.ReadLine()?.Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
                if (rowData.Length != n)
                    throw new ArgumentException($"Row {r + 1} does not contain exactly {n} columns.");

                for (int c = 0; c < n; c++)
                {
                    grid[r, c] = rowData[c].ToUpper() == "0" ? "O" : rowData[c].ToUpper();
                }
            }

            return new SpaceMap(m, n, grid);
        }

        static void DisplayResults(List<MissionResult> results)
        {
            Console.WriteLine("\n--- MISSION RESULTS ---\n");

            var failedMissions = results.Where(r => !r.IsSuccessful).OrderBy(r => r.AstronautId);
            foreach (var fail in failedMissions)
            {
                Console.WriteLine($"Mission failed — Astronaut {fail.AstronautId} lost in space!");
            }

            var successfulMissions = results.Where(r => r.IsSuccessful).OrderBy(r => r.TotalCost);
            foreach (var success in successfulMissions)
            {
                Console.WriteLine($"Astronaut {success.AstronautId} - Shortest path: {success.TotalCost} steps/cost");
                PrintGrid(success.VisualMap);
                Console.WriteLine();
            }
        }

        static void PrintGrid(string[,] grid)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Console.Write(grid[r, c] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}