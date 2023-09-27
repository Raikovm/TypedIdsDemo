namespace NotTypedIdsDemo.Models;

public class Department
{
    public int Id { get; init; }
    
    public required string Name { get; init; }

    public List<Employee> Employees { get; init; } = new();
}