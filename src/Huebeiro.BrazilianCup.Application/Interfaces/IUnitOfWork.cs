namespace Huebeiro.BrazilianCup.Application.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}