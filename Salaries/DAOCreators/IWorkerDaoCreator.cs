using Salaries.DAO;

namespace Salaries.DAOCreators;

public interface IWorkerDaoCreator
{
    IWorkerDao Create();
}