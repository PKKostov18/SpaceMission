using SpaceMission.Interfaces;

namespace SpaceMission.Core
{
    public static class MapGenerator
    {
        public static ISpaceMap GenerateRandomMap(int rows, int cols, int obstaclePercentage)
        {
            string[,] grid = new string[rows, cols];
            var rand = new Random();

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    int chance = rand.Next(1, 101);
                    if (chance <= obstaclePercentage)
                    {
                        grid[r, c] = rand.Next(1, 101) <= 80 ? "X" : "D";
                    }
                    else
                    {
                        grid[r, c] = "O";
                    }
                }
            }

            PlaceEntity(grid, rows, cols, rand, "F");

            int astronautsCount = rand.Next(1, 4);
            for (int i = 1; i <= astronautsCount; i++)
            {
                PlaceEntity(grid, rows, cols, rand, $"S{i}");
            }

            return new SpaceMap(rows, cols, grid);
        }

        private static void PlaceEntity(string[,] grid, int rows, int cols, Random rand, string entity)
        {
            while (true)
            {
                int r = rand.Next(rows);
                int c = rand.Next(cols);
                if (grid[r, c] == "O" || grid[r, c] == "X" || grid[r, c] == "D")
                {
                    grid[r, c] = entity;
                    break;
                }
            }
        }
    }
}