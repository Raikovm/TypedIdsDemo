namespace TypedIdsDemo.Data;

public class TypedContext : DbContext
{
    public TypedContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasTypedIdPrimaryKey(x => x.Id);
        modelBuilder.Entity<Employee>()
            .HasTypedIdProperty(x => x.ExternalId);
        modelBuilder.Entity<Employee>()
            .HasOne(x => x.Department)
            .WithMany(x => x.Employees)
            .HasForeignKey(x => x.DepartmentId);

        modelBuilder.Entity<Department>()
            .HasTypedIdPrimaryKey(x => x.Id);
        modelBuilder.Entity<Department>()
            .HasMany(x => x.Employees)
            .WithOne(x => x.Department)
            .HasForeignKey(x => x.DepartmentId);
    }

    public DbSet<Employee> Employees => Set<Employee>();

    public DbSet<Department> Departments => Set<Department>();
}

