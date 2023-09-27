WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StronglyTypedContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Test")));

builder.Services.AddAutoMapper(config =>
{
    config.CreateMap<EmployeeId, int>()
        .ConvertUsing(x => x.Value);
    config.CreateMap<ExternalEmployeeId, int>()
        .ConvertUsing(x => x.Value);
    config.CreateMap<DepartmentId, int>()
        .ConvertUsing(x => x.Value);
    config.CreateMap<Department, DepartmentDto>();
    config.CreateMap<Employee, EmployeeDto>();
});

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/employees", ([FromServices] StronglyTypedContext context, [FromServices] IMapper mapper) =>
    mapper.Map<IEnumerable<EmployeeDto>>(context.Employees.ToList()));

app.MapGet("/employees/{id}", ([FromServices] StronglyTypedContext context, [FromServices] IMapper mapper, int id) =>
    mapper.Map<EmployeeDto>(context.Employees.FirstOrDefault(x => x.Id == new EmployeeId(id))));

app.MapPost("/employees",
    ([FromServices] StronglyTypedContext context,
        [FromServices] IMapper mapper,
        string name,
        int departmentId,
        int externalId) =>
    {
        Employee employee = new()
        {
            DepartmentId = new(departmentId),
            Name = name,
            ExternalId = new(externalId),
        };

        context.Employees.Add(employee);
        context.SaveChanges();

        return mapper.Map<EmployeeDto>(employee);
    });

app.MapGet("/departments",
    ([FromServices] StronglyTypedContext context, [FromServices] IMapper mapper) =>
        mapper.Map<IEnumerable<DepartmentDto>>(context.Departments.Include(x => x.Employees).ToList()));

app.MapGet("/departments{id}", ([FromServices] StronglyTypedContext context, [FromServices] IMapper mapper, int id) =>
    mapper.Map<DepartmentDto>(context.Departments.FirstOrDefault(x => x.Id == new DepartmentId(id))));

app.MapPost("/departments", ([FromServices] StronglyTypedContext context, [FromServices] IMapper mapper, string name) =>
{
    Department department = new() { Name = name };

    context.Departments.Add(department);
    context.SaveChanges();

    return mapper.Map<DepartmentDto>(department);
});

app.Run();
