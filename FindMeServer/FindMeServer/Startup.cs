using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceImplementations;
using ApplicationCore.ServiceInterfaces;
using AutoMapper;
using Infrastructure.Database;
using Infrastructure.RepositoryImplementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FindMeServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFrameworkSqlServer();
            services.AddDbContext<FindMeDbContext>(options => options.
                                    UseSqlServer(this.Configuration.GetConnectionString("AzureDb"),
                                    sqlOptions => sqlOptions.MigrationsAssembly("FindMeServer")));
            #region Repositories
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<ILostRepository, LostRepository>();
            services.AddTransient<IInstitutionRepository, InstitutionRepository>();
            services.AddTransient<IInstitutionsTypeRepository, InstitutionsTypeRepository>();
            #endregion
            #region Services
            services.AddTransient<ISubscribeService, SubscribeService>();
            services.AddTransient<IDataService, DataService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            #endregion
            services.AddAutoMapper();
            services.AddScoped<FindMeDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}