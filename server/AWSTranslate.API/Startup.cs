using Amazon.Translate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Microsoft.EntityFrameworkCore;
using AWSTranslate.API.Mappings;
using AWSTranslate.API.Repositories.DL;
using Amazon.Translate.Model;
using AWSTranslate.API.Repositories.BL;

namespace AWSTranslate.API
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

            services.AddDbContext<DbContext>(options =>
            {
                options.UseNpgsql(
                Configuration.GetConnectionString("DbConn"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
   
            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AWSTranslate.API", Version = "v1" });
            });
            services.AddAWSService<IAmazonTranslate>(new AWSOptions
            {
                Region = RegionEndpoint.APSouth1, // Change to your desired AWS region
                                                 
                Credentials = new BasicAWSCredentials(Configuration["AWS:AccessKey"], Configuration["AWS:SecretKey"])
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
            });

            services.AddDistributedRedisCache(
                options =>
                {
                    options.Configuration = "localhost:6379";
                });

            services.AddScoped<IDataRepository, DataRepository>();
            services.AddScoped<IAwsRepository, AwsRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AWSTranslate.API v1"));
            }
            app.UseCors("AllowOrigin");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
