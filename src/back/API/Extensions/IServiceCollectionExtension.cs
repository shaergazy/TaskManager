using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using API.Infrastructure;
using API.Services;
using BLL.Services;
using BLL.Services.External;
using Common.Helpers;
using DAL.EF;
using DAL.Entities.Users;
using DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.Extensions;

internal static class IServiceCollectionExtension
{
    internal static void RegisterCors(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection(nameof(SettingsDto.Cors)).Get<SettingsDto.Cors>();
        services.AddCors(x => x.AddDefaultPolicy(b => b
            .WithOrigins(settings.Origins.ToArray())
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()));
    }

    internal static void RegisterIOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthDto.Jwt>(x => configuration.GetSection(nameof(AuthDto.Jwt)).Bind(x));
        services.Configure<SettingsDto.Cors>(x => configuration.GetSection(nameof(SettingsDto.Cors)).Bind(x));
        services.Configure<SettingsDto.ServiceUri>(x => configuration.GetSection(nameof(SettingsDto.ServiceUri)).Bind(x));
    }

    internal static void RegisterConnectionString(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(x => x.UseSqlServer(configuration.GetConnectionString("Default")));
    }

    internal static void RegisterAuth(this IServiceCollection services)
    {
        services.AddIdentityCore<User>(x =>
            {
                x.Password.RequiredLength = 6;
                x.Password.RequireLowercase = false;
                x.Password.RequireUppercase = false;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequireDigit = false;
            })
            .AddRoles<Role>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
    }

    internal static void RegisterJwtAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = JwtBuilder.Parameters(configuration);
            });
    }

    internal static void RegisterServiceUri(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection(nameof(SettingsDto.ServiceUri)).Get<SettingsDto.ServiceUri>();
        AppConstants.BaseUri = settings.Self.BaseUri;
    }

    internal static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<AppDbContext>();

        services.AddTransient<AuthService>();
        services.AddTransient<ProjectService>();
        services.AddTransient<EmployeeService>();
        services.AddTransient<JobService>();
        services.AddTransient<DocumentService>();
    }

    internal static void RegisterSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(x =>
        {
            x.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "SchoolGO API",
                Version = "v1",
            });
            x.DescribeAllParametersInCamelCase();

            x.AddSecurityDefinition(JwtBuilder.Bearer(), new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter JWT with Bearer into field",
                Name = JwtBuilder.Authorization(),
                Type = SecuritySchemeType.ApiKey,
                Scheme = JwtBuilder.Bearer(),
            });

            x.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBuilder.Bearer(),
                        },
                        Scheme = "oauth2",
                        Name = JwtBuilder.Bearer(),
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                },
            });
            x.SchemaFilter<DtoExampleFilter>();

            x.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{nameof(API)}.xml"));
            x.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{nameof(DTO)}.xml"));

            x.CustomSchemaIds(t => t.FullName?.Replace("+", "."));
        });
    }
}
