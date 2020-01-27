using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibraryDataContext;
using LibraryRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LibraryAPI
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
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ILibraryManager, LibraryManager>();
            services.AddDbContext<LibraryContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnectionString"));
                });
            //services.AddSwaggerGen(setupAction =>
            //{
            //    setupAction.SwaggerDoc(
            //                           "LibraryOpenAPISpecification",
            //                            new Microsoft.OpenApi.Models.OpenApiInfo
            //                            {
            //                                Title = "LibraryManagement",
            //                                Version = "1.0.0"
            //                            }
            //                            ); ;
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();
            //app.UseSwagger();
            //app.UseSwaggerUI(setupAction =>
            //{
            //    setupAction.SwaggerEndpoint("/swagger/LibraryOpenAPISpecification/swagger.json",
            //                                "LibraryAPI");
            //    setupAction.RoutePrefix = "";
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}