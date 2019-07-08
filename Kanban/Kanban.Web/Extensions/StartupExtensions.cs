using Kanban.Api.Infrastructure;
using Kanban.BusinessLogic.Authentication;
using Kanban.BusinessLogic.Implementation.Services;
using Kanban.BusinessLogic.Interfaces.Sarvices;
using Kanban.DataAccess.DatabaseContext;
using Kanban.DataAccess.Implementation.Repositories;
using Kanban.DataAccess.Interfaces.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Text;

namespace Kanban.Api.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(Constant.CONFIG_SECTION_KANBAN_DB_CONNECTION_STRING);
            var token = configuration.GetSection(Constant.CONFIG_SECTION_KANBAN_TOKEN_MANAGEMENT).Get<TokenManagement>();

            services.AddSingleton(token);

            services.AddDbContext<KanbanDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<DbContext, KanbanDbContext>();

            services.AddScoped<ITaskTypeRepository, TaskTypeRepository>();
            services.AddScoped<ITaskStatusRepository, TaskStatusRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<ITaskStatusService, TaskStatusService>();
            services.AddScoped<ITaskTypeService, TaskTypeService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var serviceProvider = services.BuildServiceProvider();
                    var token = serviceProvider.GetService<TokenManagement>();
                    var secret = Encoding.ASCII.GetBytes(token.Secret);

                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secret),
                        ValidateIssuer = true,
                        ValidIssuer = token.Issuer,
                        ValidateAudience = true,
                        ValidAudience = token.Audience,
                    };
                });

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "Dialog management API", Version = "v1" });
                options.AddSecurityDefinition("oauth2", new ApiKeyScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = "header",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            return services;
        }


    }
}
