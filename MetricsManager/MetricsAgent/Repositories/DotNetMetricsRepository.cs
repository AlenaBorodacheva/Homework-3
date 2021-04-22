using System;
using System.Collections.Generic;
using System.Linq;
using MetricsCommon;
using System.Data.SQLite;
using Dapper;
using MetricsAgent.Models;

namespace MetricsAgent
{
    public interface IDotNetMetricsRepository : IRepository<DotNetMetric>
    {
    }

    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        // инжектируем соединение с базой данных в наш репозиторий через конструктор
        public DotNetMetricsRepository()
        {
            // добавляем парсилку типа TimeSpan в качестве подсказки для SQLite
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }

        public void Create(DotNetMetric item)
        {
            using (var connection = new SQLiteConnection(SQlSettings.ConnectionString))
            {
                //  запрос на вставку данных с плейсхолдерами для параметров
                connection.Execute("INSERT INTO dotnetmetrics(value, time) VALUES(@value, @time)",
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

        public IList<DotNetMetric> GetMetrics(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new SQLiteConnection(SQlSettings.ConnectionString))
            {
                return connection.Query<DotNetMetric>("SELECT Id, Time, Value FROM dotnetmetrics WHERE time>@fromTime AND time<@toTime",
                    new { fromTime = fromTime, toTime = toTime }).ToList();
            }
        }
    }
}
