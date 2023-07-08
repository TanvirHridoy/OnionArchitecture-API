using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RepositoryLayer;
using RepositoryLayer.RespositoryPattern;
using ServicesLayer.CustomerService;
using Oracle.ManagedDataAccess; //Oracle.EntityFrameworkCore;
using RepositoryLayer.ConnectionManager;
using RepositoryLayer.QueryBuilder;
using Microsoft.AspNetCore.Identity;
using ServicesLayer.Data.EKyc;

namespace OnionArchitecture
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OnionArchitecture", Version = "v1" });
            });

            services.AddCors(options => options
            .AddPolicy(name: "dcbs", policy
            => { policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
            }));

            #region Connection String
            services.AddDbContext<ApplicationDbContext>(item => item.UseOracle(Configuration.GetConnectionString("OraConnectionString")));
            //services.AddDbContext<ApplicationDbContext>(item => item.UseSqlServer(Configuration.GetConnectionString("myconn")));
            
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            #endregion

            #region Services Injected
                        
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			services.AddScoped<IOraConnection, OraConnection>();
            services.AddScoped(typeof(IOraQueryBuilder<>), typeof(OraQueryBuilder<>));
			services.AddScoped(typeof(IOraQueryBuilder<>), typeof(OraQueryBuilder<>));

			services.AddScoped<ICustomerService, CustomerService>();
			services.AddScoped<IOtpService, OtpService>();

			#endregion
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnionArchitecture v1"));
            }
            app.UseCors("dcbs");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
