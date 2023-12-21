using Salaries.Entities;

namespace Salaries.Builders.DefaultBuilders;

public class DefaultObjectiveBuilder(int defaultId, string defaultName, string defaultDescription)
    : IObjectiveBuilder
{
    private Objective _objective = new(defaultId, defaultName, defaultDescription);

    public IObjectiveBuilder SetId(int id)
    {
        _objective.Id = id;
        return this;
    }

    public IObjectiveBuilder SetName(string name)
    {
        _objective.Name = name;
        return this;
    }

    public IObjectiveBuilder SetDescription(string description)
    {
        _objective.Description = description;
        return this;
    }

    public IObjectiveBuilder Reset()
    {
        _objective = new Objective(defaultId, defaultName, defaultDescription);
        return this;
    }

    public Objective Build()
    {
        var objective = _objective;
        Reset();
        return objective;
    }
}