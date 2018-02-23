﻿using System;
using System.IO;
using System.Text;
using CryptoManager.WebApi.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace CryptoManager.WebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            WebUtil.JwtKeyName = Configuration.GetSection("JwtKeyName").Value;

            services.AddSingleton(typeof(JwtFactory));

            services.AddCors();
            services.AddMvc();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        //ValidAudience = "the audience you want to validate",
                        ValidateIssuer = false,
                        //ValidIssuer = "the isser you want to validate",

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(WebUtil.JwtKeyName)),

                        ValidateLifetime = true, //validate the expiration and not before values in the token

                        ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                    };
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new Info
                {
                    Title = "CryptoManager API",
                    Version = "v1",
                    Description = "API to Manage Crypto Currencies using Exchanges APIs",
                    Contact = new Contact { Name = "Rômulo Nissóla Rocha", Email = "romulonissola@gmail.com", Url = "https://github.com/romulonissola/CryptoManager" },
                    License = new License { Name = "GNU General Public License v2.0", Url = "https://www.gnu.org/licenses/old-licenses/gpl-2.0.html" }
                });

                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "CryptoManager.WebApi.xml");
                setup.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CryptoManager API V1");
            });

            app.UseMvc();
        }
    }
}
