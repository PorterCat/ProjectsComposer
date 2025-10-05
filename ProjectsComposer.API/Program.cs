using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectsComposer.API.Services;
using ProjectsComposer.API.Services.Conflicts;
using ProjectsComposer.BuisnessLogic;
using ProjectsComposer.DataAccess;
using ProjectsComposer.DataAccess.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ProjectsComposerDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(ProjectsComposerDbContext)));
});

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // TODO: Outline it to extenstion method
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AuthSettings")["SecretKey"] ?? string.Empty)),
        };
    });

builder.Services.AddScoped<IProjectsRepository, ProjectsRepository>();
builder.Services.AddScoped<IProjectsService, ProjectsService>();

builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();
builder.Services.AddScoped<IEmployeesService, EmployeesService>();

builder.Services.AddScoped<IPendingCasesStore, PendingCasesStore>();

// Authentification block
builder.Services.AddScoped<AccountsRepository>();
builder.Services.AddScoped<AccountsService>();
builder.Services.AddScoped<JwtService>();

builder.Services.Configure<AuthSettings>(
    builder.Configuration.GetSection(nameof(AuthSettings)));

builder.Services.AddControllers();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "ProjectsComposer API";
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}
app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }