using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetAll();

        T GetById(int id);

        void Create(T item);

        void Update(T item);

        void Delete(int id);

        IList<T> GetMetrics(int agentId, TimeSpan fromTime, TimeSpan toTime);
    }
}
