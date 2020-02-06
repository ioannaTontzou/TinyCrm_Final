using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TinyCrm.Model.Options;
using TinyCrm.Model;

namespace TinyCrm.Services
{
    interface ICustomerService
    {
         bool CreateCustomer(AddCustomerOptions options);

        //Update data of a customer
         bool UpdateCustomer(string customerId, UpdateCustomerOptions UpCustomer);

        //Search for customers 
         List<Customer> SearchCustomer(SearchCustomerOptions option);
    }
}
