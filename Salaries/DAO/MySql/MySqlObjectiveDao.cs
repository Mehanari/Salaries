using MySql.Data.MySqlClient;
using Salaries.Builders;
using Salaries.Entities;
using Salaries.Entities.NullEntities;

namespace Salaries.DAO.MySql;

public class MySqlObjectiveDao(MySqlConnectionProvider connectionProvider, IObjectiveBuilder builder)
    : IObjectiveDao
{
    private const string GetAllObjectivesQuery = "SELECT * FROM objective";
    private const string DeleteObjectiveQuery = "DELETE FROM objective WHERE id = @id";
    private const string UpdateObjectiveQuery = "UPDATE objective SET name = @name, description = @description WHERE id = @id";
    private const string AddObjectiveQuery = "INSERT INTO objective (name, description) VALUES (@name, @description)";
    private const string GetObjectiveByIdQuery = "SELECT * FROM objective WHERE id = @id";
    private const string GetObjectiveByNameQuery = "SELECT * FROM objective WHERE name = @name";

    public List<Objective> GetAllObjectives()
    {
        var connection = connectionProvider.GetConnection();
        try
        {
            connection.Open();
            var command = new MySqlCommand(GetAllObjectivesQuery, connection);
            var reader = command.ExecuteReader();
            var objectives = new List<Objective>();
            while (reader.Read())
            {
                builder
                    .SetId(reader.GetInt32("id"))
                    .SetName(reader.GetString("name"))
                    .SetDescription(reader.GetString("description"));
                var objective = builder.Build();
                objectives.Add(objective);
            }
            return objectives;
        }
        finally
        {
            connection.Close();
        }
    }

    public bool DeleteObjective(int id)
    {
        var connection = connectionProvider.GetConnection();
        try
        {
            connection.Open();
            var command = new MySqlCommand(DeleteObjectiveQuery, connection);
            command.Parameters.AddWithValue("@id", id);
            return command.ExecuteNonQuery() > 0;
        }
        finally
        {
            connection.Close();
        }
    }

    public bool UpdateObjective(Objective objective)
    {
        var connection = connectionProvider.GetConnection();
        try
        {
            connection.Open();
            var command = new MySqlCommand(UpdateObjectiveQuery, connection);
            command.Parameters.AddWithValue("@id", objective.Id);
            command.Parameters.AddWithValue("@name", objective.Name);
            command.Parameters.AddWithValue("@description", objective.Description);
            return command.ExecuteNonQuery() > 0;
        }
        finally
        {
            connection.Close();
        }
    }

    public Objective AddObjective(Objective objective)
    {
        var connection = connectionProvider.GetConnection();
        try
        {
            connection.Open();
            var command = new MySqlCommand(AddObjectiveQuery, connection);
            command.Parameters.AddWithValue("@name", objective.Name);
            command.Parameters.AddWithValue("@description", objective.Description);
            command.ExecuteNonQuery();
            var id = (int) command.LastInsertedId;
            return GetObjectiveById(id);
        }
        finally
        {
            connection.Close();
        }
    }

    public Objective GetObjectiveById(int id)
    {
        var connection = connectionProvider.GetConnection();
        try
        {
            connection.Open();
            var command = new MySqlCommand(GetObjectiveByIdQuery, connection);
            command.Parameters.AddWithValue("@id", id);
            var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return new NullObjective();
            }
            builder
                .SetId(reader.GetInt32("id"))
                .SetName(reader.GetString("name"))
                .SetDescription(reader.GetString("description"));
            return builder.Build();
        }
        finally
        {
            connection.Close();
        }
    }

    public List<Objective> GetObjectivesByName(string name)
    {
        var connection = connectionProvider.GetConnection();
        try
        {
            connection.Open();
            var command = new MySqlCommand(GetObjectiveByNameQuery, connection);
            command.Parameters.AddWithValue("@name", name);
            var reader = command.ExecuteReader();
            var objectives = new List<Objective>();
            while (reader.Read())
            {
                builder
                    .SetId(reader.GetInt32("id"))
                    .SetName(reader.GetString("name"))
                    .SetDescription(reader.GetString("description"));
                var objective = builder.Build();
                objectives.Add(objective);
            }
            return objectives;
        }
        finally
        {
            connection.Close();
        }
    }
}