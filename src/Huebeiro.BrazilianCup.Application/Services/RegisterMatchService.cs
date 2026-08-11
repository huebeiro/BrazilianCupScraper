using Huebeiro.BrazilianCup.Application.DTO;
using Huebeiro.BrazilianCup.Application.Interfaces;
using Huebeiro.BrazilianCup.Domain;
using Huebeiro.BrazilianCup.Domain.Exceptions;

namespace Huebeiro.BrazilianCup.Application.Services;

/// <summary>
/// Serviço para registro de novas partidas
/// </summary>
public class RegisterMatchService(
    ITeamRepository teamRepository,
    IMatchRepository matchRepository,
    IUnitOfWork unitOfWork)
{
    public async Task ExecuteAsync(RegisterMatchRequest request)
    {
        if (request.HomeTeamId == request.AwayTeamId)
            throw new InvalidMatchException(
                "A team cannot play against itself.");

        if (request.HomeTeamGoals < 0 || request.AwayTeamGoals < 0)
            throw new InvalidMatchException(
                "Goals cannot be negative.");

        var homeTeam = await teamRepository.GetByIdAsync(request.HomeTeamId);

        if (homeTeam is null)
            throw new InvalidMatchException(
                "Home team was not found.");

        var awayTeam = await teamRepository.GetByIdAsync(request.AwayTeamId);

        if (awayTeam is null)
            throw new InvalidMatchException(
                "Away team was not found.");

        if (homeTeam.Matches >= 38 || awayTeam.Matches >= 38)
            throw new InvalidMatchException(
                "A team cannot play more than 38 matches.");

        homeTeam.RegisterMatch(
            request.HomeTeamGoals,
            request.AwayTeamGoals);

        awayTeam.RegisterMatch(
            request.AwayTeamGoals,
            request.HomeTeamGoals);

        var match = new Match(
            homeTeam,
            awayTeam,
            request.HomeTeamGoals,
            request.AwayTeamGoals);

        teamRepository.Update(homeTeam);
        teamRepository.Update(awayTeam);

        matchRepository.Add(match);

        await unitOfWork.SaveChangesAsync();
    }
}