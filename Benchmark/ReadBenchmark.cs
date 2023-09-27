namespace Benchmark;

[MemoryDiagnoser]
public class ReadBenchmark
{
    private const int EntitiesCount = 1000;

    private UntypedContext untypedContext;
    private TypedContext typedContext;
    private StronglyTypedContext stronglyTypedContext;

    [GlobalSetup]
    public void GlobalSetup()
    {
        untypedContext = new UntypedContext(new DbContextOptionsBuilder()
            .UseNpgsql("Database=UntypedIdsTest;Host=127.0.0.1;Port=5432;User ID=postgres;Password=1234;").Options);
        typedContext = new TypedContext(new DbContextOptionsBuilder()
            .UseNpgsql("Database=TypedIdsTest;Host=127.0.0.1;Port=5432;User ID=postgres;Password=1234;").Options);
        stronglyTypedContext = new StronglyTypedContext(new DbContextOptionsBuilder()
            .UseNpgsql("Database=StronglyTypedIdsTest;Host=127.0.0.1;Port=5432;User ID=postgres;Password=1234;").Options);

        untypedContext.Departments.RemoveRange(untypedContext.Departments);
        untypedContext.Employees.RemoveRange(untypedContext.Employees);
        untypedContext.SaveChanges();

        typedContext.Departments.RemoveRange(typedContext.Departments);
        typedContext.Employees.RemoveRange(typedContext.Employees);
        typedContext.SaveChanges();

        stronglyTypedContext.Departments.RemoveRange(stronglyTypedContext.Departments);
        stronglyTypedContext.Employees.RemoveRange(stronglyTypedContext.Employees);
        stronglyTypedContext.SaveChanges();

        typedContext.Departments.AddRange(Enumerable.Range(0, EntitiesCount).Select(_ => new TypedIdsDemo.Models.Department
        {
            Name = "Test",
            Employees = new()
            {
                new()
                {
                    Name = "TestEmployee",
                    ExternalId = new(123)
                },
            }
        }));
        typedContext.SaveChanges();

        untypedContext.Departments.AddRange(Enumerable.Range(0, EntitiesCount).Select(_ => new NotTypedIdsDemo.Models.Department
        {
            Name = "Test",
            Employees = new()
            {
                new()
                {
                    Name = "TestEmployee",
                    ExternalId = 123
                },
            }
        }));
        untypedContext.SaveChanges();

        stronglyTypedContext.Departments.AddRange(Enumerable.Range(0, EntitiesCount).Select(_ => new StronglyTypedIdsDemo.Models.Department
        {
            Name = "Test",
            Employees = new()
            {
                new()
                {
                    Name = "TestEmployee",
                    ExternalId = new(123)
                },
            }
        }));
        stronglyTypedContext.SaveChanges();
    }

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        untypedContext.Departments.RemoveRange(untypedContext.Departments);
        untypedContext.Employees.RemoveRange(untypedContext.Employees);
        untypedContext.SaveChanges();

        typedContext.Departments.RemoveRange(typedContext.Departments);
        typedContext.Employees.RemoveRange(typedContext.Employees);
        typedContext.SaveChanges();

        stronglyTypedContext.Departments.RemoveRange(stronglyTypedContext.Departments);
        stronglyTypedContext.Employees.RemoveRange(stronglyTypedContext.Employees);
        stronglyTypedContext.SaveChanges();
    }


    [Benchmark]
    public void TypedIds_GetAll()
    {
        var departments = typedContext.Departments.ToList();
    }

    [Benchmark]
    public void StronglyTypedIds_GetAll()
    {
        var departments = stronglyTypedContext.Departments.ToList();
    }

    [Benchmark]
    public void UntypedIds_GetAll()
    {
        var departments = untypedContext.Departments.ToList();
    }

    [Benchmark]
    public void TypedIds_ById()
    {
        var departments = typedContext.Departments.FirstOrDefault(x => x.Id == new DepartmentId(EntitiesCount / 2));
    }

    [Benchmark]
    public void StronglyTypedIds_ById()
    {
        var departments = stronglyTypedContext.Departments.FirstOrDefault(x =>
            x.Id == new StronglyTypedIdsDemo.Models.Ids.DepartmentId(EntitiesCount / 2));
    }

    [Benchmark]
    public void UntypedIds_ById()
    {
        var departments = untypedContext.Departments.FirstOrDefault(x => x.Id == EntitiesCount / 2);
    }
}