using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ScriptLauncher.Models;

namespace ScriptLauncher.HubConfig
{
    public class ChartHub : Hub
    {
        // ReSharper disable once StringLiteralTypo
        public async Task BroadcastChartData(List<ChartModel> data) =>
            await Clients.All.SendAsync("broadcastchartdata", data);
    }
}