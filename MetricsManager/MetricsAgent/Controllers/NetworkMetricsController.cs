using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsCommon;
using Microsoft.Extensions.Logging;
using System.Data.SQLite;
using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly INetworkMetricsRepository _repository;

        private readonly IMapper _mapper;

        private readonly ILogger<NetworkMetricsController> _logger;

        public NetworkMetricsController(INetworkMetricsRepository repository, IMapper mapper, ILogger<NetworkMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в NetworkMetricsController");

            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentile([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime,
            [FromRoute] Percentile percentile)
        {
            _logger.LogInformation("Сообщение из NetworkMetricsController из метода GetMetricsByPercentile");
            _logger.LogInformation($"{fromTime}, {toTime}, {percentile}");

            var metrics = _repository.GetMetrics(fromTime, toTime);

            //метод - заглушка
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Сообщение из NetworkMetricsController из метода GetMetrics");
            _logger.LogInformation($"{fromTime}, {toTime}");

            var metrics = _repository.GetMetrics(fromTime, toTime);

            var response = new AllNetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetricDto>(metric));
            }

            return Ok(metrics);
        }
    }
}
