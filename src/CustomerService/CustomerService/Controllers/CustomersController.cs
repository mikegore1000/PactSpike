using System.Web.Http;
using CustomerService.Models;

namespace CustomerService.Controllers
{
    public class CustomersController : ApiController
    {
        public Customer Get(int id)
        {
            return new Customer(id, "Mike", "Gore");
        }
    }
}
