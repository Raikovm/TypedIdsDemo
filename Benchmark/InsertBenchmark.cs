namespace Benchmark;

[MemoryDiagnoser]
public class InsertBenchmark
{
    private const int EntitiesCount = 100;

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

    [IterationCleanup]
    public void IterationCleanup()
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
    public void TypedIds()
    {
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
    }
    
    [Benchmark]
    public void StronglyTypedIds()
    {
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
    
    [Benchmark]
    public void UntypedIds()
    {
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
    }
}