using ForDevs.Services.Api.Configurations;
using ForDevs.Services.Api.Extensions;
using MediatR;

namespace ForDevs.Services.Api
{
    public interface IStartup
    {
        IConfiguration Configuration { get; }
        void Configure(WebApplication app, IWebHostEnvironment environment);
        void ConfigureServices(IServiceCollection services);
    }

    public class Startup : IStartup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseConfiguration(Configuration);

            services.AddApiConfiguration(Configuration);

            services.AddIdentityConfiguration(Configuration);
            
            services.AddMediatR(typeof(Program));

            services.AddSwaggerConfiguration();

            services.RegisterServices();
        }

        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseSwaggerConfiguration();

            app.UseApiConfiguration(environment);
        }
    }
}
