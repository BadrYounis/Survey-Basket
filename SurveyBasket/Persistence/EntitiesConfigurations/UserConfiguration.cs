namespace SurveyBasket.Persistence.EntitiesConfigurations;
public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .OwnsMany(p => p.RefreshTokens)
            .ToTable("RefreshTokens")
            .WithOwner()
            .HasForeignKey("UserId");

        builder.Property(p => p.FirstName).HasMaxLength(100);
        builder.Property(p => p.LastName).HasMaxLength(100);

        //Default Data
        builder.HasData(new ApplicationUser
        {
            Id = DefaultUsers.AdminId,
            FirstName = "Survey Basket",
            LastName = "Admin",
            UserName = DefaultUsers.AdminEmail,
            NormalizedUserName = DefaultUsers.AdminEmail.ToUpper(),
            Email = DefaultUsers.AdminEmail,
            NormalizedEmail = DefaultUsers.AdminEmail.ToUpper(),
            SecurityStamp = DefaultUsers.AdminSecurityStamp,
            ConcurrencyStamp = DefaultUsers.AdminConcurrencyStamp,
            EmailConfirmed = true,                                          //Important
            PasswordHash = DefaultUsers.AdminPassword
        });
    }
}