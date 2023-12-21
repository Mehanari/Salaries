using Salaries.Entities;

namespace Salaries.DAO;

public interface IPositionDao
{
    List<Position> GetAllPositions();
    bool DeletePosition(int id);
    bool UpdatePosition(Position position);
    Position AddPosition(Position position);
    Position GetPositionById(int id);
    Position GetPositionByRankAndName(string name, int rank);
}