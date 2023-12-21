using MySql.Data.MySqlClient;

namespace Salaries;

public class MySqlConnectionProvider(string server, string username, string password, string database)
{
    public MySqlConnection GetConnection()
    {
        var builder = new MySqlConnectionStringBuilder
        {
            Server = server,
            UserID = username,
            Password = password,
            Database = database
        }; 
        return new MySqlConnection(builder.ConnectionString);
    }
}