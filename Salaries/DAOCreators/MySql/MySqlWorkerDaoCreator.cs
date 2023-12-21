using Salaries.Builders;
using Salaries.DAO;
using Salaries.DAO.MySql;

namespace Salaries.DAOCreators.MySql;

public class MySqlWorkerDaoCreator : IWorkerDaoCreator
{
    /// <summary>
    /// Takes MySqlConnectionProvider and IWorkerBuilder from Singleton and creates WorkerDAO with them.
    /// Be sure to register singletons with specified types before using this method.
    /// </summary>
    /// <returns></returns>
    public IWorkerDao Create()
    {
        var connectionProvider = Singleton<MySqlConnectionProvider>.Instance;
        var builder = Singleton<IWorkerBuilder>.Instance;
        return new MySqlWorkerDao(connectionProvider, builder);
    }
}