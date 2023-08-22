using Microsoft.AspNetCore.Mvc;
//using StoneMessages;

namespace StoneManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StoneController : ControllerBase
{
    private readonly ILogger<StoneController> _logger;

    public StoneController(ILogger<StoneController> logger)
    {
        _logger = logger;
    }

    //[HttpGet]
    //public async Task<List<Stone>> Get([FromQuery] int? count, CancellationToken ct)
    //{
    //    if (count >= 10)
    //    {
    //        throw new InvalidOperationException();
    //    }

    //    if (count >= 5)
    //    {
    //        await Task.Delay(
    //            TimeSpan.FromSeconds(count.GetValueOrDefault()),
    //            ct);
    //    }

    //    List<Stone> stones = Enumerable.Range(1, count ?? 3)
    //        .Select(_ => new Stone
    //        {
    //            Id = Guid.NewGuid(),
    //            Weight = Random.Shared.Next(0, 100)
    //        })
    //        .ToList();

    //    return stones;
    //}
}