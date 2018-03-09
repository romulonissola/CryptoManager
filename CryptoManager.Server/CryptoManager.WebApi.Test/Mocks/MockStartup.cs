﻿using CryptoManager.WebApi.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using CryptoManager.Repository;
using CryptoManager.Repository.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using CryptoManager.Domain.Mapper;

namespace CryptoManager.WebApi.Test.Mocks
{
    public class MockStartup<TStartup> : IDisposable
    {
        private const string SOLUTION_NAME = "CryptoManager.Server.sln";
        private readonly TestServer _server;
        public static MockStartup<TStartup> Instance { get; private set; }

        static MockStartup()
        {
            Instance = new MockStartup<TStartup>();
        }

        public IConfiguration _configuration { get; }

        private MockStartup()
        {
            var startupAssembly = typeof(TStartup).GetTypeInfo().Assembly;
            var contentRoot = GetProjectPath(string.Empty, startupAssembly);

            var configbuilder = new ConfigurationBuilder()
                .SetBasePath(contentRoot)
                .AddEnvironmentVariables();
            configbuilder.AddUserSecrets<MockStartup<Startup>>();
            _configuration = configbuilder.Build();

            var builder = new WebHostBuilder()
                .UseContentRoot(contentRoot)
                .ConfigureServices(ConfigureServices)
                .UseEnvironment("Development")
                .UseStartup(typeof(TStartup))
                .Configure(Configure);

            _server = new TestServer(builder);
        }

        public void Dispose()
        {
            _server.Dispose();
        }

        public HttpClient GetCliente()
        {
            return _server.CreateClient();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            TestWebUtil.JwtKeyName = _configuration["JwtKeyName"];
            TestWebUtil.FacebookAppId = _configuration["Authentication:Facebook:AppId"];
            TestWebUtil.FacebookAppSecret = _configuration["Authentication:Facebook:AppSecret"];
            TestWebUtil.FacebookAccessToken = _configuration["Authentication:Facebook:AccessToken"];

            services.AddDbContext<ApplicationIdentityDbContext>(options =>
            {
                options.UseInMemoryDatabase("DBINTEGRATIONTEST")
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });

            services.AddDbContext<EntityContext>(options =>
            {
                options.UseInMemoryDatabase("DBINTEGRATIONTEST")
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });

            services.AddORM();
            services.AddRepositories();
           
            services.AddSingleton(typeof(JwtFactory));

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfileConfiguration());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);


            services.AddCors();
            services.AddMvc();
        }


        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });

            app.UseMvc();
        }
        /// <summary>
        /// Gets the full path to the target project path that we wish to test
        /// </summary>
        /// <param name="solutionRelativePath">
        /// The parent directory of the target project.
        /// e.g. src, samples, test, or test/Websites
        /// </param>
        /// <param name="startupAssembly">The target project's assembly.</param>
        /// <returns>The full path to the target project.</returns>
        private static string GetProjectPath(string solutionRelativePath, Assembly startupAssembly)
        {
            // Get name of the target project which we want to test
            var projectName = startupAssembly.GetName().Name;

            // Get currently executing test project path
            var applicationBasePath = PlatformServices.Default.Application.ApplicationBasePath;

            // Find the folder which contains the solution file. We then use this information to find the target
            // project which we want to test.
            var directoryInfo = new DirectoryInfo(applicationBasePath);
            do
            {
                var solutionFileInfo = new FileInfo(Path.Combine(directoryInfo.FullName, SOLUTION_NAME));
                if (solutionFileInfo.Exists)
                {
                    return Path.GetFullPath(Path.Combine(directoryInfo.FullName, solutionRelativePath, projectName));
                }

                directoryInfo = directoryInfo.Parent;
            }
            while (directoryInfo.Parent != null);

            throw new Exception($"Solution root could not be located using application root {applicationBasePath}.");
        }
    }
}
