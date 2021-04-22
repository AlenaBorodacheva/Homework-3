using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using Dapper;
using MetricsAgent.Models;

namespace MetricsAgent
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetric>
    {
    }

    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        // инжектируем соединение с базой данных в наш репозиторий через конструктор
        public NetworkMetricsRepository()
        {
            // добавляем парсилку типа TimeSpan в качестве подсказки для SQLite
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }

        public void Create(NetworkMetric item)
        {
            using (var connection = new SQLiteConnection(SQlSettings.ConnectionString))
            {
                //  запрос на вставку данных с плейсхолдерами для параметров
                connection.Execute("INSERT INTO networkmetrics(value, time) VALUES(@value, @time)",
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


        public IList<NetworkMetric> GetMetrics(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using (var connection = new SQLiteConnection(SQlSettings.ConnectionString))
            {
                return connection.Query<NetworkMetric>("SELECT Id, Time, Value FROM networkmetrics WHERE time>@fromTime AND time<@toTime",
                    new { fromTime = fromTime, toTime = toTime }).ToList();
            }
        }
    }
}
