namespace Salaries.Entities.NullEntities;

public sealed class NullPeriod() : Period(0, DateTime.MinValue, DateTime.MinValue)
{
    public override bool IsNull() => true;
}