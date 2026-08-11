namespace Huebeiro.BrazilianCup.Scraper.Exceptions;

/// <summary>
/// Exceção que representa um erro durante a coleta de dados pelo Scraper
/// </summary>
/// <param name="message">Mensagem detalhando a exceção</param>
public class WrongStandingFormatException(string? message)
    : Exception(message);