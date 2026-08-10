using Huebeiro.BrazilianCup.Application.Interfaces;

namespace Huebeiro.BrazilianCup.Infrastructure.Persistence.Repositories;

public class UnitOfWork(BrazilianCupDbContext context) : IUnitOfWork
{
    public Task SaveChangesAsync()
        => context.SaveChangesAsync();
}