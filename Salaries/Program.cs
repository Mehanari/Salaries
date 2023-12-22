// See https://aka.ms/new-console-template for more information

using ConsoleTables;
using Salaries.Builders;
using Salaries.Builders.DefaultBuilders;
using Salaries.DAO;
using Salaries.DAOCreators.MySql;
using Salaries.Entities;

namespace Salaries;

internal class Program
{
    private static readonly IPositionBuilder DefaultPositionBuilder = new DefaultPositionBuilder(0, "Undefined", 0, 0);

    private static readonly IPeriodBuilder DefaultPeriodBuilder =
        new DefaultPeriodBuilder(0, DateTime.MinValue, DateTime.MinValue);

    private static readonly IWorkerBuilder DefaultWorkerBuilder =
        new DefaultWorkerBuilder(0, 0, "Undefined", "Undefined", "Undefined");

    private static readonly IObjectiveBuilder DefaultObjectiveBuilder =
        new DefaultObjectiveBuilder(0, "Undefined", "Undefined");

    private static readonly IResultBuilder DefaultResultBuilder = new DefaultResultBuilder(0, 0, 0, 0, 0);

    private static readonly MySqlConnectionProvider MySqlConnectionProvider = new MySqlConnectionProvider(
        MySqlConnectionParameters.Server,
        MySqlConnectionParameters.User,
        MySqlConnectionParameters.Password,
        MySqlConnectionParameters
            .Database); // To connect to the database create a file named MySqlConnectionParameters.cs in the root directory of the project and add needed properties.

    private static IWorkerDao _workerDao;
    private static IObjectiveDao _objectiveDao;
    private static IResultDao _resultDao;
    private static IPeriodDao _periodDao;
    private static IPositionDao _positionDao;

    public static void Main(string[] args)
    {
        RegisterSingletons();
        InitializeDaoClasses();
        MainMenu();
    }

    # region Initialization

    private static void RegisterSingletons()
    {
        Singleton<IPositionBuilder>.Register(DefaultPositionBuilder);
        Singleton<IPeriodBuilder>.Register(DefaultPeriodBuilder);
        Singleton<IWorkerBuilder>.Register(DefaultWorkerBuilder);
        Singleton<IObjectiveBuilder>.Register(DefaultObjectiveBuilder);
        Singleton<IResultBuilder>.Register(DefaultResultBuilder);
        Singleton<MySqlConnectionProvider>.Register(MySqlConnectionProvider);
    }

    private static void InitializeDaoClasses()
    {
        _workerDao = new MySqlWorkerDaoCreator().Create();
        _objectiveDao = new MySqlObjectiveDaoCreator().Create();
        _resultDao = new MySqlResultDaoCreator().Create();
        _periodDao = new MySqlPeriodDaoCreator().Create();
        _positionDao = new MySqlPositionDaoCreator().Create();
    }

    # endregion

    # region Main menu

    private static void MainMenu()
    {
        Console.Clear();
        ShowMainMenuOptions();
        ProcessMainMenuOption(ReadMainMenuOption());
    }

    private static void ShowMainMenuOptions()
    {
        Console.WriteLine("1. Work with workers table.");
        Console.WriteLine("2. Work with objectives table.");
        Console.WriteLine("3. Work with results table.");
        Console.WriteLine("4. Work with periods table.");
        Console.WriteLine("5. Work with positions table.");
    }
    
    private static int ReadMainMenuOption()
    {
        return ReadOptions(1, 2, 3, 4, 5);
    }

    private static void ProcessMainMenuOption(int option)
    {
        switch (option)
        {
            case 1:
                WorkersMenu();
                break;
            case 2:
                ObjectivesMenu();
                break;
            case 3:
                ResultsMenu();
                break;
            case 4:
                PeriodsMenu();
                break;
            case 5:
                PositionsMenu();
                break;
        }
    }
    
    # endregion
    
    # region Workers
    
    private static void WorkersMenu()
    {
        Console.Clear();
        ShowWorkersMenuOptions();
        ProcessWorkersMenuOption(ReadWorkersMenuOption());
    }
    
    private static void ShowWorkersMenuOptions()
    {
        Console.WriteLine("1. Get all workers.");
        Console.WriteLine("2. Delete worker by id.");
        Console.WriteLine("3. Update worker.");
        Console.WriteLine("4. Add worker.");
        Console.WriteLine("5. Get worker by id.");
        Console.WriteLine("6. Get workers by full name.");
        Console.WriteLine("7. Back to main menu.");
    }
    
