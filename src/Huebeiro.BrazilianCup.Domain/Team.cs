using Huebeiro.BrazilianCup.Domain.Exceptions;

namespace Huebeiro.BrazilianCup.Domain;

public class Team
{
    public short Id { get; }
    public string Name { get; } = null!;
    public short Points => (short)(Wins * WinPoints + Draws * DrawPoints);
    public short Matches => (short)(Wins + Draws + Losses);
    public short Wins { get; private set; }
    public short Draws { get; private set; }
    public short Losses { get; private set; }
    public short GoalsFor { get; private set; }
    public short GoalsAgainst { get; private set; }
    public short GoalDifference => (short)(GoalsFor - GoalsAgainst);

    /// <summary>
    /// A quantidade de pontos que uma vitória garante
    /// </summary>
    private const short WinPoints = 3;
    /// <summary>
    /// A quantidade de pontos que um empate garante
    /// </summary>
    private const short DrawPoints = 1;

    /// <summary>
    /// Construtor primário de Team para o retorno de Repository
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="wins"></param>
    /// <param name="draws"></param>
    /// <param name="losses"></param>
    /// <param name="goalsFor"></param>
    /// <param name="goalsAgainst"></param>
    /// <exception cref="InvalidTeamStateException"></exception>
    public Team(
        short id,
        string name,
        short wins,
        short draws,
        short losses,
        short goalsFor,
        short goalsAgainst)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidTeamStateException("Team name cannot be empty.");
        if (wins < 0 || draws < 0 || losses < 0)
            throw new InvalidTeamStateException("Match registry cannot be negative.");
        if (goalsFor < 0 || goalsAgainst < 0)
            throw new InvalidTeamStateException("Goals registry cannot be negative.");

        Id = id;
        Name = name;
        Wins = wins;
        Draws = draws;
        Losses = losses;
        GoalsFor = goalsFor;
        GoalsAgainst = goalsAgainst;
    }

    /// <summary>
    /// Construtor alternativo de Team para a utilização do Scraper
    /// </summary>
    /// <param name="name"></param>
    /// <param name="wins"></param>
    /// <param name="draws"></param>
    /// <param name="losses"></param>
    /// <param name="goalsFor"></param>
    /// <param name="goalsAgainst"></param>
    public Team(
        string name,
        short wins,
        short draws,
        short losses,
        short goalsFor,
        short goalsAgainst)
    : this(
        0, // Iniciando o Id como 0 para cadastro de Team
        name,
        wins,
        draws,
        losses,
        goalsFor,
        goalsAgainst)
    { }

    public void RegisterMatch(short goalsFor, short goalsAgainst)
    {
        if (goalsFor < 0 || goalsAgainst < 0)
            throw new InvalidMatchException("Goals cannot be negative.");

        if (goalsFor > goalsAgainst)
        {
            Wins++;
        }
        else if (goalsFor == goalsAgainst)
        {
            Draws++;
        }
        else
        {
            Losses++;
        }

        GoalsFor += goalsFor;
        GoalsAgainst += goalsAgainst;
    }
}
