using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using Dapper;
using MetricsAgent.Models;

namespace MetricsAgent
{
    public interface IRamMetricsRepository : IRepository<RamMetric>
    {
    }

    public class RamMetricsRepository : IRamMetricsRepository
    {
        // инжектируем соединение с базой данных в наш репозиторий через конструктор
        public RamMetricsRepository()
        {
            // добавляем парсилку типа TimeSpan в качестве подсказки для SQLite
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }

        public void Create(RamMetric item)
        {
            using (var connection = new SQLiteConnection(SQlSettings.ConnectionString))
            {
                //  запрос на вставку данных с плейсхолдерами для параметров
                connection.Execute("INSERT INTO rammetrics(value, time) VALUES(@value, @time)",
                    // анонимный объект с параметрами запроса
                    new
                    {
                        // value подставится на место "@value" в строке запроса
                        // значение запишется из поля Value объекта item
                        value = item.Value,

                        // записываем в поле time количество секунд
                        time = item.Time
                    });
            }
        }

        public IList<RamMetric> GetMetrics(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new SQLiteConnection(SQlSettings.ConnectionString))
            {
                return connection.Query<RamMetric>("SELECT Id, Time, Value FROM rammetrics WHERE time>@fromTime AND time<@toTime",
                    new { fromTime = fromTime, toTime = toTime }).ToList();
            }
        }
    }
}
