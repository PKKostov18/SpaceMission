namespace SpaceMission.Models
{
    public class MissionResult
    {
        public string AstronautId { get; set; } = string.Empty;
        public bool IsSuccessful { get; set; }
        public int TotalCost { get; set; }
        public List<Point> Path { get; set; } = new();
        public string[,] VisualMap { get; set; } = new string[0, 0];
    }
}