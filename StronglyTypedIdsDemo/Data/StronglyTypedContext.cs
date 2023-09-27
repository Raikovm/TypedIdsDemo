namespace StronglyTypedIdsDemo.Data;

public class StronglyTypedContext : DbContext
{
    public StronglyTypedContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<Employee>().Property(x => x.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Employee>()
            .HasOne(x => x.Department)
            .WithMany(x => x.Employees)
            .HasForeignKey(x => x.DepartmentId);

        modelBuilder.Entity<Department>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<Department>()
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Department>()
            .HasMany(x => x.Employees)
            .WithOne(x => x.Department)
            .HasForeignKey(x => x.DepartmentId);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<EmployeeId>()
            .HaveConversion<EmployeeId.EfCoreValueConverter>();
        configurationBuilder.Properties<DepartmentId>()
            .HaveConversion<DepartmentId.EfCoreValueConverter>();
        configurationBuilder.Properties<ExternalEmployeeId>()
            .HaveConversion<ExternalEmployeeId.EfCoreValueConverter>();
    }

    public DbSet<Employee> Employees => Set<Employee>();

    public DbSet<Department> Departments => Set<Department>();
}