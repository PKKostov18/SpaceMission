using SpaceMission.Interfaces;
using SpaceMission.Models;

namespace SpaceMission.Core
{
    public class DijkstraPathFinder : IPathFinder
    {
        private readonly int[] dRow = { -1, 1, 0, 0 };
        private readonly int[] dCol = { 0, 0, -1, 1 };

        public MissionResult FindPath(ISpaceMap map, string astronautId, Point start)
        {
            var distances = new Dictionary<Point, int>();
            var previous = new Dictionary<Point, Point>();
            var pq = new PriorityQueue<Point, int>();

            distances[start] = 0;
            pq.Enqueue(start, 0);

            while (pq.Count > 0)
            {
                var current = pq.Dequeue();

                if (current == map.Destination) break;

                for (int i = 0; i < 4; i++)
                {
                    int newRow = current.Row + dRow[i];
                    int newCol = current.Col + dCol[i];
                    Point neighbor = new Point(newRow, newCol);

                    if (!map.IsValidMove(newRow, newCol)) continue;

                    int newDist = distances[current] + map.GetMovementCost(newRow, newCol);

                    if (!distances.ContainsKey(neighbor) || newDist < distances[neighbor])
                    {
                        distances[neighbor] = newDist;
                        previous[neighbor] = current;
                        pq.Enqueue(neighbor, newDist);
                    }
                }
            }

            return GenerateResult(map, astronautId, start, previous, distances);
        }

        private MissionResult GenerateResult(ISpaceMap map, string astronautId, Point start, Dictionary<Point, Point> previous, Dictionary<Point, int> distances)
        {
            var result = new MissionResult { AstronautId = astronautId };

            if (!distances.ContainsKey(map.Destination))
            {
                result.IsSuccessful = false;
                return result;
            }

            result.IsSuccessful = true;
            result.TotalCost = distances[map.Destination];

            Point current = map.Destination;
            while (current != start)
            {
                result.Path.Add(current);
                current = previous[current];
            }
            result.Path.Reverse();

            result.VisualMap = new string[map.Rows, map.Cols];
            Array.Copy(map.Grid, result.VisualMap, map.Grid.Length);

            foreach (var step in result.Path)
            {
                if (step != map.Destination)
                {
                    result.VisualMap[step.Row, step.Col] = "*";
                }
            }

            return result;
        }
    }
}