using Salaries.Entities;

namespace Salaries.Builders.DefaultBuilders
{
    public class DefaultPositionBuilder(int defaultId, string defaultName, decimal defaultSalary) : IPositionBuilder
    {
        private Position _position = new(defaultId, defaultName, defaultSalary);

        public IPositionBuilder SetId(int id)
        {
            _position.Id = id;
            return this;
        }

        public IPositionBuilder SetName(string name)
        {
            _position.Name = name;
            return this;
        }

        public IPositionBuilder SetSalary(decimal salary)
        {
            _position.Salary = salary;
            return this;
        }

        public IPositionBuilder Reset()
        {
            _position = new Position(defaultId, defaultName, defaultSalary);
            return this;
        }

        public Position Build()
        {
            var position = _position;
            Reset();
            return position;
        }
    }
}