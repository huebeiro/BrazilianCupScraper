namespace Huebeiro.BrazilianCup.Domain.Exceptions;

public class InvalidTeamStateException(string? message)
    : Exception(message);