using Salaries.Builders;
using Salaries.DAO;
using Salaries.DAO.MySql;

namespace Salaries.DAOCreators.MySql;

public class MySqlPositionDaoCreator : IPositionDaoCreator
{
    /// <summary>
    /// Takes MySqlConnectionProvider and IPositionBuilder from Singleton and creates PositionDAO with them.
    /// Be sure to register singletons with specified types before using this method.
    /// </summary>
    /// <returns></returns>
    public IPositionDao Create()
    {
        var connectionProvider = Singleton<MySqlConnectionProvider>.Instance;
        var builder = Singleton<IPositionBuilder>.Instance;
        return new MySqlPositionDao(connectionProvider, builder);
    }
}