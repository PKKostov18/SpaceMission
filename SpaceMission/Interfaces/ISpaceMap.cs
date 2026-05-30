using SpaceMission.Models;

namespace SpaceMission.Interfaces
{
    public interface ISpaceMap
    {
        int Rows { get; }
        int Cols { get; }
        string[,] Grid { get; }
        Point Destination { get; }
        List<(string Id, Point Position)> GetAstronauts();
        bool IsValidMove(int row, int col);
        int GetMovementCost(int row, int col);
    }
}