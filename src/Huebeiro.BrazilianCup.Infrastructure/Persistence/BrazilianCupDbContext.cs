using Huebeiro.BrazilianCup.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Huebeiro.BrazilianCup.Infrastructure.Persistence;

public partial class BrazilianCupDbContext : DbContext
{
    public BrazilianCupDbContext(DbContextOptions<BrazilianCupDbContext> options)
        : base(options)
    { }

    public virtual DbSet<Match> Matches { get; set; }
    public virtual DbSet<Team> Teams { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_matches");

            entity.ToTable("matches");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AwayGoals).HasColumnName("away_goals");
            entity.Property(e => e.AwayTeamId).HasColumnName("away_team");
            entity.Property(e => e.HomeGoals).HasColumnName("home_goals");
            entity.Property(e => e.HomeTeamId).HasColumnName("home_team");
            entity.Property(e => e.MatchDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("match_date");

            entity.HasOne(d => d.AwayTeam).WithMany(p => p.AwayMatches)
                .HasForeignKey(d => d.AwayTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_matches_away");

            entity.HasOne(d => d.HomeTeam).WithMany(p => p.HomeMatches)
                .HasForeignKey(d => d.HomeTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_matches_home");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_teams");

            entity.ToTable("teams");

            entity.HasIndex(e => e.Name, "teams_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Draws).HasColumnName("draws");
            entity.Property(e => e.GoalsAgainst).HasColumnName("goals_against");
            entity.Property(e => e.GoalsFor).HasColumnName("goals_for");
            entity.Property(e => e.Losses).HasColumnName("losses");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Wins).HasColumnName("wins");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}