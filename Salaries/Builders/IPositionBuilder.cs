using Salaries.Entities;

namespace Salaries.Builders
{
    public interface IPositionBuilder
    {
        IPositionBuilder SetId(int id);
        IPositionBuilder SetName(string name);
        IPositionBuilder SetSalary(decimal salary);
        IPositionBuilder Reset();
        Position Build();
    }
}