using Huebeiro.BrazilianCup.Domain;

namespace Huebeiro.BrazilianCup.Application.Interfaces;

public interface IMatchRepository
{
    void Add(Match match);
}