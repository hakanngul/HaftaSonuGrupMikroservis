﻿namespace MicroserviceOne
{
    public class QueueDataSaveToDatabase : BackgroundService
    {
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //while (!stoppingToken.IsCancellationRequested)
            //{


            //    await Task.Delay(1000*60*60*24, stoppingToken);
            //}
        }
    }
}