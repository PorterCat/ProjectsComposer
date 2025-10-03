using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectsComposer.API.Services;
using ProjectsComposer.API.Services.Conflicts;
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
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
        };
    });

builder.Services.AddScoped<IProjectsRepository, ProjectsRepository>();
builder.Services.AddScoped<IProjectsService, ProjectsService>();

builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();
builder.Services.AddScoped<IEmployeesService, EmployeesService>();

builder.Services.AddScoped<IPendingCasesStore, PendingCasesStore>();

builder.Services.AddControllers();
var app = builder.Build();

app.UseAuthorization();
app.UseAuthentication();

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
app.MapControllers();

app.Run();

public partial class Program { }