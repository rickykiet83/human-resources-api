using System;
using Entities;
using HumanResourceAPI.Infrastructure;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HumanResourceAPI.Utility
{
    public static class MigrationManager
    {
        private static readonly ILoggerManager _logger = new LoggerManager();
        
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>())
                {
                    try
                    {
                        appContext.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                        throw ex;
                    }
                }
            }

            return host;
        }
    }
}