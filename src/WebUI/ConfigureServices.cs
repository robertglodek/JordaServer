using Hangfire;
using Hangfire.SqlServer;
using Jorda.Application.Common.Interfaces;
using Jorda.Server.Application.Common.Configuration;
using Jorda.Server.WebUi.Middleware;
using Jorda.WebUi.Filters;
using Jorda.WebUi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using System.Text;

namespace Jorda.WebUi
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebUIServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.Configure<EmailSettings>(configuration.GetSection("SendGridSettings"));
            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddHttpContextAccessor();
            services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)),
                };
            });
            services.AddScoped<RequestCultureMiddleware>();
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
            services.AddSwaggerDoc();
            services.AddCors(options =>
            {
                var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<IEnumerable<string>>()!.ToArray();
                options.AddPolicy("FrontEndClients", builder =>
                             builder.AllowAnyMethod().AllowAnyHeader().WithOrigins(allowedOrigins));
            });


            //Configure HangFire
            services.AddHangfire(c => c
           .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
           .UseSimpleAssemblyNameTypeSerializer()
           .UseRecommendedSerializerSettings()
           .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
           {
               CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
               SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
               QueuePollInterval = TimeSpan.Zero,
               UseRecommendedIsolationLevel = true,
               DisableGlobalLocks = true
           }));
            services.AddHangfireServer();

            return services;
        }

        private static void AddSwaggerDoc(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                    Enter your token in the text input below.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                   {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                         
                      },
                      new List<string>()
                   }
                });
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1", 
                    Title = "\"Jorda\".Api",
                    Contact = new OpenApiContact
                    {
                        Name = "Robert Głodek",
                        Email = "robert.glodek98@gmail.com"
                    }
                });
            });
        }
    }
}
