using System.Globalization;
using Brewery.Datalayer.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;

namespace hive.core.api
{
    public class Startup
    {
        
        private readonly IWebHostEnvironment _env;
        private readonly IHostEnvironment _host;
        
        
        public Startup(IConfiguration configuration, IWebHostEnvironment env, IHostEnvironment host)
        {
            Configuration = configuration;
            _env = env;
            _host = host;
        }
        
        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // var service = Configuration.GetSection("Service").Get<Service>();
            // services.Configure<configuration.AWS>(Configuration.GetSection("AWS"));
            // services.Configure<Service>(Configuration.GetSection("Service"));
            // services.Configure<configuration.Stripe>(Configuration.GetSection("Stripe"));

            

            
            //  services.AddApiVersioning(config =>
            // {
            //     config.DefaultApiVersion = new ApiVersion(1, 0);
            //     config.AssumeDefaultVersionWhenUnspecified = true;
            //     config.ReportApiVersions = true;
            //     config.ApiVersionReader = ApiVersionReader.Combine(
            //         new QueryStringApiVersionReader("api-version"),
            //         new HeaderApiVersionReader("api-version"));
            // });


            services.AddDbContext<BreweryContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("BreweryContext"),
                    ServerVersion.AutoDetect(Configuration.GetConnectionString("BreweryContext"))));


            // services.AddVersionedApiExplorer(options =>
            // {
            //     // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
            //     // note: the specified format code will format the version as "'v'major[.minor][-status]"
            //     options.GroupNameFormat = "'v'VVV";
            //
            //     // note: this option is only necessary when versioning by url segment. the SubstitutionFormat≠≠
            //     // can also be used to control the format of the API version in route templates
            //     options.SubstituteApiVersionInUrl = true;
            // });
            
            services.AddSwaggerGen();


    
   

            //Add Services
            //services.AddTransient<IWorkspaceService, WorkspaceService>();


       
            
            //Add Repositories
             //services.AddTransient<IUserRepository, UserRepository>();
  
             

  


            //Automapper
            //services.AddAutoMapper(typeof(Startup));

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void  Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseSwagger();
            app.UseSwaggerUI();
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapHub<AlertHub>("/alertHub");
                endpoints.MapControllers();
            });
    
            
        }
    }
}
