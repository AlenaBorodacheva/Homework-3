using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent
{
    public interface IRepository<T> where T : class
    {
        void Create(T item);

        IList<T> GetMetrics(DateTimeOffset fromTime, DateTimeOffset toTime);
    }
}
