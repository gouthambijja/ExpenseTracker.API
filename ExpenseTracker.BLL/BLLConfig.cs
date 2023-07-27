using ExpenseTracker.DAL;
using ExpenseTrackerLogicLayer.Contracts;
using ExpenseTrackerLogicLayer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.BLL
{
    public class BLLConfig
    {
        public static void Configure(IServiceCollection services, ConfigurationManager config)
        {
            DALConfig.Configure(services, config);
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ICategoryService,CategoryService>();
        }
    }
}