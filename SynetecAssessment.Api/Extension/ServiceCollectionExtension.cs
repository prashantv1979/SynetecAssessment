using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SynetecAssessment.Core.Interface;
using SynetecAssessment.Core.Services;
using SynetecAssessment.Data;
using SynetecAssessment.Data.Interface;

namespace SynetecAssessment.Api.Extension
{
    internal static class ServiceCollectionExtension
    {


        public static IServiceCollection AddBonusPoolServices(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: "HrDb"));
            services.AddTransient<IDbContextGenerator, DbContextGenerator>();
            services.AddTransient<IBonusPoolService, BonusPoolService>();
            return services;
        }
    }
}
