using Salaries.Entities;

namespace Salaries.Builders
{
    public interface IObjectiveBuilder
    {
        IObjectiveBuilder SetId(int id);
        IObjectiveBuilder SetName(string name);
        IObjectiveBuilder SetDescription(string description);
        IObjectiveBuilder Reset();
        Objective Build();
    }
}