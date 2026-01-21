// using fenixjobs_api.Application.Interfaces;
// using fenixjobs_api.Application.Services;
using fenixjobs_api.Infrastructure.Persistence.MongoDB;
using fenixjobs_api.Infrastructure.Persistence.MySQL;
// using fenixjobs_api.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace fenixjobs_api.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var sqlConnection = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<FenixDbContext>(options =>
                options.UseMySql(sqlConnection,
                    Microsoft.EntityFrameworkCore.ServerVersion.AutoDetect(sqlConnection),
                    b => b.MigrationsAssembly(typeof(FenixDbContext).Assembly.FullName)
                ));

            var mongoConnection = configuration.GetConnectionString("MongoConnection");

            services.AddSingleton<IMongoClient>(sp => new MongoClient(mongoConnection));

            services.AddScoped<IMongoDatabase>(sp => {
                var client = sp.GetRequiredService<IMongoClient>();
                var databaseName = MongoUrl.Create(mongoConnection).DatabaseName;
                return client.GetDatabase(databaseName);
            });

            return services;
        }
    }
}
