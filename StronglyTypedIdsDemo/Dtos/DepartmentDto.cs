namespace StronglyTypedIdsDemo.Dtos;

public record DepartmentDto(int Id, string Name, List<EmployeeDto> Employees);
