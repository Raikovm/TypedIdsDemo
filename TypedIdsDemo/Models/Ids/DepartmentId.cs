namespace TypedIdsDemo.Models.Ids;

public readonly record struct DepartmentId(int Value) : ITypedId;