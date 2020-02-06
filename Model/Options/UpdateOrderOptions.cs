using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyCrm.Model.Options
{
   public  class UpdateOrderOptions
    {
        /// <summary>
        /// The deliveryaddress that will be changed in order
        /// </summary>
        public string DeliveryAddress { get; set; }

      

        /// <summary>
        /// A bool variable for cancel an order
        /// </summary>
        public bool TobeCancel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OrderStatus OrdStatus { get; set; }
    }
}
