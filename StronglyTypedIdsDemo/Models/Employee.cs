namespace StronglyTypedIdsDemo.Models;

public class Employee
{
    public EmployeeId Id { get; init; }
    
    public required ExternalEmployeeId ExternalId { get; init; }
    
    public required string Name { get; init; }
    
    public  DepartmentId DepartmentId { get; init; }
    
    public Department? Department { get; init; }
}