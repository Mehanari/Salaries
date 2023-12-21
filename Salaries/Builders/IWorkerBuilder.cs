using Salaries.Entities;

namespace Salaries.Builders;

public interface IWorkerBuilder
{
    IWorkerBuilder SetId(int id);
    IWorkerBuilder SetName(string name);
    IWorkerBuilder SetSurname(string surname);
    IWorkerBuilder SetPatronymic(string patronymic);
    IWorkerBuilder SetPositionId(int positionId);
    IWorkerBuilder Reset();
    Worker Build();
}