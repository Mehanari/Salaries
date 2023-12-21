using Salaries.Entities;

namespace Salaries.Builders.DefaultBuilders;

public class DefaultResultBuilder(int defaultId, int defaultWorkerId, int defaultPeriodId, int defaultObjectiveId,
        float defaultCompletion)
    : IResultBuilder
{
    private Result _result = new(defaultId, defaultWorkerId, defaultPeriodId, defaultObjectiveId, defaultCompletion);

    public IResultBuilder SetId(int id)
    {
        _result.Id = id;
        return this;
    }

    public IResultBuilder SetWorkerId(int workerId)
    {
        _result.WorkerId = workerId;
        return this;
    }

    public IResultBuilder SetPeriodId(int periodId)
    {
        _result.PeriodId = periodId;
        return this;
    }

    public IResultBuilder SetObjectiveId(int objectiveId)
    {
        _result.ObjectiveId = objectiveId;
        return this;
    }

    public IResultBuilder SetCompletion(float completion)
    {
        _result.Completion = completion;
        return this;
    }

    public IResultBuilder Reset()
    {
        _result = new Result(defaultId, defaultWorkerId, defaultPeriodId, defaultObjectiveId, defaultCompletion);
        return this;
    }

    public Result Build()
    {
        var result = _result;
        Reset();
        return result;
    }
}