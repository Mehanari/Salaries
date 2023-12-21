using Salaries.Builders;
using Salaries.DAO;
using Salaries.DAO.MySql;

namespace Salaries.DAOCreators.MySql;

public class MySqlResultDaoCreator : IResultDaoCreator
{
    /// <summary>
    /// Takes MySqlConnectionProvider and IResultBuilder from Singleton and creates ResultDAO with them.
    /// Be sure to register singletons with specified types before using this method.
    /// </summary>
    /// <returns></returns>
    public IResultDao Create()
    {
        var connectionProvider = Singleton<MySqlConnectionProvider>.Instance;
        var builder = Singleton<IResultBuilder>.Instance;
        return new MySqlResultDao(connectionProvider, builder);
    }
}