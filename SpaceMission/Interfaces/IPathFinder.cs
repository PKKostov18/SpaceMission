using SpaceMission.Models;

namespace SpaceMission.Interfaces
{
    public interface IPathFinder
    {
        MissionResult FindPath(ISpaceMap map, string astronautId, Point start);
    }
}