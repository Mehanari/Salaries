namespace Salaries.Entities
{
    public class Objective(int id, string name, string description)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;

        public virtual bool IsNull() => false;
    }
}