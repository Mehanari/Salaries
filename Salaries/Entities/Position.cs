namespace Salaries.Entities;

public class Position(int id, string name, int rank, decimal salary)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public int Rank { get; set; } = rank;
    public decimal Salary { get; set; } = salary;

    public virtual bool IsNull() => false;
}