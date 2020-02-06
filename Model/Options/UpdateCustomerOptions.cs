using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyCrm.Model.Options
{
   public class UpdateCustomerOptions
    {
        /// <summary>
        /// Firstname that will be changed
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Firstname that will be changed
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// LastName that will be changed
        /// </summary>
        public string VatNumber { get; set; }

        /// <summary>
        /// VatNumber that will be changed
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Email that will be changed
        /// </summary>
        public bool Status { get; set; }
    }
}
