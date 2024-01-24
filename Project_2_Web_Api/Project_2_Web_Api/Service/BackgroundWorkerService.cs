using Project_2_Web_Api.Helpers;
using Project_2_Web_API.Models;
using System.Threading;

namespace Project_2_Web_Api.Service;

public class BackgroundWorkerService : BackgroundService
{
	private readonly ILogger<BackgroundWorkerService> logger;
	private IServiceProvider service;
	public BackgroundWorkerService(ILogger<BackgroundWorkerService> logger, IServiceProvider service)
	{
		this.logger = logger;
		this.service = service;
		
	}

	public async Task StartAsync(CancellationToken cancellationToken)
	{
		logger.LogInformation("Service Start");

	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		logger.LogInformation("stop Start");
		return Task.CompletedTask;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			var startTime = DateTimeOffset.Now.ToUniversalTime().AddDays(1).AddHours(0).AddMinutes(0).AddSeconds(0);
			logger.LogInformation("Word Runing at: {time}", DateTimeOffset.Now);
			using (var scope = service.CreateScope())
			{
				var notificationService = scope.ServiceProvider.GetRequiredService<NotificationService>();
				notificationService.SendAutoNotificationVisit();
			}

			await Task.Delay(24*60*60*1000, stoppingToken);

		}
	}
}
