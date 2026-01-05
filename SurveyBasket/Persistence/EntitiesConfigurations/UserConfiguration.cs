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
            Id = DefaultUsers.Admin.Id,
            FirstName = "Survey Basket",
            LastName = "Admin",
            UserName = DefaultUsers.Admin.Email,
            NormalizedUserName = DefaultUsers.Admin.Email.ToUpper(),
            Email = DefaultUsers.Admin.Email,
            NormalizedEmail = DefaultUsers.Admin.Email.ToUpper(),
            SecurityStamp = DefaultUsers.Admin.SecurityStamp,
            ConcurrencyStamp = DefaultUsers.Admin.ConcurrencyStamp,
            EmailConfirmed = true,                                          //Important
            PasswordHash = DefaultUsers.Admin.Password
        });
    }
}