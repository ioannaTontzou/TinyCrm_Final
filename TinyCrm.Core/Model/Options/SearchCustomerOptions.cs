using System;
using System.Collections.Generic;
using System.Text;

namespace TinyCrm.Core.Model.Options
{
    public class SearchCustomerOptions
    {
        public string VatNumber { get; set; }
        public string Email { get; set; }
        public DateTimeOffset Created { get; set; }
        
        public int CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }
    }
}
