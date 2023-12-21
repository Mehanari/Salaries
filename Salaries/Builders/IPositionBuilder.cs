using Salaries.Entities;

namespace Salaries.Builders;

public interface IPositionBuilder
{
    IPositionBuilder SetId(int id);
    IPositionBuilder SetName(string name);
    IPositionBuilder SetRank(int rank);
    IPositionBuilder SetSalary(decimal salary);
    IPositionBuilder Reset();
    Position Build();
}