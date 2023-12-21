namespace Salaries.Entities
{
    public class Position(int id, string name, decimal salary)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public decimal Salary { get; set; } = salary;

        public virtual bool IsNull() => false;
    }
}