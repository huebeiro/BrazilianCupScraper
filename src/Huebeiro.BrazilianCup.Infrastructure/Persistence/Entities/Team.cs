namespace Huebeiro.BrazilianCup.Infrastructure.Persistence.Entities;

public partial class Team
{
    public short Id { get; set; }
    public string Name { get; set; } = null!;
    public short Wins { get; set; }
    public short Draws { get; set; }
    public short Losses { get; set; }
    public short GoalsFor { get; set; }
    public short GoalsAgainst { get; set; }

    public virtual ICollection<Match> AwayMatches { get; set; } = new List<Match>();
    public virtual ICollection<Match> HomeMatches { get; set; } = new List<Match>();
}
