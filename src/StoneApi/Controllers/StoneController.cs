using Microsoft.AspNetCore.Mvc;
using StoneMessages;

namespace StoneApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StoneController : ControllerBase
{
    private readonly ILogger<StoneController> _logger;

    public StoneController(ILogger<StoneController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public Task<List<Stone>> Get([FromQuery] int? count)
    {
        List<Stone> stones = Enumerable.Range(1, count ?? 5)
            .Select(index => new Stone
            {
                Id = Guid.NewGuid(),
                Weight = Random.Shared.Next(0, 100)
            })
            .ToList();

        return Task.FromResult(stones);
    }
    
    [HttpGet("Legacy")]
    public Task<List<Stone>> GetLegacy([FromQuery] int? count)
    {
        List<Stone> stones = Enumerable.Range(1, count ?? 5)
            .Select(index => new Stone
            {
                Id = Guid.NewGuid(),
                Weight = Random.Shared.Next(0, 100)
            })
            .ToList();

        return Task.FromResult(stones);
    }
}