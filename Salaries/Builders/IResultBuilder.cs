using Salaries.Entities;

namespace Salaries.Builders
{
    public interface IResultBuilder
    {
        IResultBuilder SetId(int id);
        IResultBuilder SetWorkerId(int workerId);
        IResultBuilder SetPeriodId(int periodId);
        IResultBuilder SetObjectiveId(int objectiveId);
        IResultBuilder SetCompletion(float completion);
        IResultBuilder Reset();
        Result Build();
    }
}