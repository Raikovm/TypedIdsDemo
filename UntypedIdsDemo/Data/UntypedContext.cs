using NotTypedIdsDemo.Models;

namespace NotTypedIdsDemo.Data;

public class UntypedContext : DbContext
{
    public UntypedContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<Employee>()
            .HasOne(x => x.Department)
            .WithMany(x => x.Employees)
            .HasForeignKey(x => x.DepartmentId);

        modelBuilder.Entity<Department>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<Department>()
            .HasMany(x => x.Employees)
            .WithOne(x => x.Department)
            .HasForeignKey(x => x.DepartmentId);
    }

    public DbSet<Employee> Employees => Set<Employee>();

    public DbSet<Department> Departments => Set<Department>();
}