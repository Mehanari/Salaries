namespace Salaries.Entities.NullEntities
{
    public sealed class NullWorker() : Worker(0, 0, "No FirstName", "No Surname", "No Patronymic")
    {
        public override bool IsNull() => true;
    }
}