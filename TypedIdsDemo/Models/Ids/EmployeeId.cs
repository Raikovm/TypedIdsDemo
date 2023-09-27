namespace TypedIdsDemo.Models.Ids;

public readonly record struct EmployeeId(int Value) : ITypedId;