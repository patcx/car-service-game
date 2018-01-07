using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarServiceGame.Domain.Concrete;
using CarServiceGame.Domain.Contracts;
using CarServiceGame.Domain.Database;
using CarServiceGame.Domain.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CarServiceGame.WebAPI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var secretKey = "secret_key_car_service_game@!";
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                     .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = tokenValidationParameters;
                });

            services.Configure<MailCredentialsOptions>(x =>
            {
                x.User = "carservicegame@gmail.com";
                x.Password = "Qaz123123";
            });

            services.AddMvc();
            services.AddApiVersioning();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IGarageRepository, GarageRepository>();
            services.AddTransient<IWorkerRepository, WorkerRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IMailClient, GmailMailClient>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Dostosować do odpowiednich domen
            app.UseCors(builder =>
                 builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
