using Core.Infrastructure.Persistence.Users.Context;
using Core.Infrastructure.Persistence.Users.Repository;
using Core.Infrastructure.Persistence.Users.Validations;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using Core.Infrastructure.Securities.Models;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Core.Application.Behaviors.Validations;
using Core.Infrastructure.Persistence.Users.Repository.Interfaces;
using Core.Infrastructure.Persistence.Users.Features.Manager;
using Core.Infrastructure.Securities;

namespace Core.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Sql"));
            });
            services = AddPackageAssemblies(services, Assembly.GetExecutingAssembly());
            services = AddValidators(services);
            services.Configure<TokenOptions>(configuration.GetSection(nameof(TokenOptions)));
            services = AddAuthentications(services, configuration);

            services.AddScoped<IUserUnitOfWork, UserUnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<ITokenHelper, JwtTokenHelper>();

            return services;
        }

        private static IServiceCollection AddPackageAssemblies(IServiceCollection services, Assembly assembly)
        {
            services.AddAutoMapper(assembly);
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(assembly);
            });

            return services;
        }

        private static IServiceCollection AddValidators(IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<UserLoginValidator>();

            return services;
        }

        private static IServiceCollection AddAuthentications(IServiceCollection services, IConfiguration configuration)
        {
            var tokenOptions = configuration.GetSection(nameof(TokenOptions)).Get<TokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecretKey))
                    };
                });

            services.AddAuthorization();

            return services;
        }


    }
}
