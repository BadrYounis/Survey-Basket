using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;

namespace SurveyBasket.Persistence;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> _options, IHttpContextAccessor httpContextAccessor) :
    IdentityDbContext<ApplicationUser>(_options)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<AuditableEntity>();

        foreach (var entryEntity in entries)
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            if (entryEntity.State == EntityState.Added)
            {
                entryEntity.Property(x => x.CreatedById).CurrentValue = currentUserId;
            }
            else if (entryEntity.State == EntityState.Modified)
            {
                entryEntity.Property(x => x.UpdatedById).CurrentValue = currentUserId;
                entryEntity.Property(x => x.UpdatedOn).CurrentValue = DateTime.UtcNow;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
    public DbSet<Poll> Polls { get; set; }
}