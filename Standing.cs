namespace TestScraper;
public class Standing
{
    public Standing(string[] cellsText)
    {
        if (cellsText.Length != TotalCells)
            throw new WrongStandingFormatException("Wrong table row format.");

        Name = cellsText[NameIndex];
        Wins = TryParseCell(cellsText[WinsIndex]);
        Draws = TryParseCell(cellsText[DrawsIndex]);
        Losses = TryParseCell(cellsText[LossesIndex]);
        GoalsFor = TryParseCell(cellsText[GoalsForIndex]);
        GoalsAgainst = TryParseCell(cellsText[GoalsAgainstIndex]);
    }

    public string Name { get; set; }
    public int Points => Wins * WinPoints + Draws * DrawPoints; 
    public int Matches => Wins + Draws + Losses;
    public int Wins { get; set; }
    public int Draws { get; set; }
    public int Losses { get; set; }
    public int GoalsFor { get; set; }
    public int GoalsAgainst { get; set; }
    public int GoalDifference => GoalsFor - GoalsAgainst;

    /// <summary>
    /// The ammount of points a win provides
    /// </summary>
    private const int WinPoints = 3;

    /// <summary>
    /// The ammount of points a draw provides
    /// </summary>
    private const int DrawPoints = 1;

    // TODO move to mapper in final version
    private const int TotalCells = 12;
    private const int NameIndex = 1;
    private const int WinsIndex = 4;
    private const int DrawsIndex = 5;
    private const int LossesIndex = 6;
    private const int GoalsForIndex = 7;
    private const int GoalsAgainstIndex = 8;

    // TODO move to mapper in final version
    private int TryParseCell(string text) =>
            int.TryParse(text, out int parsed) 
            ? parsed 
            : throw new WrongStandingFormatException($"Cell '{text}' is not a valid int");


    // TODO move to mapper related class in final version
    public class WrongStandingFormatException(string? message) : Exception(message);

    public override string ToString()
    {
        return $"'{Name}' | P: {Points} | Pl: {Matches} | W: {Wins} | D: {Draws} " +
            $"| L: {Losses} | GF: {GoalsFor} | GA: {GoalsAgainst} | GD: {GoalDifference}";
    }
}