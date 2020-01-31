using System;
using System.Collections.Generic;
using AutoMapper;
using LibraryAPI.Authentication;
using LibraryDataContext;
using LibraryRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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
            //For Authentication
            services.AddMvc(setupAction =>
            {
                setupAction.EnableEndpointRouting = false;
                setupAction.Filters.Add(new AuthorizeFilter());
            });

            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ILibraryManager, LibraryManager>();
            services.AddDbContext<LibraryContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnectionString"));
                });
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                                       "LibraryOpenAPISpecification",
                                        new Microsoft.OpenApi.Models.OpenApiInfo
                                        {
                                            Title = "LibraryManagement",
                                            Version = "1.0.0"
                                        });

                setupAction.IncludeXmlComments(Configuration.GetConnectionString("ControllerCommentsXML"));
                setupAction.IncludeXmlComments(Configuration.GetConnectionString("ModelCommentsXML"));

                setupAction.AddSecurityDefinition("basicAuth", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    Description = "Input your username and password to access this API"
                });

                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "basicAuth" }
                        }, new List<string>() }
                });
            });

            services.AddAuthentication("Basic")
                    .AddScheme<AuthenticationSchemeOptions, AuthenticationHandler>("Basic", null);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/LibraryOpenAPISpecification/swagger.json",
                                            "LibraryAPI");
                setupAction.RoutePrefix = "";
            });
            app.UseAuthentication();
            app.UseMvc();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}