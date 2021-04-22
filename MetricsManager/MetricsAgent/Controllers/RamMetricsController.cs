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
using MetricsAgent.Requests;
using MetricsAgent.Models;
using MetricsAgent.Responses;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly IRamMetricsRepository _repository;

        private readonly IMapper _mapper;

        private readonly ILogger<RamMetricsController> _logger;

        public RamMetricsController(IRamMetricsRepository repository, IMapper mapper, ILogger<RamMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в RamMetricsController");

            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Сообщение из RamMetricsController из метода GetMetrics");
            _logger.LogInformation($"{fromTime}, {toTime}");

            var metrics = _repository.GetMetrics(fromTime, toTime);

            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RamMetricDto>(metric));
            }

            return Ok(metrics);
        }

        [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentile([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime,
            [FromRoute] Percentile percentile)
        {
            _logger.LogInformation("Сообщение из RamMetricsController из метода GetMetricsByPercentile");
            _logger.LogInformation($"{fromTime}, {toTime}, {percentile}");

            var metrics = _repository.GetMetrics(fromTime, toTime);

            //метод - заглушка
            return Ok();
        }

        [HttpGet("available")]
        public IActionResult GetMetricsAvailable()
        {
            _logger.LogInformation("Сообщение из RamMetricsController из метода GetMetricsAvailable");

            //метод - заглушка
            return Ok();
        }
    }
}
