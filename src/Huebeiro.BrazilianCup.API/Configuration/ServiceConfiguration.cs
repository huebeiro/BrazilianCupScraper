using Huebeiro.BrazilianCup.Application.Interfaces;
using Huebeiro.BrazilianCup.Application.Services;
using Huebeiro.BrazilianCup.Infrastructure.Persistence;
using Huebeiro.BrazilianCup.Infrastructure.Persistence.Repositories;
using Huebeiro.BrazilianCup.Scraper;
using Huebeiro.BrazilianCup.Scraper.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Huebeiro.BrazilianCup.API.Configuration;

public static class ServiceConfiguration
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<BrazilianCupDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")));

        // Scraper Routine
        services.AddScoped<IStandingScraper, SeleniumStandingScraper>();

        // Repositories
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<IMatchRepository, MatchRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Services
        services.AddScoped<InitializeTeamsService>();
        services.AddScoped<GetStandingsService>();
        services.AddScoped<RegisterMatchService>();
        services.AddScoped<ApplicationInitializer>();

        return services;
    }
}