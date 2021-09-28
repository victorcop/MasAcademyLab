using MasAcademyLab.Service.Extention;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MasAcademyLab.Web
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
            services.AddServiceDependencies(Configuration);
            services.AddVersionedApiExplorer(setupAction => 
            {
                setupAction.GroupNameFormat = "'v'VV";
            });
            services.AddApiVersioning(opt =>
            {
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ReportApiVersions = true;
                opt.ApiVersionReader = new UrlSegmentApiVersionReader();
                opt.ApiVersionReader = new HeaderApiVersionReader("X-Version");
            });

            services.AddControllers()
                    .AddNewtonsoftJson(setupAction =>
                    {
                        setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    });

            var apiVersionDescriptionProvider =
                services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();

            services.AddSwaggerGen(c =>
            {
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    c.SwaggerDoc($"MasAcademyLabAPISpecification{description.GroupName}",
                             new OpenApiInfo
                             {
                                 Title = "MasAcademyLab API",
                                 Version = description.ApiVersion.ToString(),
                                 Description = "Through this API you can access Trainings and theirs talks",
                                 Contact = new OpenApiContact
                                 {
                                     Email = "victorcop90@gmail.com",
                                     Name = "Victor Velasquez",
                                     Url = new Uri("https://twitter.com/victorcop55")
                                 },
                                 License = new OpenApiLicense
                                 {
                                     Name = "MIT License",
                                     Url = new Uri("https://opensource.org/licenses/MIT")
                                 }
                             });
                }

                c.AddSecurityDefinition("basicAuth", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    Description = "Input your username and password to access this API"
                });

                c.DocInclusionPredicate((documentName, apiDescription) =>
                {
                    var actionApiVersionModel = apiDescription.ActionDescriptor
                    .GetApiVersionModel(ApiVersionMapping.Explicit | ApiVersionMapping.Implicit);

                    if (actionApiVersionModel == null)
                    {
                        return true;
                    }

                    if (actionApiVersionModel.DeclaredApiVersions.Any())
                    {
                        return actionApiVersionModel.DeclaredApiVersions.Any(v =>
                        $"MasAcademyLabAPISpecificationv{v}" == documentName);
                    }
                    return actionApiVersionModel.ImplementedApiVersions.Any(v =>
                        $"MasAcademyLabAPISpecificationv{v}" == documentName);
                });

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                c.IncludeXmlComments(xmlCommentsFullPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
              IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(setupAction =>
                {
                    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                    {
                        setupAction.SwaggerEndpoint($"/swagger/" +
                            $"MasAcademyLabAPISpecification{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }

                    setupAction.RoutePrefix = "";
                });
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    });
                });
            }

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
