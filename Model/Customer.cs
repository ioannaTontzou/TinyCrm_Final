using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyCrm.Model
{
   public class Customer
    {
        
        /// <summary>
        ///Id of customer
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Date that customer created in system
        /// </summary>
        public DateTimeOffset DateCreated { get; set; }

        /// <summary>
        /// Firstname of customer
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// LastName of customer
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The order list of customer
        /// </summary>
        public List<Order> Orders { get; set; }

        /// <summary>
        /// VatNumber of customer
        /// </summary>
        public string VatNumber { get; set; }

        /// <summary>
        /// Email of customer
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// //ACTIVE -> TRUE
        /// </summary>
        //public bool status { get { return status; } set { status = true; } }
        public bool Status { get; set; }

        /// <summary>
        /// Total money that customer spends
        /// </summary>
        public decimal TotalMoney { get; set; }

        public Customer(string vatNum, string email)
        {
            DateCreated = DateTimeOffset.Now;
            VatNumber = vatNum;
            Email = email;
        }
    }
}
