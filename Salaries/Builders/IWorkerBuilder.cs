using Salaries.Entities;

namespace Salaries.Builders
{
    public interface IWorkerBuilder
    {
        IWorkerBuilder SetId(int id);
        IWorkerBuilder SetFirstName(string firstName);
        IWorkerBuilder SetSurname(string surname);
        IWorkerBuilder SetPatronymic(string patronymic);
        IWorkerBuilder SetPositionId(int positionId);
        IWorkerBuilder Reset();
        Worker Build();
    }
}