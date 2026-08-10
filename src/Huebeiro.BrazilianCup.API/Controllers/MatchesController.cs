using Huebeiro.BrazilianCup.Application.DTO;
using Huebeiro.BrazilianCup.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Huebeiro.BrazilianCup.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatchesController(RegisterMatchService registerMatchService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(
        [FromBody] RegisterMatchRequest request)
    {
        await registerMatchService.ExecuteAsync(request);

        return Created(); // Resposta 201 indicando partida registrada
    }
}