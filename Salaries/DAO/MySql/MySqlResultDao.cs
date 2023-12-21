using MySql.Data.MySqlClient;
using Salaries.Builders;
using Salaries.Entities;
using Salaries.Entities.NullEntities;

namespace Salaries.DAO.MySql;

public class MySqlResultDao(MySqlConnectionProvider connectionProvider, IResultBuilder resultBuilder)
    : IResultDao
{
    private const string GetAllResultsQuery = "SELECT * FROM result";
    private const string DeleteResultQuery = "DELETE FROM result WHERE id = @id";
    private const string UpdateResultQuery = "UPDATE result SET workerId = @workerId, objectiveId = @objectiveId, periodId = @periodId, completion = @completion WHERE id = @id";
    private const string AddResultQuery = "INSERT INTO result (workerId, objectiveId, periodId, completion) VALUES (@workerId, @objectiveId, @periodId, @completion)";
    private const string GetResultByIdQuery = "SELECT * FROM result WHERE id = @id";
    private const string GetResultsByWorkerIdQuery = "SELECT * FROM result WHERE workerId = @workerId";
    

    public List<Result> GetAllResults()
    {
        var connection = connectionProvider.GetConnection();
        try
        {
            connection.Open();
            var command = new MySqlCommand(GetAllResultsQuery, connection);
            var reader = command.ExecuteReader();
            var results = new List<Result>();
            while (reader.Read())
            {
                resultBuilder
                    .SetId(reader.GetInt32("id"))
                    .SetWorkerId(reader.GetInt32("workerId"))
                    .SetObjectiveId(reader.GetInt32("objectiveId"))
                    .SetPeriodId(reader.GetInt32("periodId"))
                    .SetCompletion(reader.GetFloat("completion"));
                var result = resultBuilder.Build();
                results.Add(result);
            }
            return results;
        }
        finally
        {
            connection.Close();
        }
    }

    public bool DeleteResult(int id)
    {
        var connection = connectionProvider.GetConnection();
        try
        {
            connection.Open();
            var command = new MySqlCommand(DeleteResultQuery, connection);
            command.Parameters.AddWithValue("@id", id);
            return command.ExecuteNonQuery() > 0;
        }
        finally
        {
            connection.Close();
        }
    }

    public bool UpdateResult(Result result)
    {
        var connection = connectionProvider.GetConnection();
        try
        {
            connection.Open();
            var command = new MySqlCommand(UpdateResultQuery, connection);
            command.Parameters.AddWithValue("@id", result.Id);
            command.Parameters.AddWithValue("@workerId", result.WorkerId);
            command.Parameters.AddWithValue("@objectiveId", result.ObjectiveId);
            command.Parameters.AddWithValue("@periodId", result.PeriodId);
            command.Parameters.AddWithValue("@completion", result.Completion);
            return command.ExecuteNonQuery() > 0;
        }
        finally
        {
            connection.Close();
        }
    }

    public Result AddResult(Result result)
    {
        var connection = connectionProvider.GetConnection();
        try
        {
            connection.Open();
            var command = new MySqlCommand(AddResultQuery, connection);
            command.Parameters.AddWithValue("@workerId", result.WorkerId);
            command.Parameters.AddWithValue("@objectiveId", result.ObjectiveId);
            command.Parameters.AddWithValue("@periodId", result.PeriodId);
            command.Parameters.AddWithValue("@completion", result.Completion);
            command.ExecuteNonQuery();
            var id = (int) command.LastInsertedId;
            result.Id = id;
            return result;
        }
        finally
        {
            connection.Close();
        }
    }

    public Result GetResultById(int id)
    {
        var connection = connectionProvider.GetConnection();
        try
        {
            connection.Open();
            var command = new MySqlCommand(GetResultByIdQuery, connection);
            command.Parameters.AddWithValue("@id", id);
            var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return new NullResult();
            }
            resultBuilder
                .SetId(reader.GetInt32("id"))
                .SetWorkerId(reader.GetInt32("workerId"))
                .SetObjectiveId(reader.GetInt32("objectiveId"))
                .SetPeriodId(reader.GetInt32("periodId"))
                .SetCompletion(reader.GetFloat("completion"));
            return resultBuilder.Build();
        }
        finally
        {
            connection.Close();
        }
    }

    public List<Result> GetResultsByWorkerId(int workerId)
    {
        var connection = connectionProvider.GetConnection();
        try
        {
            connection.Open();
            var command = new MySqlCommand(GetResultsByWorkerIdQuery, connection);
            command.Parameters.AddWithValue("@workerId", workerId);
            var reader = command.ExecuteReader();
            var results = new List<Result>();
            while (reader.Read())
            {
                resultBuilder
                    .SetId(reader.GetInt32("id"))
                    .SetWorkerId(reader.GetInt32("workerId"))
                    .SetObjectiveId(reader.GetInt32("objectiveId"))
                    .SetPeriodId(reader.GetInt32("periodId"))
                    .SetCompletion(reader.GetFloat("completion"));
                var result = resultBuilder.Build();
                results.Add(result);
            }
            return results;
        }
        finally
        {
            connection.Close();
        }
    }
}