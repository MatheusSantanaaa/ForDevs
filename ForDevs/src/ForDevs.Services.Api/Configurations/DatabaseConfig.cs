using ForDevs.Infra.CrossCutting;
using ForDevs.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ForDevs.Services.Api.Configurations
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ForDevsContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}