    private static int ReadWorkersMenuOption()
    {
        return ReadOptions(1, 2, 3, 4, 5, 6, 7);
    }
    
    private static void ProcessWorkersMenuOption(int option)
    {
        switch (option)
        {
            case 1:
                ShowAllWorkers();
                break;
            case 2:
                DeleteWorkerById();
                break;
            case 3:
                UpdateWorker();
                break;
            case 4:
                AddWorker();
                break;
            case 5:
                GetWorkerById();
                break;
            case 6:
                GetWorkersByFullName();
                break;
            case 7:
                MainMenu();
                break;
        }
    }
    
    private static void ShowAllWorkers()
    {
        try
        {
            var workers = _workerDao.GetAllWorkers();
            ShowWorkersInTable(workers);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            WorkersMenu();
        }
    }
    
    private static void DeleteWorkerById()
    {
        try
        {
            Console.Write("Enter worker id: ");
            var id = ReadPositiveNumber();
            var result = _workerDao.DeleteWorker(id);
            Console.WriteLine(result ? "Worker was deleted." : "Worker was not deleted.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            WorkersMenu();
        }
    }
    
    private static void UpdateWorker()
    {
        Console.WriteLine("Updating worker.");
        var worker = ReadWorkerWithId();
        try
        {
            var result = _workerDao.UpdateWorker(worker);
            Console.WriteLine(result ? "Worker was updated." : "Worker was not updated.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            WorkersMenu();
        }
    }
    
    private static void ShowWorkersInTable(List<Worker> workers)
    {
        var table = new ConsoleTable("Id", "Name", "Surname", "Patronymic", "Position id");
        foreach (var worker in workers)
        {
            table.AddRow(worker.Id, worker.FirstName, worker.Surname, worker.Patronymic, worker.PositionId);
        }
        table.Write();
    }
    
    private static void AddWorker()
    {
        Console.WriteLine("Inserting worker.");
        var worker = ReadWorker();
        try
        {
            var result = _workerDao.AddWorker(worker);
            Console.WriteLine(!result.IsNull() ? "Worker was inserted." : "Worker was not inserted.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            WorkersMenu();
        }
    }
    
    private static void GetWorkerById()
    {
        try
        {
            Console.WriteLine("Enter worker id: ");
            var id = ReadPositiveNumber();
            var worker = _workerDao.GetWorkerById(id);
            ShowWorkersInTable(new List<Worker> {worker});
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            WorkersMenu();
        }
    }
    
    private static void GetWorkersByFullName()
    {
        try
        {
            var fullname = ReadWorkerFullname();
            var worker = _workerDao.GetWorkerByFullname(fullname.name, fullname.surname, fullname.patronymic);
            ShowWorkersInTable(new List<Worker> {worker});
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            WorkersMenu();
        }
    }

    private static Worker ReadWorkerWithId()
    {
        var worker = ReadWorker();
        Console.WriteLine("Enter worker id.");
        worker.Id = ReadPositiveNumber();
        return worker;
    }

    private static Worker ReadWorker()
    {
        var workerBuilder = Singleton<IWorkerBuilder>.Instance;
        var fullname = ReadWorkerFullname();
        workerBuilder.SetName(fullname.name);
        workerBuilder.SetSurname(fullname.surname);
        workerBuilder.SetPatronymic(fullname.patronymic);
        Console.WriteLine("Enter position id.");
        workerBuilder.SetPositionId(ReadPositiveNumber());
        return workerBuilder.Build();
    }

    private static (string name, string surname, string patronymic) ReadWorkerFullname()
    {
        Console.WriteLine("Enter name.");
        var name = ReadString();
        Console.WriteLine("Enter surname.");
        var surname = ReadString();
        Console.WriteLine("Enter patronymic.");
        var patronymic = ReadString();
        return (name, surname, patronymic);
    }
    
    # endregion
    
    # region Objectives
    
    private static void ObjectivesMenu()
    {
        Console.Clear();
        ShowObjectivesMenuOptions();
        ProcessObjectivesMenuOption(ReadObjectivesMenuOption());
    }
    
    private static void ShowObjectivesMenuOptions()
    {
        Console.WriteLine("1. Get all objectives.");
        Console.WriteLine("2. Delete objective by id.");
        Console.WriteLine("3. Update objective.");
        Console.WriteLine("4. Add objective.");
        Console.WriteLine("5. Get objective by id.");
        Console.WriteLine("6. Get objectives by name.");
        Console.WriteLine("7. Back to main menu.");
    }
    
    private static int ReadObjectivesMenuOption()
    {
        return ReadOptions(1, 2, 3, 4, 5, 6, 7);
    }
    
    private static void ProcessObjectivesMenuOption(int option)
    {
        switch (option)
        {
            case 1:
                ShowAllObjectives();
                break;
            case 2:
                DeleteObjectiveById();
                break;
            case 3:
                UpdateObjective();
                break;
            case 4:
                AddObjective();
                break;
            case 5:
                GetObjectiveById();
                break;
            case 6:
                GetObjectivesByName();
                break;
            case 7:
                MainMenu();
                break;
        }
    }
    
    private static void ShowAllObjectives()
    {
        try
        {
            var objectives = _objectiveDao.GetAllObjectives();
            ShowObjectivesInTable(objectives);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            ObjectivesMenu();
        }
    }
    
    private static void DeleteObjectiveById()
    {
        try
        {
            Console.Write("Enter objective id: ");
            var id = ReadPositiveNumber();
            var result = _objectiveDao.DeleteObjective(id);
            Console.WriteLine(result ? "Objective was deleted." : "Objective was not deleted.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            ObjectivesMenu();
        }
    }
    
    private static void UpdateObjective()
    {
        Console.WriteLine("Updating objective.");
        var objective = ReadObjectiveWithId();
        try
        {
            var result = _objectiveDao.UpdateObjective(objective);
            Console.WriteLine(result ? "Objective was updated." : "Objective was not updated.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            ObjectivesMenu();
        }
    }
    
    private static void AddObjective()
    {
        Console.WriteLine("Inserting objective.");
        var objective = ReadObjective();
        try
        {
            var result = _objectiveDao.AddObjective(objective);
            Console.WriteLine(!result.IsNull() ? "Objective was inserted." : "Objective was not inserted.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            ObjectivesMenu();
        }
    }
    
    private static void GetObjectiveById()
    {
        try
        {
            Console.WriteLine("Enter objective id: ");
            var id = ReadPositiveNumber();
            var objective = _objectiveDao.GetObjectiveById(id);
            ShowObjectivesInTable(new List<Objective> {objective});
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            ObjectivesMenu();
        }
    }
    
    private static void GetObjectivesByName()
    {
        try
        {
            Console.WriteLine("Enter objective name.");
            var name = ReadString();
            var objectives = _objectiveDao.GetObjectivesByName(name);
            ShowObjectivesInTable(objectives);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            ObjectivesMenu();
        }
    }
    
    private static void ShowObjectivesInTable(List<Objective> objectives)
    {
        var table = new ConsoleTable("Id", "Name", "Description");
        foreach (var objective in objectives)
        {
            table.AddRow(objective.Id, objective.Name, objective.Description);
        }
        table.Write();
    }
    
    private static Objective ReadObjectiveWithId()
    {
        var objective = ReadObjective();
        Console.WriteLine("Enter objective id.");
        objective.Id = ReadPositiveNumber();
        return objective;
    }
    
    private static Objective ReadObjective()
    {
        var objectiveBuilder = Singleton<IObjectiveBuilder>.Instance;
        Console.WriteLine("Enter objective name.");
        objectiveBuilder.SetName(ReadString());
        Console.WriteLine("Enter objective description.");
        objectiveBuilder.SetDescription(ReadString());
        return objectiveBuilder.Build();
    }
    
    # endregion
    
    # region Results
    
    private static void ResultsMenu()
    {
        Console.Clear();
        ShowResultsMenuOptions();
        ProcessResultsMenuOption(ReadResultsMenuOption());
    }
    
    private static void ShowResultsMenuOptions()
    {
        Console.WriteLine("1. Get all results.");
        Console.WriteLine("2. Delete result by id.");
        Console.WriteLine("3. Update result.");
        Console.WriteLine("4. Add result.");
        Console.WriteLine("5. Get result by id.");
        Console.WriteLine("6. Get results by worker id.");
        Console.WriteLine("7. Back to main menu.");
    }
    
    private static int ReadResultsMenuOption()
    {
        return ReadOptions(1, 2, 3, 4, 5, 6, 7);
    }
    
    private static void ProcessResultsMenuOption(int option)
    {
        switch (option)
        {
            case 1:
                ShowAllResults();
                break;
            case 2:
                DeleteResultById();
                break;
            case 3:
                UpdateResult();
                break;
            case 4:
                AddResult();
                break;
            case 5:
                GetResultById();
                break;
            case 6:
                GetResultsByWorkerId();
                break;
            case 7:
                MainMenu();
                break;
        }
    }
    
    private static void ShowAllResults()
    {
        try
        {
            var results = _resultDao.GetAllResults();
            ShowResultsInTable(results);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            ResultsMenu();
        }
    }
    
    private static void DeleteResultById()
    {
        try
        {
            Console.Write("Enter result id: ");
            var id = ReadPositiveNumber();
            var result = _resultDao.DeleteResult(id);
            Console.WriteLine(result ? "Result was deleted." : "Result was not deleted.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            ResultsMenu();
        }
    }
    
    private static void UpdateResult()
    {
        Console.WriteLine("Updating result.");
        var result = ReadResultWithId();
        try
        {
            var isUpdated = _resultDao.UpdateResult(result);
            Console.WriteLine(isUpdated ? "Result was updated." : "Result was not updated.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            ResultsMenu();
        }
    }
    
    private static void AddResult()
    {
        Console.WriteLine("Inserting result.");
        var result = ReadResult();
        try
        {
            var isAdded = _resultDao.AddResult(result);
            Console.WriteLine(!isAdded.IsNull() ? "Result was inserted." : "Result was not inserted.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            ResultsMenu();
        }
    }
    
    private static void GetResultById()
    {
        try
        {
            Console.WriteLine("Enter result id: ");
            var id = ReadPositiveNumber();
            var result = _resultDao.GetResultById(id);
            ShowResultsInTable(new List<Result> {result});
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            ResultsMenu();
        }
    }
    
    private static void GetResultsByWorkerId()
    {
        try
        {
            Console.WriteLine("Enter worker id: ");
            var id = ReadPositiveNumber();
            var results = _resultDao.GetResultsByWorkerId(id);
            ShowResultsInTable(results);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            ResultsMenu();
        }
    }
    
    private static void ShowResultsInTable(List<Result> results)
    {
        var table = new ConsoleTable("Id", "Worker id", "Objective id", "Period id", "Completion");
        foreach (var result in results)
        {
            table.AddRow(result.Id, result.WorkerId, result.ObjectiveId, result.PeriodId, result.Completion);
        }
        table.Write();
    }
    
    private static Result ReadResultWithId()
    {
        var result = ReadResult();
        Console.WriteLine("Enter result id.");
        result.Id = ReadPositiveNumber();
        return result;
    }
    
    private static Result ReadResult()
    {
        var resultBuilder = Singleton<IResultBuilder>.Instance;
        Console.WriteLine("Enter worker id.");
        resultBuilder.SetWorkerId(ReadPositiveNumber());
        Console.WriteLine("Enter objective id.");
        resultBuilder.SetObjectiveId(ReadPositiveNumber());
        Console.WriteLine("Enter period id.");
        resultBuilder.SetPeriodId(ReadPositiveNumber());
        Console.WriteLine("Enter completion.");
        resultBuilder.SetCompletion(ReadPositiveNumber());
        return resultBuilder.Build();
    }
    
    # endregion
    
    # region Periods
    
    private static void PeriodsMenu()
    {
        Console.Clear();
        ShowPeriodsMenuOptions();
        ProcessPeriodsMenuOption(ReadPeriodsMenuOption());
    }
    
    private static void ShowPeriodsMenuOptions()
    {
        Console.WriteLine("1. Get all periods.");
        Console.WriteLine("2. Delete period by id.");
        Console.WriteLine("3. Update period.");
        Console.WriteLine("4. Add period.");
        Console.WriteLine("5. Get period by id.");
        Console.WriteLine("6. Get period by dates.");
        Console.WriteLine("7. Back to main menu.");
    }
    
    private static int ReadPeriodsMenuOption()
    {
        return ReadOptions(1, 2, 3, 4, 5, 6, 7);
    }
    
    private static void ProcessPeriodsMenuOption(int option)
    {
        switch (option)
        {
            case 1:
                ShowAllPeriods();
                break;
            case 2:
                DeletePeriodById();
                break;
            case 3:
                UpdatePeriod();
                break;
            case 4:
                AddPeriod();
                break;
            case 5:
                GetPeriodById();
                break;
            case 6:
                GetPeriodByDates();
                break;
            case 7:
                MainMenu();
                break;
        }
    }
    
    private static void ShowAllPeriods()
    {
        try
        {
            var periods = _periodDao.GetAllPeriods();
            ShowPeriodsInTable(periods);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            PeriodsMenu();
        }
    }
    
    private static void DeletePeriodById()
    {
        try
        {
            Console.Write("Enter period id: ");
            var id = ReadPositiveNumber();
            var result = _periodDao.DeletePeriod(id);
            Console.WriteLine(result ? "Period was deleted." : "Period was not deleted.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            PeriodsMenu();
        }
    }
    
    private static void UpdatePeriod()
    {
        Console.WriteLine("Updating period.");
        var period = ReadPeriodWithId();
        try
        {
            var result = _periodDao.UpdatePeriod(period);
            Console.WriteLine(result ? "Period was updated." : "Period was not updated.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            PeriodsMenu();
        }
    }
    
    private static void AddPeriod()
    {
        Console.WriteLine("Inserting period.");
        var period = ReadPeriod();
        try
        {
            var result = _periodDao.AddPeriod(period);
            Console.WriteLine(!result.IsNull() ? "Period was inserted." : "Period was not inserted.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            PeriodsMenu();
        }
    }
    
    private static void GetPeriodById()
    {
        try
        {
            Console.WriteLine("Enter period id: ");
            var id = ReadPositiveNumber();
            var period = _periodDao.GetPeriodById(id);
            ShowPeriodsInTable(new List<Period> {period});
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            PeriodsMenu();
        }
    }
    
    private static void GetPeriodByDates()
    {
        try
        {
            Console.WriteLine("Enter begin date.");
            var beginDate = ReadDateTime();
            Console.WriteLine("Enter end date.");
            var endDate = ReadDateTime();
            var period = _periodDao.GetPeriodByDates(beginDate, endDate);
            ShowPeriodsInTable(new List<Period> {period});
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            PeriodsMenu();
        }
    }
    
    private static void ShowPeriodsInTable(List<Period> periods)
    {
        var table = new ConsoleTable("Id", "Begin date", "End date");
        foreach (var period in periods)
        {
            table.AddRow(period.Id, period.BeginDate, period.EndDate);
        }
        table.Write();
    }
    
    private static Period ReadPeriodWithId()
    {
        var period = ReadPeriod();
        Console.WriteLine("Enter period id.");
        period.Id = ReadPositiveNumber();
        return period;
    }
    
    private static Period ReadPeriod()
    {
        var periodBuilder = Singleton<IPeriodBuilder>.Instance;
        Console.WriteLine("Enter begin date.");
        periodBuilder.SetBeginDate(ReadDateTime());
        Console.WriteLine("Enter end date.");
        periodBuilder.SetEndDate(ReadDateTime());
        return periodBuilder.Build();
    }
    
    # endregion
    
    # region Positions
    
    private static void PositionsMenu()
    {
        Console.Clear();
        ShowPositionsMenuOptions();
        ProcessPositionsMenuOption(ReadPositionsMenuOption());
    }
    
    private static void ShowPositionsMenuOptions()
    {
        Console.WriteLine("1. Get all positions.");
        Console.WriteLine("2. Delete position by id.");
        Console.WriteLine("3. Update position.");
        Console.WriteLine("4. Add position.");
        Console.WriteLine("5. Get position by id.");
        Console.WriteLine("6. Get positions by name.");
        Console.WriteLine("7. Back to main menu.");
    }
    
    private static int ReadPositionsMenuOption()
    {
        return ReadOptions(1, 2, 3, 4, 5, 6, 7);
    }
    
    private static void ProcessPositionsMenuOption(int option)
    {
        switch (option)
        {
            case 1:
                ShowAllPositions();
                break;
            case 2:
                DeletePositionById();
                break;
            case 3:
                UpdatePosition();
                break;
            case 4:
                AddPosition();
                break;
            case 5:
                GetPositionById();
                break;
            case 6:
                GetPositionsByName();
                break;
            case 7:
                MainMenu();
                break;
        }
    }
    
    private static void ShowAllPositions()
    {
        try
        {
            var positions = _positionDao.GetAllPositions();
            ShowPositionsInTable(positions);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            PositionsMenu();
        }
    }
    
    private static void DeletePositionById()
    {
        try
        {
            Console.Write("Enter position id: ");
            var id = ReadPositiveNumber();
            var result = _positionDao.DeletePosition(id);
            Console.WriteLine(result ? "Position was deleted." : "Position was not deleted.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            PositionsMenu();
        }
    }
    
    private static void UpdatePosition()
    {
        Console.WriteLine("Updating position.");
        var position = ReadPositionWithId();
        try
        {
            var result = _positionDao.UpdatePosition(position);
            Console.WriteLine(result ? "Position was updated." : "Position was not updated.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            PositionsMenu();
        }
    }
    
    private static void AddPosition()
    {
        Console.WriteLine("Inserting position.");
        var position = ReadPosition();
        try
        {
            var result = _positionDao.AddPosition(position);
            Console.WriteLine(!result.IsNull() ? "Position was inserted." : "Position was not inserted.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            PositionsMenu();
        }
    }
    
    private static void GetPositionById()
    {
        try
        {
            Console.WriteLine("Enter position id: ");
            var id = ReadPositiveNumber();
            var position = _positionDao.GetPositionById(id);
            ShowPositionsInTable(new List<Position> {position});
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            PositionsMenu();
        }
    }
    
    private static void GetPositionsByName()
    {
        try
        {
            Console.WriteLine("Enter position name.");
            var name = ReadString();
            var rank = ReadPositiveNumber();
            var position = _positionDao.GetPositionByRankAndName(name, rank);
            ShowPositionsInTable(new List<Position> {position});
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            PositionsMenu();
        }
    }
    
    private static void ShowPositionsInTable(List<Position> positions)
    {
        var table = new ConsoleTable("Id", "Name", "Rank");
        foreach (var position in positions)
        {
            table.AddRow(position.Id, position.Name, position.Rank);
        }
        table.Write();
    }
    
    private static Position ReadPositionWithId()
    {
        var position = ReadPosition();
        Console.WriteLine("Enter position id.");
        position.Id = ReadPositiveNumber();
        return position;
    }
    
    private static Position ReadPosition()
    {
        var positionBuilder = Singleton<IPositionBuilder>.Instance;
        Console.WriteLine("Enter position name.");
        positionBuilder.SetName(ReadString());
        Console.WriteLine("Enter position rank.");
        positionBuilder.SetRank(ReadPositiveNumber());
        Console.WriteLine("Enter position salary.");
        positionBuilder.SetSalary(ReadDecimal());
        return positionBuilder.Build();
    }
    
    # endregion
    
    # region Console utils
    
    private static int ReadPositiveNumber()
    {
        while (true)
        {
            var number = ReadNumber();
            if (number > 0)
            {
                return number;
            }
            Console.WriteLine("Number must be positive.");
        }
    }
    
    private static int ReadOptions(params int[] allowedOptions)
    {
        var option = ReadNumber();
        while (!allowedOptions.Contains(option))
        {
            Console.WriteLine("Invalid option.");
            option = ReadNumber();
        }
        return option;
    }
    
    private static int ReadNumber()
    {
        while (true)
        {
            Console.WriteLine("Enter number: ");
            var numberString = Console.ReadLine();
            if (int.TryParse(numberString, out var number))
            {
                return number;
            }
            Console.WriteLine("Invalid number.");
        }
    }
    
    private static string ReadString()
    {
        while (true)
        {
            Console.WriteLine("Enter string: ");
            var str = Console.ReadLine();
            if (!string.IsNullOrEmpty(str))
            {
                return str;
            }
            Console.WriteLine("String cannot be empty.");
        }
    }
    
    private static DateTime ReadDateTime()
    {
        while (true)
        {
            Console.WriteLine("Enter date in format dd.mm.yyyy: ");
            var dateString = Console.ReadLine();
            if (DateTime.TryParse(dateString, out var date))
            {
                return date;
            }
            Console.WriteLine("Cannot parse to date.");
        }
    }
    
    private static decimal ReadDecimal()
    {
        while (true)
        {
            Console.WriteLine("Enter decimal: ");
            var decimalString = Console.ReadLine();
            if (decimal.TryParse(decimalString, out var number))
            {
                return number;
            }
            Console.WriteLine("Cannot parse to decimal.");
        }
    }
    
    # endregion
}