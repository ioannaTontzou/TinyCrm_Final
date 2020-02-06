using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TinyCrm.Model.Options;
using TinyCrm.Model;

namespace TinyCrm.Services
{
    interface IOrderService
    {
        /// <summary>
        /// Add order to the system and customer 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="addOrder"></param>
        /// <returns></returns>
         Order AddOrder(string orderId, AddOrderOptions options);

        /// <summary>
        /// Change the details of an order
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        bool UpdateOrder(string orderId, UpdateOrderOptions options);

        /// <summary>
        /// Return the details of an order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        void GetOrderById(string orderId);
    }
}
