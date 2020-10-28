﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Funda.Core.Application;
using Funda.Core.Domain;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Funda.Workers
{
    public class TopTenAgentsWorker : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IRealEstateService _realEstateService;

        public TopTenAgentsWorker(ILogger<TopTenAgentsWorker> logger, IRealEstateService realEstateService)
        {
            _logger = logger;
            _realEstateService = realEstateService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("starting worker......");
            var objectsInAmsterdam = await _realEstateService.GetAllRealEstateObjects(new SearchQuery()
            {
                Type = "koop",
                Location = "Amsterdam",
                Page = 0,
                PageSize = 25
            });

            Console.Clear();
            Console.WriteLine($"TOTAL NUMBER OF OBJECTS IN AMSTERDAM : {objectsInAmsterdam.Count}");
            Console.WriteLine("TOP 10 REAL ESTATE AGENTS IN AMSTERDAM");
            _realEstateService.GetAgentsByObjectCount(objectsInAmsterdam).Take(10).ToList().ForEach(i => Console.WriteLine($"makelaar {i.Name} has {i.ObjectCount} objects listed"));
        }
    }
}
