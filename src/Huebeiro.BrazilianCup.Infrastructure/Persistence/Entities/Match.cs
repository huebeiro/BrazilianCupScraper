namespace Huebeiro.BrazilianCup.Infrastructure.Persistence.Entities;

public partial class Match
{
    public short Id { get; set; }
    public short HomeTeamId { get; set; }
    public short AwayTeamId { get; set; }
    public short HomeGoals { get; set; }
    public short AwayGoals { get; set; }
    public DateTime MatchDate { get; set; }

    public virtual Team AwayTeam { get; set; } = null!;
    public virtual Team HomeTeam { get; set; } = null!;
}
