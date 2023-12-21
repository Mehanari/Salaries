using Salaries.Entities;

namespace Salaries.Builders
{
    public interface IPeriodBuilder
    {
        IPeriodBuilder SetId(int id);
        IPeriodBuilder SetBeginDate(DateTime beginDate);
        IPeriodBuilder SetEndDate(DateTime endDate);
        IPeriodBuilder Reset();
        Period Build();
    }
}