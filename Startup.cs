using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceApi.Constants;
using InvoiceApi.IServices;
using InvoiceApi.Models;
using InvoiceApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace InvoiceApi
{
    public class Startup
    {
        #region  Variable Declaration

        private  readonly string CipherKey = "xafmg2H0bLk2kZc0PvklMQ==";
        private  readonly string CipherIV = "VcCvRGkh9Z3NyN/09/Cspg==";
        #endregion
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            var secret = Configuration.GetSection("AppSettings").GetSection("SecretKey").Value;
            var key = Encoding.ASCII.GetBytes(secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            services.AddMvc();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });


            services.AddControllers();

            services.AddSingleton<IConfiguration>(Configuration);

            // Encrypted Conncetion String

            SqlHelper.ConnectionString = Configuration.GetConnectionString("InvoiceApiDB");
            services.Configure<SMTPConfig>(Configuration.GetSection("SMTPConfig"));
            //Resolve Dependancy Injection

            ResolveDependancy.RegisterServices(services);
            services.AddSwaggerGen();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<JwtMiddleware>();
            app.UseExceptionHandlerMiddleware();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(x => x
         .SetIsOriginAllowed(origin => true)
         .AllowAnyMethod()
         .AllowAnyHeader()
         .AllowCredentials());
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Eforms Buddy Api v1");
            });

        }
    }
}
