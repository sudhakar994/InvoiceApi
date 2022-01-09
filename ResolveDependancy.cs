using InvoiceApi.IRepository;
using InvoiceApi.IServices;
using InvoiceApi.Repository;
using InvoiceApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;


namespace InvoiceApi
{
    public static class ResolveDependancy
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<IJwtService, JwtService>();
            services.AddTransient<IHtmlReaderService, HtmlReaderService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserReposiotry, UserRepository>();
            services.AddHttpContextAccessor();
            services.AddSingleton<ISqlService, SqlService>();

        }
    }
}
