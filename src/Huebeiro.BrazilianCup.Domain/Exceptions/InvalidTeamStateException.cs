namespace Huebeiro.BrazilianCup.Domain.Exceptions;

/// <summary>
/// Exceção que representa um estado de time que fere as regras de negócio da aplicação
/// </summary>
/// <param name="message">Mensagem detalhando a exceção</param>
public class InvalidTeamStateException(string? message)
    : Exception(message);