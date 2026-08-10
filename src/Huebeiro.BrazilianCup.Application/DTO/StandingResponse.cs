namespace Huebeiro.BrazilianCup.Application.DTO;

public record StandingResponse(
    int Position,
    short TeamId, // Incluindo Id para referenciação do POST de RegisterMatch
    string TeamName,
    short Points,
    short Matches,
    short Wins,
    short Draws,
    short Losses,
    short GoalsFor,
    short GoalsAgainst,
    short GoalDifference);