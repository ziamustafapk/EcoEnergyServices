using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.EntityFrameworkCore;
using YourProjectName.Infrastructure.DataContext;
using YourProjectName.Infrastructure.Repositories;
using YourProjectName.Services;
using YourProjectName.Utility;

namespace YourProjectName.Extensions
{
    public static class ServiceExtensions
    {
        // SQL Connection Configuration
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<YourProjectNameDbContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("YourProjectNameSqlConnection"))
            );

        // Repository Manager Configuration
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        // Service Manager Configuration
        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();

        // PDF Service Configuration
        public static void ConfigurePdfService(this IServiceCollection services)
        {
            var context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll"));

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

        }



    }
}
