namespace Salaries.Entities.NullEntities
{
    public sealed class NullObjective() : Objective(0, "No Objective", "No Description")
    {
        public override bool IsNull() => true;
    }
}