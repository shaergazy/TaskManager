using System.Linq;
using System.Reflection.Emit;
using DAL.Entities;
using DAL.Entities.Users;
using DAL.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF;

public class AppDbContext : IdentityDbContext<User, Role, string,
    IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>,
    IdentityRoleClaim<string>, IdentityUserToken<string>>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<Project> Projects { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Document> Documents { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        
        builder.Entity<UserRole>()
            .HasOne(x => x.User)
            .WithMany(x => x.Roles)
            .HasForeignKey(x => x.UserId)
            .IsRequired();

        builder.Entity<UserRole>()
            .HasOne(x => x.Role)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.RoleId)
        .IsRequired();

        builder.Entity<ProjectEmployee>()
             .HasKey(pa => new { pa.ProjectId, pa.EmployeeId });

        builder.Entity<ProjectEmployee>()
            .HasOne(x => x.Project)
            .WithMany(x => x.Employees)
            .HasForeignKey(x => x.ProjectId)
            .IsRequired();

        builder.Entity<ProjectEmployee>()
            .HasOne(x => x.Employee)
            .WithMany(x => x.Projects)
            .HasForeignKey(x => x.EmployeeId)
            .IsRequired();

        foreach (var x in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            x.DeleteBehavior = DeleteBehavior.ClientCascade;
    }
}
