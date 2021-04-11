using System;
using System.Collections.Generic;
using System.Linq;
using MetricsCommon;

namespace MetricsAgent
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetAll();

        T GetById(int id);

        void Create(T item);

        void Update(T item);

        void Delete(int id);

        IList<T> GetMetrics(TimeSpan fromTime, TimeSpan toTime);
    }
}
