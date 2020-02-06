using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCrm.Services;

namespace TinyCrm.Model.Options
{
   public class AddCustomerOptions
    {
        /// <summary>
        /// Firstname of newCustomer
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Lastname of newCustomr
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Id of new Customer
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// VatNumber of newCustomer
        /// </summary>
        public string VatNumber { get; set; }

        /// <summary>
        /// Email of newCustomer
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Status of newCustomer
        /// active->true   inactive->false
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// CustomerId generator
        /// </summary>
        /// <returns></returns>
        public static string RandomGeneratorCustomerId()
        {
            Random r = new Random();
            var randomNum = r.Next(1, 1000000);
            var randomId = Convert.ToString(randomNum);

            
            if (CustomerService.CustomerList.Where(s => s.CustomerId == randomId) == null) {
                RandomGeneratorCustomerId();
            }
            return randomId;
        }
    }
}
