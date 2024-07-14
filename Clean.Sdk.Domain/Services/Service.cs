﻿using Microsoft.Extensions.Logging;

namespace Clean.Sdk.Domain.Services
{
	public abstract class Service
	{
		protected readonly ILogger<Service> Logger;

		protected Service(ILogger<Service> logger)
		{
			Logger = logger;
		}
	}
}
