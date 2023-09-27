namespace NotTypedIdsDemo.Models;

public class Employee
{
    public int Id { get; init; }

    public required int ExternalId { get; init; }

    public required string Name { get; init; }

    public int? DepartmentId { get; init; }

    public Department? Department { get; init; }
}