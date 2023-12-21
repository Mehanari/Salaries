using Salaries.Entities;

namespace Salaries.DAO;

public interface IObjectiveDao
{
    List<Objective> GetAllObjectives();
    bool DeleteObjective(int id);
    bool UpdateObjective(Objective objective);
    Objective AddObjective(Objective objective);
    Objective GetObjectiveById(int id);
    List<Objective> GetObjectivesByName(string name);
}