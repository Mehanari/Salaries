using Salaries.Entities;

namespace Salaries.DAO;

public interface IWorkerDao
{
    List<Worker> GetAllWorkers();
    bool DeleteWorker(int id);
    bool UpdateWorker(Worker worker);
    Worker AddWorker(Worker worker);
    Worker GetWorkerById(int id);
    Worker GetWorkerByFullname(string name, string surname, string patronymic);
}