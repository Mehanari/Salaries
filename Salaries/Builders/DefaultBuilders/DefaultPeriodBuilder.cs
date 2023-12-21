using Salaries.Entities;

namespace Salaries.Builders.DefaultBuilders
{
    public class DefaultPeriodBuilder(int defaultId, DateTime defaultBeginDate, DateTime defaultEndDate)
        : IPeriodBuilder
    {
        private Period _period = new(defaultId, defaultBeginDate, defaultEndDate);

        public IPeriodBuilder SetId(int id)
        {
            _period.Id = id;
            return this;
        }

        public IPeriodBuilder SetBeginDate(DateTime beginDate)
        {
            _period.BeginDate = beginDate;
            return this;
        }

        public IPeriodBuilder SetEndDate(DateTime endDate)
        {
            _period.EndDate = endDate;
            return this;
        }

        public IPeriodBuilder Reset()
        {
            _period = new Period(defaultId, defaultBeginDate, defaultEndDate);
            return this;
        }

        public Period Build()
        {
            var period = _period;
            Reset();
            return period;
        }
    }
}