namespace Salaries.Entities
{
    public class Result(int id, int workerId, int periodId, int objectiveId, float completion)
    {
        public int Id { get; set; } = id;
        public int WorkerId { get; set; } = workerId;
        public int PeriodId { get; set; } = periodId;
        public int ObjectiveId { get; set; } = objectiveId;
        public float Completion { get; set; } = completion;

        public virtual bool IsNull() => false;
    }
}