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

      
        [HttpPost("create")]
        public IActionResult Create([FromBody] DotNetMetricCreateRequest request)
        {
            _repository.Create(_mapper.Map<DotNetMetric>(request));

            _logger.LogInformation("Сообщение из DotNetMetricsController из метода Create");
            _logger.LogInformation($"{request.Time}, {request.Value}");

            return Ok();
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] DotNetMetricCreateRequest request)
        {
            _repository.Update(_mapper.Map<DotNetMetric>(request));

            _logger.LogInformation("Сообщение из DotNetMetricsController из метода Update");
            _logger.LogInformation($"{request.Time}, {request.Value}");

            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _repository.GetAll();

            var response = new AllDotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetric>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetric>(metric));
            }

            _logger.LogInformation("Сообщение из DotNetMetricsController из метода GetAll");

            return Ok(response);
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromBody] int id)
        {
            _repository.Delete(id);

            _logger.LogInformation("Сообщение из DotNetMetricsController из метода Delete");
            _logger.LogInformation($"{id}");

            return Ok();
        }

        [HttpGet("GetById")]
        public IActionResult GetById([FromBody] int id)
        {
            var metrics = _repository.GetById(id);

            _logger.LogInformation("Сообщение из DotNetMetricsController из метода GetById");
            _logger.LogInformation($"{id}");

            return Ok(metrics);
        }


        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Сообщение из DotNetMetricsController из метода GetMetrics");
            _logger.LogInformation($"{fromTime}, {toTime}");
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentile([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime,
            [FromRoute] Percentile percentile)
        {
            _logger.LogInformation("Сообщение из DotNetMetricsController из метода GetMetricsByPercentile");
            _logger.LogInformation($"{fromTime}, {toTime}, {percentile}");
            return Ok();
        }

        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromErrorsCount([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Сообщение из DotNetMetricsController из метода GetMetricsFromErrorsCount");
            _logger.LogInformation($"{fromTime}, {toTime}");
            return Ok();
        }
    }
}
