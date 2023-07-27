using ExpenseTracker.DAL.Contracts;
using ExpenseTracker.DAL.Models;
using ExpenseTracker.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.DAL
{
    public class DALConfig
    {
        public static void Configure(IServiceCollection services, ConfigurationManager config)
        {
            services.AddDbContext<ExpenseTrackerContext>(
                                options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            Console.WriteLine(config.GetConnectionString("DefaultConnection"));
            services.AddScoped<IUserCredentialsRepository, UserCredentialsRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        }
    }
}