using Salaries.Builders;
using Salaries.DAO;
using Salaries.DAO.MySql;

namespace Salaries.DAOCreators.MySql;

public class MySqlObjectiveDaoCreator : IObjectiveDaoCreator
{
    /// <summary>
    /// Takes MySqlConnectionProvider and IObjectiveBuilder from Singleton and creates ObjectiveDAO with them.
    /// Be sure to register singletons with specified types before using this method.
    /// </summary>
    /// <returns></returns>
    public IObjectiveDao Create()
    {
        var connectionProvider = Singleton<MySqlConnectionProvider>.Instance;
        var builder = Singleton<IObjectiveBuilder>.Instance;
        return new MySqlObjectiveDao(connectionProvider, builder);
    }
}