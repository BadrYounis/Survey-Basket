using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using SurveyBasket.Authentication;
using SurveyBasket.Health;
using SurveyBasket.Settings;
using System.Text;
using System.Threading.RateLimiting;

namespace SurveyBasket;
public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();

        services.AddHybridCache();

        services.AddCors(options =>
            options.AddDefaultPolicy(builder =>
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins(configuration.GetSection("AllowedOrigins").Get<string[]>()!)
            )
        );

        services.AddAuthConfig(configuration);

        var connectionString = configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services
            .AddSwaggerServices()
            .AddMapsterConfig()
            .AddFluentValidationConfig();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IPollService, PollService>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IVoteService, VoteService>();
        services.AddScoped<IResultService, ResultService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IEmailSender, EmailService>();

        //Add Exception Handling
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();  //Without calling it, ASP.NET Core does not automatically format errors using the Problem Details standard.
        services.AddBackgroundJobsConfig(configuration);

        services.AddHttpContextAccessor();

        services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));

        services.AddHealthChecks()
            .AddSqlServer(name: "Database", connectionString: connectionString)
            .AddHangfire(options => { options.MinimumAvailableServers = 1; })
            .AddCheck<MailProviderHealthCheck>(name: "Mail Provider");

        services.AddRateLimitingConfig();

        services.AddApiVersioning();

        return services;
    }
    private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
    private static IServiceCollection AddMapsterConfig(this IServiceCollection services)
    {
        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton<IMapper>(new Mapper(mappingConfig));

        return services;
    }
    private static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
    {
        services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
    private static IServiceCollection AddAuthConfig(this IServiceCollection services,
        IConfiguration _configuration)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        //Jwt Configurations
        services.AddSingleton<IJwtProvider, JwtProvider>();  // We Need Only 1 Instance Through Lifetime Of Application

        // Options Pattern Configurations
        //services.Configure<JwtOptions>(_configuration.GetSection(nameof(JwtOptions)));

        services.AddOptions<JwtOptions>()
            .BindConfiguration(nameof(JwtOptions))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var _jwtsettings = _configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

        // When Using [Authorize] Attribute In Controller or Action => Will Know That We Use Bearer [Bearer Tokens]
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtsettings?.Key!)),
                ValidIssuer = _jwtsettings?.Issuer,
                ValidAudience = _jwtsettings?.Audience
            };
        });

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 8;
            options.SignIn.RequireConfirmedEmail = true;
            options.User.RequireUniqueEmail = true;
        });

        return services;
    }
    private static IServiceCollection AddBackgroundJobsConfig(this IServiceCollection services,
       IConfiguration _configuration)
    {
        services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(_configuration.GetConnectionString("HangfireConnection")));

        services.AddHangfireServer();

        return services;
    }
    private static IServiceCollection AddRateLimitingConfig(this IServiceCollection services)
    {
        services.AddRateLimiter(rateLimiterOptions =>
        {
            rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            rateLimiterOptions.AddPolicy(RateLimiters.IpLimiter, httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 2,
                        Window = TimeSpan.FromSeconds(20)
                    }
                )
            );

            rateLimiterOptions.AddPolicy(RateLimiters.UserLimiter, httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.User.GetUserId(),
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 2,
                        Window = TimeSpan.FromSeconds(20)
                    }
                )
            );

            rateLimiterOptions.AddConcurrencyLimiter(RateLimiters.Concurrency, options =>
            {
                options.PermitLimit = 1000;
                options.QueueLimit = 100;
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            });

            #region TokenBucketLimiter
            ///rateLimiterOptions.AddTokenBucketLimiter("token", options =>
            ///{
            ///    options.TokenLimit = 2;
            ///    options.QueueLimit = 1;
            ///    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            ///    options.ReplenishmentPeriod = TimeSpan.FromSeconds(30);
            ///    options.TokensPerPeriod = 2;
            ///    options.AutoReplenishment = true;
            ///}); 
            #endregion

            #region FixedWindowLimiter
            ///rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
            ///{
            ///    options.PermitLimit = 2;
            ///    options.Window = TimeSpan.FromSeconds(20);
            ///    options.QueueLimit = 1;
            ///    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;                
            ///}); 
            #endregion

            #region SlidingWindowLimiter
            ///rateLimiterOptions.AddSlidingWindowLimiter("sliding", options =>
            ///{
            ///    options.PermitLimit = 2;
            ///    options.Window = TimeSpan.FromSeconds(20);
            ///    options.SegmentsPerWindow = 2;
            ///    options.QueueLimit = 1;
            ///    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;                
            ///}); 
            #endregion

        });
        return services;
    }
}