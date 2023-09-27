namespace TypedIdsDemo.Models.Ids;

public readonly record struct ExternalEmployeeId(int Value) : ITypedId;