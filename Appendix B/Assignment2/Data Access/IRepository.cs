using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Repositories
{
    public interface IRepository<T>
    {
        T GetByID(int id);
        List<T> GetAll();
        public List<T> GetPage(int limit, int offset = 0);
        bool Add(T entity);
        bool Edit(T entity, int id);
        bool Delete(int id);
    }
}
