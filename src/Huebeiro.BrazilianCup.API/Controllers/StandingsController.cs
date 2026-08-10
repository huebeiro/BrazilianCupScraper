using Huebeiro.BrazilianCup.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Huebeiro.BrazilianCup.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StandingsController(
    GetStandingsService getStandingsService) : ControllerBase
{
    private const int LibertadoresTeamCount = 6;
    private const int SudamericanaTeamCount = 5; // Rever regras do desafio para incluir 12º lugar
    private const int RelegationTeamCount   = 4;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var standings = await getStandingsService.ExecuteAsync();

        return Ok(standings);
    }

    [HttpGet("libertadores")]
    public async Task<IActionResult> GetLibertadores()
    {
        var standings = await getStandingsService.ExecuteAsync();

        return Ok(standings.Take(LibertadoresTeamCount));
    }

    [HttpGet("sudamericana")]
    public async Task<IActionResult> GetSudamericana()
    {
        var standings = await getStandingsService.ExecuteAsync();

        return Ok(standings.Skip(LibertadoresTeamCount).Take(SudamericanaTeamCount));
    }

    [HttpGet("relegation")]
    public async Task<IActionResult> GetRelegation()
    {
        var standings = await getStandingsService.ExecuteAsync();

        return Ok(standings.TakeLast(RelegationTeamCount));
    }
}