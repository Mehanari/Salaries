using MySql.Data.MySqlClient;
using Salaries.Builders;
using Salaries.Entities;
using Salaries.Entities.NullEntities;

namespace Salaries.DAO.MySql;

public class MySqlWorkerDao(MySqlConnectionProvider connectionProvider, IWorkerBuilder builder)
    : IWorkerDao
{
    private const string GetAllWorkersQuery = "SELECT * FROM worker";
    private const string DeleteWorkerQuery = "DELETE FROM worker WHERE id = @id";
    private const string UpdateWorkerQuery = "UPDATE worker SET `name` = @name, surname = @surname, patronymic = @patronymic, positionId = @positionId WHERE id = @id";
    private const string AddWorkerQuery = "INSERT INTO worker (`name`, surname, patronymic, positionId) VALUES (@name, @surname, @patronymic, @positionId)";
    private const string GetWorkerByIdQuery = "SELECT * FROM worker WHERE id = @id";
    private const string GetWorkerByFullnameQuery = "SELECT * FROM worker WHERE `name` = @name AND surname = @surname AND patronymic = @patronymic";


    public List<Worker> GetAllWorkers()
    {
        var connection = connectionProvider.GetConnection();
        try
        {
            connection.Open();
            var command = new MySqlCommand(GetAllWorkersQuery, connection);
            var reader = command.ExecuteReader();
            var workers = new List<Worker>();
            while (reader.Read())
            {
                builder
                    .SetId(reader.GetInt32("id"))
                    .SetPositionId(reader.GetInt32("positionId"))
                    .SetName(reader.GetString("name"))
                    .SetSurname(reader.GetString("surname"))
                    .SetPatronymic(reader.GetString("patronymic"));
                var worker = builder.Build();
                workers.Add(worker);
            }
            return workers;
        }
        finally
        {
            connection.Close();
        }
    }

    public bool DeleteWorker(int id)
    {
        var connection = connectionProvider.GetConnection();
        try
        {
            connection.Open();
            var command = new MySqlCommand(DeleteWorkerQuery, connection);
            command.Parameters.AddWithValue("@id", id);
            return command.ExecuteNonQuery() > 0;
        }
        finally
        {
            connection.Close();
        }
    }

    public bool UpdateWorker(Worker worker)
    {
        var connection = connectionProvider.GetConnection();
        try
        {
            connection.Open();
            var command = new MySqlCommand(UpdateWorkerQuery, connection);
            command.Parameters.AddWithValue("@id", worker.Id);
            command.Parameters.AddWithValue("@name", worker.FirstName);
            command.Parameters.AddWithValue("@surname", worker.Surname);
            command.Parameters.AddWithValue("@patronymic", worker.Patronymic);
            command.Parameters.AddWithValue("@positionId", worker.PositionId);
            return command.ExecuteNonQuery() > 0;
        }
        finally
        {
            connection.Close();
        }
    }

    public Worker AddWorker(Worker worker)
    {
        var connection = connectionProvider.GetConnection();
        try
        {
            connection.Open();
            var command = new MySqlCommand(AddWorkerQuery, connection);
            command.Parameters.AddWithValue("@name", worker.FirstName);
            command.Parameters.AddWithValue("@surname", worker.Surname);
            command.Parameters.AddWithValue("@patronymic", worker.Patronymic);
            command.Parameters.AddWithValue("@positionId", worker.PositionId);
            command.ExecuteNonQuery();
            var id = (int)command.LastInsertedId;
            return GetWorkerById(id);
        }
        finally
        {
            connection.Close();
        }
    }

    public Worker GetWorkerById(int id)
    {
        var connection = connectionProvider.GetConnection();
        try
        {
            connection.Open();
            var command = new MySqlCommand(GetWorkerByIdQuery, connection);
            command.Parameters.AddWithValue("@id", id);
            var reader = command.ExecuteReader();
            if (!reader.Read()) return new NullWorker();
            builder
                .SetId(reader.GetInt32("id"))
                .SetPositionId(reader.GetInt32("positionId"))
                .SetName(reader.GetString("name"))
                .SetSurname(reader.GetString("surname"))
                .SetPatronymic(reader.GetString("patronymic"));
            return builder.Build();
        }
        finally
        {
            connection.Close();
        }
    }

    public Worker GetWorkerByFullname(string name, string surname, string patronymic)
    {
        var connection = connectionProvider.GetConnection();
        try
        {
            connection.Open();
            var command = new MySqlCommand(GetWorkerByFullnameQuery, connection);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname", surname);
            command.Parameters.AddWithValue("@patronymic", patronymic);
            var reader = command.ExecuteReader();
            if (!reader.Read()) return new NullWorker();
            builder
                .SetId(reader.GetInt32("id"))
                .SetPositionId(reader.GetInt32("positionId"))
                .SetName(reader.GetString("name"))
                .SetSurname(reader.GetString("surname"))
                .SetPatronymic(reader.GetString("patronymic"));
            return builder.Build();
        }
        finally
        {
            connection.Close();
        }
    }
}