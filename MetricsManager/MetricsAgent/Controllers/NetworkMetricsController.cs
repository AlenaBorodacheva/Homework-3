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


        [HttpPost("create")]
        public IActionResult Create([FromBody] NetworkMetricCreateRequest request)
        {
            _repository.Create(_mapper.Map<NetworkMetric>(request));

            _logger.LogInformation("Сообщение из NetworkMetricsController из метода Create");
            _logger.LogInformation($"{request.Time}, {request.Value}");

            return Ok();
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] NetworkMetricCreateRequest request)
        {
            _repository.Update(_mapper.Map<NetworkMetric>(request));

            _logger.LogInformation("Сообщение из NetworkMetricsController из метода Update");
            _logger.LogInformation($"{request.Time}, {request.Value}");

            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _repository.GetAll();
            
            var response = new AllNetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetric>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetric>(metric));
            }

            _logger.LogInformation("Сообщение из NetworkMetricsController из метода GetAll");

            return Ok(response);
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromBody] int id)
        {
            _repository.Delete(id);

            _logger.LogInformation("Сообщение из NetworkMetricsController из метода Delete");
            _logger.LogInformation($"{id}");

            return Ok();
        }

        [HttpGet("GetById")]
        public IActionResult GetById([FromBody] int id)
        {
            var metrics = _repository.GetById(id);

            _logger.LogInformation("Сообщение из NetworkMetricsController из метода GetById");
            _logger.LogInformation($"{id}");

            return Ok(metrics);
        }


        [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentile([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime,
            [FromRoute] Percentile percentile)
        {
            _logger.LogInformation("Сообщение из NetworkMetricsController из метода GetMetricsByPercentile");
            _logger.LogInformation($"{fromTime}, {toTime}, {percentile}");
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Сообщение из NetworkMetricsController из метода GetMetrics");
            _logger.LogInformation($"{fromTime}, {toTime}");
            return Ok();
        }
    }
}
