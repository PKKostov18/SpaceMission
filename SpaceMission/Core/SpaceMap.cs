using SpaceMission.Interfaces;
using SpaceMission.Models;

namespace SpaceMission.Core
{
    public class SpaceMap : ISpaceMap
    {
        public int Rows { get; }
        public int Cols { get; }
        public string[,] Grid { get; }
        public Point Destination { get; private set; } = new(-1, -1);

        public SpaceMap(int rows, int cols, string[,] grid)
        {
            Rows = rows;
            Cols = cols;
            Grid = grid;
            FindEntities();
        }

        private void FindEntities()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    if (Grid[r, c] == "F") Destination = new Point(r, c);
                }
            }
        }

        public List<(string Id, Point Position)> GetAstronauts()
        {
            var astronauts = new List<(string, Point)>();
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    if (Grid[r, c].StartsWith("S"))
                    {
                        astronauts.Add((Grid[r, c], new Point(r, c)));
                    }
                }
            }
            return astronauts;
        }

        public bool IsValidMove(int row, int col)
        {
            if (row < 0 || row >= Rows || col < 0 || col >= Cols) return false;
            if (Grid[row, col] == "X") return false;
            return true;
        }

        public int GetMovementCost(int row, int col)
        {
            if (Grid[row, col] == "D") return 2;
            return 1;
        }
    }
}