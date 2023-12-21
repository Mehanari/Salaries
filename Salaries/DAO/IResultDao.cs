using Salaries.Entities;

namespace Salaries.DAO;

public interface IResultDao
{
    List<Result> GetAllResults();
    bool DeleteResult(int id);
    bool UpdateResult(Result result);
    Result AddResult(Result result);
    Result GetResultById(int id);
    List<Result> GetResultsByWorkerId(int workerId);
}