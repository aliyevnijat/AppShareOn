using System.Net;
using System.Net.Mail;
using System.Text;
using AppShareOn.Application.Interfaces;
using AppShareOn.Application.Services;
using AppShareOn.Core.Interfaces;
using AppShareOn.Infrastructure;
using AppShareOn.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Register application services.
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IPlatformService, PlatformService>();
builder.Services.AddTransient<IProfileService, ProfileService>();

builder.Services.AddHttpClient("AppshareonClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7241");
});

// Setup SmtpClient
var emailSettings = builder.Configuration.GetSection("EmailSettings").Get<EmailSettings>();
if (emailSettings != null)
{
    builder.Services.AddSingleton<SmtpClient>(sp =>
    {
        var smtpClient = new SmtpClient(emailSettings.SmtpServer)
        {
            Port = emailSettings.Port,
            Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password),
            EnableSsl = emailSettings.EnableSsl
        };

        return smtpClient;
    });
}

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// Generates json at /openapi/v1.json
builder.Services.AddOpenApi();

// Setup Db Context with Sqlite.
builder.Services.AddDbContext<AppshareonDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Setup Identity with EF Core.
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = true;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;
})
.AddEntityFrameworkStores<AppshareonDbContext>()
.AddDefaultTokenProviders();

// Configure Authentication.
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings["SecretKey"] ?? "")),
        ValidateLifetime = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
