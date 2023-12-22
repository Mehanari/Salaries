using MySql.Data.MySqlClient;
using Salaries.Builders;
using Salaries.Entities;
using Salaries.Entities.NullEntities;

namespace Salaries.DAO.MySql;

public class MySqlPositionDao(MySqlConnectionProvider connectionProvider, IPositionBuilder builder)
    : IPositionDao
{
    private const string GetAllPositionsQuery = "SELECT * FROM position";
    private const string DeletePositionQuery = "DELETE FROM position WHERE id = @id";
    private const string UpdatePositionQuery = "UPDATE position SET `name` = @name, `rank` = @rank, salary = @salary WHERE id = @id";
    private const string AddPositionQuery = "INSERT INTO position (`name`, `rank`, salary) VALUES (@name, @rank, @salary)";
    private const string GetPositionByIdQuery = "SELECT * FROM position WHERE id = @id";
    private const string GetPositionByRankAndNameQuery = "SELECT * FROM position WHERE `name` = @name AND `rank` = @rank";

    public List<Position> GetAllPositions()
    {
        var connection = connectionProvider.GetConnection();
        try
        {
            connection.Open();
            var command = new MySqlCommand(GetAllPositionsQuery, connection);
            var reader = command.ExecuteReader();
            var positions = new List<Position>();
            while (reader.Read())
            {
                builder
                    .SetId(reader.GetInt32("id"))
                    .SetName(reader.GetString("name"))
                    .SetRank(reader.GetInt32("rank"))
                    .SetSalary(reader.GetDecimal("salary"));
                var position = builder.Build();
                positions.Add(position);
            }
            return positions;
        }
        finally
        {
            connection.Close();
        }
    }

    public bool DeletePosition(int id)
    {
        var connection = connectionProvider.GetConnection();
        try
        {
            connection.Open();
            var command = new MySqlCommand(DeletePositionQuery, connection);
            command.Parameters.AddWithValue("@id", id);
            return command.ExecuteNonQuery() > 0;
        }
        finally
        {
            connection.Close();
        }
    }

    public bool UpdatePosition(Position position)
    {
        var connection = connectionProvider.GetConnection();
        try
        {
            connection.Open();
            var command = new MySqlCommand(UpdatePositionQuery, connection);
            command.Parameters.AddWithValue("@id", position.Id);
            command.Parameters.AddWithValue("@name", position.Name);
            command.Parameters.AddWithValue("@rank", position.Rank);
            command.Parameters.AddWithValue("@salary", position.Salary);
            return command.ExecuteNonQuery() > 0;
        }
        finally
        {
            connection.Close();
        }
    }

    public Position AddPosition(Position position)
    {
        var connection = connectionProvider.GetConnection();
        try
        {
            connection.Open();
            var command = new MySqlCommand(AddPositionQuery, connection);
            command.Parameters.AddWithValue("@name", position.Name);
            command.Parameters.AddWithValue("@rank", position.Rank);
            command.Parameters.AddWithValue("@salary", position.Salary);
            command.ExecuteNonQuery();
            var id = (int)command.LastInsertedId;
            return GetPositionById(id);
        }
        finally
        {
            connection.Close();
        }
    }

    public Position GetPositionById(int id)
    {
        var connection = connectionProvider.GetConnection();
        try
        {
            connection.Open();
            var command = new MySqlCommand(GetPositionByIdQuery, connection);
            command.Parameters.AddWithValue("@id", id);
            var reader = command.ExecuteReader();
            if (!reader.Read()) return new NullPosition();
            builder
                .SetId(reader.GetInt32("id"))
                .SetName(reader.GetString("name"))
                .SetRank(reader.GetInt32("rank"))
                .SetSalary(reader.GetDecimal("salary"));
            return builder.Build();
        }
        finally
        {
            connection.Close();
        }
    }

    public Position GetPositionByRankAndName(string name, int rank)
    {
        var connection = connectionProvider.GetConnection();
        try
        {
            connection.Open();
            var command = new MySqlCommand(GetPositionByRankAndNameQuery, connection);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@rank", rank);
            var reader = command.ExecuteReader();
            if (!reader.Read()) return new NullPosition();
            builder
                .SetId(reader.GetInt32("id"))
                .SetName(reader.GetString("name"))
                .SetRank(reader.GetInt32("rank"))
                .SetSalary(reader.GetDecimal("salary"));
            return builder.Build();
        }
        finally
        {
            connection.Close();
        }
    }
}