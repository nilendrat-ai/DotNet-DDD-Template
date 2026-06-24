using DotNet.DDD.Template.Application.Common.Behaviours;
using DotNet.DDD.Template.Domain.Common;
using DotNet.DDD.Template.Infrastructure.Persistence;
using DotNet.DDD.Template.Infrastructure.Persistence.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNet.DDD.Template.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // EF Core
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        // Unit of Work — AppDbContext implements IUnitOfWork
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<AppDbContext>());

        // Concrete repositories — one per aggregate, injected directly into handlers
        // Add new aggregate repositories here as the domain grows
        services.AddScoped<OrderRepository>();

        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(Application.Orders.Commands.CreateOrder.CreateOrderCommand).Assembly;

        // MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

        // Pipeline behaviours
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        // FluentValidation
        services.AddValidatorsFromAssembly(applicationAssembly);

        // AutoMapper
        services.AddAutoMapper(applicationAssembly);

        return services;
    }
}
