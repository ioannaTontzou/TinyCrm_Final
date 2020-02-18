using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyCrm.Core.Model;

namespace TinyCrm.Core.Services
{
    public interface ICustomerService
    {
       Task<ApiResult<Customer>> CreateCustomerAsync(
             Model.Options.CreateCustomerOptions options);

        IQueryable<Model.Customer> SearchCustomers(
            Model.Options.SearchCustomerOptions options);

        Task<ApiResult<Customer>> UpdateCustomerAsync(int customerId,
            Model.Options.UpdateCustomerOptions options);


      
    }
}
