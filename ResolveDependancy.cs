using InvoiceApi.IRepository;
using InvoiceApi.IServices;
using InvoiceApi.Repository;
using InvoiceApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

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
            services.AddTransient<IEmailService, EmailService>();
            services.AddHttpContextAccessor();
            services.AddSingleton<ISqlService, SqlService>();
            services.AddTransient<IMasterValueRepository, MasterValueRepository>();
            services.AddTransient<IDashboardService, DashboardService>();
            services.AddTransient<IDashboardRepository, DashboardRepository>();
            services.AddTransient<IInvoiceService, InvoiceService>();
            services.AddTransient<IInvoiceRepository, InvoiceRepository>();
        }
    }
}
