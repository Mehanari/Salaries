using Salaries.Entities;

namespace Salaries.Builders.DefaultBuilders
{
    public class DefaultWorkerBuilder(int defaultId, int defaultPositionId, string defaultFirstName,
            string defaultSurname, string defaultPatronymic)
        : IWorkerBuilder
    {
        private Worker _worker = new(defaultId, defaultPositionId, defaultFirstName, defaultSurname, defaultPatronymic);

        public IWorkerBuilder SetId(int id)
        {
            _worker.Id = id;
            return this;
        }

        public IWorkerBuilder SetFirstName(string firstName)
        {
            _worker.FirstName = firstName;
            return this;
        }

        public IWorkerBuilder SetSurname(string surname)
        {
            _worker.Surname = surname;
            return this;
        }

        public IWorkerBuilder SetPatronymic(string patronymic)
        {
            _worker.Patronymic = patronymic;
            return this;
        }

        public IWorkerBuilder SetPositionId(int positionId)
        {
            _worker.PositionId = positionId;
            return this;
        }

        public IWorkerBuilder Reset()
        {
            _worker = new Worker(defaultId, defaultPositionId, defaultFirstName, defaultSurname, defaultPatronymic);
            return this;
        }

        public Worker Build()
        {
            var worker = _worker;
            Reset();
            return worker;
        }
    }
}