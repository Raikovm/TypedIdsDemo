namespace TypedIdsDemo.Models;

public class Department
{
    public DepartmentId Id { get; init; }

    public required string Name { get; init; }

    public List<Employee> Employees { get; init; } = new();
}