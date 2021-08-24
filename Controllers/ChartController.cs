using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ScriptLauncher.DataStorage;
using ScriptLauncher.HubConfig;
using ScriptLauncher.TimerFeatures;

namespace ScriptLauncher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly IHubContext<ChartHub> _hub;

        public ChartController(IHubContext<ChartHub> hub)
        {
            _hub = hub;
        }

        public IActionResult Get()
        {
            // ReSharper disable once StringLiteralTypo
            _ = new TimerManager(() => _hub.Clients.All.SendAsync("transferchartdata", DataManager.GetData()));
            return Ok(new { Message = "Request Completed" });
        }
    }
}