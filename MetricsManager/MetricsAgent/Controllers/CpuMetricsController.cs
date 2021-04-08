using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsCommon;
using System.Data.SQLite;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ICpuMetricsRepository _repository;

        private readonly IMapper _mapper;

        private readonly ILogger<CpuMetricsController> _logger;

        public CpuMetricsController(ICpuMetricsRepository repository, IMapper mapper, ILogger<CpuMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");

            _repository = repository;
            _mapper = mapper;
        }


        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            _repository.Create(_mapper.Map<CpuMetric>(request));

            _logger.LogInformation("Сообщение из CpuMetricsController из метода Create");
            _logger.LogInformation($"{request.Time}, {request.Value}");

            return Ok();
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] CpuMetricCreateRequest request)
        {
            _repository.Update(_mapper.Map<CpuMetric>(request));

            _logger.LogInformation("Сообщение из CpuMetricsController из метода Update");
            _logger.LogInformation($"{request.Time}, {request.Value}");

            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            IList<CpuMetric> metrics = _repository.GetAll();
            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetric>()
            };
            
            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetric>(metric));
            }

            _logger.LogInformation("Сообщение из CpuMetricsController из метода GetAll");

            return Ok(response);
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromBody] int id)
        {
            _repository.Delete(id);

            _logger.LogInformation("Сообщение из CpuMetricsController из метода Delete");
            _logger.LogInformation($"{id}");

            return Ok();
        }

        [HttpGet("GetById")]
        public IActionResult GetById([FromBody] int id)
        {
            var metrics = _repository.GetById(id);

            _logger.LogInformation("Сообщение из CpuMetricsController из метода GetById");
            _logger.LogInformation($"{id}");

            return Ok(metrics);
        }


        [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentile([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime, [FromRoute] Percentile percentile)
        {
            _logger.LogInformation("Сообщение из CpuMetricsController из метода GetMetricsByPercentile");
            _logger.LogInformation($"{fromTime}, {toTime}, {percentile}");
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Сообщение из CpuMetricsController из метода GetMetrics");
            _logger.LogInformation($"{fromTime}, {toTime}");
            return Ok();
        }
    }
}
