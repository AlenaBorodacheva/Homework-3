using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsCommon;
using Microsoft.Extensions.Logging;
using System.Data.SQLite;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private IRamMetricsRepository _repository;

        private readonly ILogger<RamMetricsController> _logger;

        public RamMetricsController(IRamMetricsRepository repository)
        {
            _repository = repository;
        }

        public RamMetricsController(ILogger<RamMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в RamMetricsController");
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            _repository.Create(new RamMetric
            {
                Time = request.Time,
                Value = request.Value
            });

            _logger.LogInformation("Сообщение из RamMetricsController из метода Create");
            _logger.LogInformation($"{request.Time}, {request.Value}");

            return Ok();
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] RamMetricCreateRequest request)
        {
            _repository.Update(new RamMetric
            {
                Time = request.Time,
                Value = request.Value
            });

            _logger.LogInformation("Сообщение из RamMetricsController из метода Update");
            _logger.LogInformation($"{request.Time}, {request.Value}");

            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = _repository.GetAll();
            
            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetric>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new RamMetric { Time = metric.Time, Value = metric.Value, Id = metric.Id });
            }

            _logger.LogInformation("Сообщение из RamMetricsController из метода GetAll");

            return Ok(response);
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromBody] int id)
        {
            _repository.Delete(id);

            _logger.LogInformation("Сообщение из RamMetricsController из метода Delete");
            _logger.LogInformation($"{id}");

            return Ok();
        }

        [HttpGet("GetById")]
        public IActionResult GetById([FromBody] int id)
        {
            var metrics = _repository.GetById(id);

            _logger.LogInformation("Сообщение из RamMetricsController из метода GetById");
            _logger.LogInformation($"{id}");

            return Ok(metrics);
        }


        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Сообщение из RamMetricsController из метода GetMetrics");
            _logger.LogInformation($"{fromTime}, {toTime}");
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentile([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime,
            [FromRoute] Percentile percentile)
        {
            _logger.LogInformation("Сообщение из RamMetricsController из метода GetMetricsByPercentile");
            _logger.LogInformation($"{fromTime}, {toTime}, {percentile}");
            return Ok();
        }

        [HttpGet("available")]
        public IActionResult GetMetricsAvailable()
        {
            _logger.LogInformation("Сообщение из RamMetricsController из метода GetMetricsAvailable");
            return Ok();
        }


        [HttpGet("sql-test")]
        public IActionResult TryToSqlLite()
        {
            string cs = "Data Source=:memory:";
            string stm = "SELECT SQLITE_VERSION()";

            using (var con = new SQLiteConnection(cs))
            {
                con.Open();

                using var cmd = new SQLiteCommand(stm, con);
                string version = cmd.ExecuteScalar().ToString();

                return Ok(version);
            }
        }

        [HttpGet("sql-read-write-test")]
        public IActionResult TryToInsertAndRead()
        {
            // Создаем строку подключения в виде базы данных в оперативной памяти
            string connectionString = "Data Source=:memory:";

            // создаем соединение с базой данных
            using (var connection = new SQLiteConnection(connectionString))
            {
                // открываем соединение
                connection.Open();

                // создаем объект через который будут выполняться команды к базе данных
                using (var command = new SQLiteCommand(connection))
                {
                    // задаем новый текст команды для выполнения
                    // удаляем таблицу с метриками если она существует в базе данных
                    command.CommandText = "DROP TABLE IF EXISTS cpumetrics";
                    // отправляем запрос в базу данных
                    command.ExecuteNonQuery();

                    // создаем таблицу с метриками
                    command.CommandText = @"CREATE TABLE cpumetrics(id INTEGER PRIMARY KEY,
                    value INT, time INT)";
                    command.ExecuteNonQuery();

                    // создаем запрос на вставку данных
                    command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(10,1)";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(50,2)";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(75,4)";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(90,5)";
                    command.ExecuteNonQuery();

                    // создаем строку для выборки данных из базы
                    // LIMIT 3 обозначает, что мы достанем только 3 записи
                    string readQuery = "SELECT * FROM cpumetrics LIMIT 3";

                    // создаем массив, в который запишем объекты с данными из базы данных
                    var returnArray = new RamMetric[3];
                    // изменяем текст команды на наш запрос чтения
                    command.CommandText = readQuery;

                    // создаем читалку из базы данных
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        // счетчик для того, чтобы записать объект в правильное место в массиве
                        var counter = 0;
                        // цикл будет выполняться до тех пор, пока есть что читать из базы данных
                        while (reader.Read())
                        {
                            // создаем объект и записываем его в массив
                            returnArray[counter] = new RamMetric
                            {
                                Id = reader.GetInt32(0), // читаем данные полученные из базы данных
                                Value = reader.GetInt32(0), // преобразуя к целочисленному типу
                                Time = TimeSpan.FromSeconds(reader.GetInt32(0))
                            };
                            // увеличиваем значение счетчика
                            counter++;
                        }
                    }
                    // оборачиваем массив с данными в объект ответа и возвращаем пользователю 
                    return Ok(returnArray);
                }
            }
        }
    }
}
