using Salaries.Entities;

namespace Salaries.DAO;

public interface IPeriodDao
{
    List<Period> GetAllPeriods();
    bool DeletePeriod(int id);
    bool UpdatePeriod(Period period);
    Period AddPeriod(Period period);
    Period GetPeriodById(int id);
    Period GetPeriodByDates(DateTime beginDate, DateTime endDate);
}