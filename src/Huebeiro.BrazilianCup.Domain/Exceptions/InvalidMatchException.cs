namespace Huebeiro.BrazilianCup.Domain.Exceptions;

/// <summary>
/// Exceção que representa a tentativa de registro de uma partida inválida
/// </summary>
/// <param name="message">Mensagem detalhando a exceção</param>
public class InvalidMatchException(string? message)
    : Exception(message);