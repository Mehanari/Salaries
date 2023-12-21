namespace Salaries.Entities.NullEntities;

public sealed class NullPosition() : Position(0, "No Position", 0, 0)
{
    public override bool IsNull() => true;
}