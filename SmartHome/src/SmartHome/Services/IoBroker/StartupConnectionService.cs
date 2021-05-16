using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ioBroker.net;
using Microsoft.Extensions.Hosting;

namespace SmartHome.Services.IoBroker
{
    public class StartupConnectionService : IHostedService
    {
        private readonly IIoBrokerDotNet _ioBrokerConnection;
        public StartupConnectionService(IIoBrokerDotNet ioBrokerConnection)
        {
            _ioBrokerConnection = ioBrokerConnection;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _ioBrokerConnection.ConnectAsync(TimeSpan.FromSeconds(5));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // do nothing

            return Task.CompletedTask;
        }
    }
}
