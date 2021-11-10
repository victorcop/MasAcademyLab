﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MasAcademyLab.Web.Extention
{
    public static class OpenApiExterders
    {
        public static void AddOpenApi(this IServiceCollection services)
        {
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

        public static IApplicationBuilder UseOpenApi(this IApplicationBuilder app, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            app.UseSwagger();
            return app.UseSwaggerUI(setupAction =>
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
    }
}
