using Assignment2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        public List<Customer> GetByName(string name);
        public List<CustomerCountry> GetCountryCount();
        public List<CustomerSpender> GetCustomerSpenders();
        public List<CustomerGenre> GetMostPopularGenres(int id);
    }
}
