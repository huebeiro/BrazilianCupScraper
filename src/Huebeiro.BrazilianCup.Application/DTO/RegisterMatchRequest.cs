namespace Huebeiro.BrazilianCup.Application.DTO;

public record RegisterMatchRequest(
    short HomeTeamId,
    short AwayTeamId,
    short HomeTeamGoals,
    short AwayTeamGoals);