using InvoiceApi.IRepository;
using InvoiceApi.IServices;
using InvoiceApi.Repository;
using InvoiceApi.Services;
using Microsoft.Extensions.DependencyInjection;


namespace InvoiceApi
{
    public static class ResolveDependancy
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<IJwtService, JwtService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserReposiotry, UserRepository>();
            services.AddHttpContextAccessor();
        }
    }
}
