using MySql.Data.MySqlClient;
using Salaries.Builders;
using Salaries.Entities;

namespace Salaries.DAO.MySql;

public class MySqlPeriodDao(MySqlConnectionProvider connectionProvider, IPeriodBuilder periodBuilder) : IPeriodDao
{
    private readonly IPeriodBuilder _periodBuilder = periodBuilder;

    private const string GetAllPeriodsQuery = "SELECT * FROM period";
    private const string DeletePeriodQuery = "DELETE FROM period WHERE id = @id";
    private const string UpdatePeriodQuery = "UPDATE period SET beginDate = @beginDate, endDate = @endDate WHERE id = @id";
    private const string AddPeriodQuery = "INSERT INTO period (beginDate, endDate) VALUES (@beginDate, @endDate)";
    private const string GetPeriodByIdQuery = "SELECT * FROM period WHERE id = @id";
    private const string GetPeriodByDatesQuery = "SELECT * FROM period WHERE beginDate = @beginDate AND endDate = @endDate";

    public List<Period> GetAllPeriods()
    {
        var connection = connectionProvider.GetConnection();
        try {
            connection.Open();
            var command = new MySqlCommand(GetAllPeriodsQuery, connection);
            var reader = command.ExecuteReader();
            var periods = new List<Period>();
            while (reader.Read()) {
                periodBuilder
                    .SetId(reader.GetInt32("id"))
                    .SetBeginDate(reader.GetDateTime("beginDate"))
                    .SetEndDate(reader.GetDateTime("endDate"));
                var period = periodBuilder.Build();
                periods.Add(period);
            }
            return periods;
        }
        finally {
            connection.Close();
        }
    }

    public bool DeletePeriod(int id)
    {
        var connection = connectionProvider.GetConnection();
        try {
            connection.Open();
            var command = new MySqlCommand(DeletePeriodQuery, connection);
            command.Parameters.AddWithValue("@id", id);
            return command.ExecuteNonQuery() > 0;
        }
        finally {
            connection.Close();
        }
    }

    public bool UpdatePeriod(Period period)
    {
        var connection = connectionProvider.GetConnection();
        try {
            connection.Open();
            var command = new MySqlCommand(UpdatePeriodQuery, connection);
            command.Parameters.AddWithValue("@id", period.Id);
            command.Parameters.AddWithValue("@beginDate", period.BeginDate);
            command.Parameters.AddWithValue("@endDate", period.EndDate);
            return command.ExecuteNonQuery() > 0;
        }
        finally {
            connection.Close();
        }
    }

    public Period AddPeriod(Period period)
    { 
        var connection = connectionProvider.GetConnection();
        try {
            connection.Open();
            var command = new MySqlCommand(AddPeriodQuery, connection);
            command.Parameters.AddWithValue("@beginDate", period.BeginDate);
            command.Parameters.AddWithValue("@endDate", period.EndDate);
            command.ExecuteNonQuery();
            var id = (int)command.LastInsertedId;
            return GetPeriodById(id);
        }
        finally {
            connection.Close();
        }
    }

    public Period GetPeriodById(int id)
    {
        var connection = connectionProvider.GetConnection();
        try {
            connection.Open();
            var command = new MySqlCommand(GetPeriodByIdQuery, connection);
            command.Parameters.AddWithValue("@id", id);
            var reader = command.ExecuteReader();
            if (!reader.Read()) {
                return _periodBuilder.Build();
            }
            _periodBuilder
                .SetId(reader.GetInt32("id"))
                .SetBeginDate(reader.GetDateTime("beginDate"))
                .SetEndDate(reader.GetDateTime("endDate"));
            return _periodBuilder.Build();
        }
        finally {
            connection.Close();
        }
    }

    public Period GetPeriodByDates(DateTime beginDate, DateTime endDate)
    {
        var connection = connectionProvider.GetConnection();
        try
        {
            connection.Open();
            var command = new MySqlCommand(GetPeriodByDatesQuery, connection);
            command.Parameters.AddWithValue("@beginDate", beginDate);
            command.Parameters.AddWithValue("@endDate", endDate);
            var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return _periodBuilder.Build();
            }

            _periodBuilder
                .SetId(reader.GetInt32("id"))
                .SetBeginDate(reader.GetDateTime("beginDate"))
                .SetEndDate(reader.GetDateTime("endDate"));
            return _periodBuilder.Build();
        }
        finally
        {
            connection.Close();
        }
    }
}