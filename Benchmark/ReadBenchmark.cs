using NotTypedIdsDemo.Models;
using Department = TypedIdsDemo.Models.Department;

namespace Benchmark;

[MemoryDiagnoser]
public class ReadBenchmark
{
    private UntypedContext untypedContext;
    private TypedContext typedContext;
    
    [GlobalSetup]
    public void GlobalSetup()
    {
        untypedContext = new UntypedContext(new DbContextOptionsBuilder()
            .UseNpgsql("Database=UntypedIdsTest;Host=127.0.0.1;Port=5432;User ID=postgres;Password=1234;").Options);
        typedContext = new TypedContext(new DbContextOptionsBuilder()
            .UseNpgsql("Database=TypedIdsTest;Host=127.0.0.1;Port=5432;User ID=postgres;Password=1234;").Options);
        
        untypedContext.Departments.RemoveRange(untypedContext.Departments);
        untypedContext.Employees.RemoveRange(untypedContext.Employees);
        untypedContext.SaveChanges();
        
        typedContext.Departments.RemoveRange(typedContext.Departments);
        typedContext.Employees.RemoveRange(typedContext.Employees);
        typedContext.SaveChanges();
        
        typedContext.Departments.AddRange(Enumerable.Range(0, 10_000).Select(_ => new Department
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
        
        untypedContext.Departments.AddRange(Enumerable.Range(0, 10_000).Select(_ => new NotTypedIdsDemo.Models.Department
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

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        untypedContext.Departments.RemoveRange(untypedContext.Departments);
        untypedContext.Employees.RemoveRange(untypedContext.Employees);
        untypedContext.SaveChanges();
        
        typedContext.Departments.RemoveRange(typedContext.Departments);
        typedContext.Employees.RemoveRange(typedContext.Employees);
        typedContext.SaveChanges();
    }


    [Benchmark]
    public void TypedIdsBenchmark()
    {
        var departments = typedContext.Departments.ToList();
    }
    
    [Benchmark]
    public void UntypedIdsBenchmark()
    {
        var departments = untypedContext.Departments.ToList();
    }
}