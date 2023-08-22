using Microsoft.AspNetCore.Mvc;
using StoneApi.Clients;
using StoneMessages;

namespace StoneApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StoneController : ControllerBase
{
    private readonly IStonehengeClient _stonehengeClient;
    private readonly IStoneManagerClient _stoneManagerClient;
    private readonly ILogger<StoneController> _logger;

    public StoneController(IStonehengeClient stonehengeClient, IStoneManagerClient stoneManagerClient, ILogger<StoneController> logger)
    {
        _stonehengeClient = stonehengeClient;
        _stoneManagerClient = stoneManagerClient;
        _logger = logger;
    }

    [HttpGet]
    public Task<List<Stone>> Get([FromQuery] int? count, CancellationToken ct) =>
        _stoneManagerClient.GetStonesAsync(count, ct);

    [HttpGet("Legacy")]
    public Task<List<Stone>> GetLegacy([FromQuery] int? count, CancellationToken ct) =>
        _stonehengeClient.GetStones(count, ct);
}