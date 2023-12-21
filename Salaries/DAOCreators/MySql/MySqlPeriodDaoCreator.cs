using Salaries.Builders;
using Salaries.DAO;
using Salaries.DAO.MySql;

namespace Salaries.DAOCreators.MySql;

public class MySqlPeriodDaoCreator : IPeriodDaoCreator
{
    /// <summary>
    /// Takes MySqlConnectionProvider and IPeriodBuilder from Singleton and creates PeriodDAO with them.
    /// Be sure to register singletons with specified types before using this method.
    /// </summary>
    /// <returns></returns>
    public IPeriodDao Create()
    {
        var connectionProvider = Singleton<MySqlConnectionProvider>.Instance;
        var builder = Singleton<IPeriodBuilder>.Instance;
        return new MySqlPeriodDao(connectionProvider, builder);
    }
}