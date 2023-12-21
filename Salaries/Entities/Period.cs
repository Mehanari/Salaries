namespace Salaries.Entities
{
    public class Period(int id, DateTime beginDate, DateTime endDate)
    {
        public int Id { get; set; } = id;
        public DateTime BeginDate { get; set; } = beginDate;
        public DateTime EndDate { get; set; } = endDate;

        public virtual bool IsNull()
        {
            return false;
        }
    }
}