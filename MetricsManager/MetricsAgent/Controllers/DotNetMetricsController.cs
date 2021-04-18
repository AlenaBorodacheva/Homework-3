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
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly IDotNetMetricsRepository _repository;

        private readonly IMapper _mapper;

        private readonly ILogger<DotNetMetricsController> _logger;

        public DotNetMetricsController(IDotNetMetricsRepository repository, IMapper mapper, ILogger<DotNetMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в DotNetMetricsController");

            _repository = repository;
            _mapper = mapper;
        }

      
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Сообщение из DotNetMetricsController из метода GetMetrics");
            _logger.LogInformation($"{fromTime}, {toTime}");

            var metrics = _repository.GetMetrics(fromTime, toTime);

            var response = new AllDotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetricDto>(metric));
            }

            return Ok(metrics);
        }

        [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentile([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime,
            [FromRoute] Percentile percentile)
        {
            _logger.LogInformation("Сообщение из DotNetMetricsController из метода GetMetricsByPercentile");
            _logger.LogInformation($"{fromTime}, {toTime}, {percentile}");

            var metrics = _repository.GetMetrics(fromTime, toTime);

            //метод - заглушка
            return Ok();
        }

        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromErrorsCount([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation("Сообщение из DotNetMetricsController из метода GetMetricsFromErrorsCount");
            _logger.LogInformation($"{fromTime}, {toTime}");

            var metrics = _repository.GetMetrics(fromTime, toTime);

            //метод - заглушка
            return Ok();
        }
    }
}
