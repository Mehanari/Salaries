namespace Salaries.Entities;

public class Worker(int id, int positionId, string firstName, string surname, string patronymic)
{
    public int Id { get; set; } = id;
    public int PositionId { get; set; } = positionId;
    public string FirstName { get; set; } = firstName;
    public string Surname { get; set; } = surname;
    public string Patronymic { get; set; } = patronymic;

    public virtual bool IsNull() => false;
}