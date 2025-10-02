using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using ProjectsComposer.DataAccess;

namespace ProjectsComposer.Tests;

public class CustomWebApplicationFactory<TProgram>
        : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(IDbContextOptionsConfiguration<ProjectsComposerDbContext>));

                if (dbContextDescriptor != null)
                    services.Remove(dbContextDescriptor);

                var dbConnectionDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbConnection));

                if (dbConnectionDescriptor != null)
                    services.Remove(dbConnectionDescriptor);
                
                services.AddSingleton<DbConnection>(container =>
                {
                    var configuration = container.GetRequiredService<IConfiguration>();
                    var connString = configuration.GetConnectionString("ProjectsComposerDbContext");
                    var connection = new NpgsqlConnection(connString);
                    connection.Open();
                    return connection;
                });

                services.AddDbContext<ProjectsComposerDbContext>((container, options) =>
                {
                    var connection = container.GetRequiredService<DbConnection>();
                    options.UseNpgsql((NpgsqlConnection)connection);
                });
                
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var ctx = scope.ServiceProvider.GetRequiredService<ProjectsComposerDbContext>();
                ctx.Database.Migrate();
            });

            builder.UseEnvironment("Development");
        }
    }