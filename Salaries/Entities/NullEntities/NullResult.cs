namespace Salaries.Entities.NullEntities;

public sealed class NullResult() : Result(0, 0, 0, 0, 0)
{
    public override bool IsNull() => true;
}