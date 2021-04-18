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
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly IHddMetricsRepository _repository;

        private readonly IMapper _mapper;

        private readonly ILogger<HddMetricsController> _logger;
        
        public HddMetricsController(IHddMetricsRepository repository, IMapper mapper, ILogger<HddMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в HddMetricsController");

            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Сообщение из HddMetricsController из метода GetMetrics");
            _logger.LogInformation($"{fromTime}, {toTime}");

            var metrics = _repository.GetMetrics(fromTime, toTime);

            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HddMetricDto>(metric));
            }

            return Ok(metrics);
        }

        [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentile([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime,
            [FromRoute] Percentile percentile)
        {
            _logger.LogInformation("Сообщение из HddMetricsController из метода GetMetricsByPercentile");
            _logger.LogInformation($"{fromTime}, {toTime}, {percentile}");

            var metrics = _repository.GetMetrics(fromTime, toTime);

            //метод - заглушка
            return Ok();
        }

        [HttpGet("left")]
        public IActionResult GetMetricsLeft()
        {
            _logger.LogInformation("Сообщение из HddMetricsController из метода GetMetricsLeft");

            //метод - заглушка
            return Ok();
        }
    }
}
