using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCrm.Model;
using TinyCrm.Model.Options;

namespace TinyCrm.Services
{
    class OrderService : IOrderService

    {

        public ProductService prodService = new ProductService();
        public  CustomerService customerService = new CustomerService();

        private List<Order> OrdersList = new List<Order>();

        public int counter_ = 0;

       

        public Order AddOrder(string custId, AddOrderOptions options)
        {
            if (options == null) {
                return null;
            }
            if (string.IsNullOrWhiteSpace(custId)) {
                return null;
            }
            
            var customer = customerService.GetCustomerById(options.CustomerId);
            if(customer == null) {
                return null;
            }
            if (string.IsNullOrWhiteSpace(options.DeliveryAddress)) {
                return null;
            }
            if(options.OrderProductList.Count <= 0) {
                return null;
            }
            foreach(var p in options.OrderProductList) {
                if (prodService.GetProductById(p.Id) ==null) {
                    return null;
                }
            }
            if (!customer.Status.Equals(true)) {
                return null;
            }

            var order = new Order() {
                DeliveryAddress = options.DeliveryAddress,
                OrderProductList = options.OrderProductList,
                CustomerId = options.CustomerId,
                OrderId = Convert.ToString(counter_ + 1),
                OrderStatus = OrderStatus.Pending,
                TotalMount = GetTotalAmount(options.OrderProductList)
            };
            OrdersList.Add(order);
            return order;
        }

        public Order FindOrderById(string orderId)
        {
            if (string.IsNullOrWhiteSpace(orderId)) {
                return null;
            }
            return OrdersList.Where(s => s.CustomerId.Equals(orderId))
                 .SingleOrDefault();
        }

        public bool UpdateOrder(string orderId, UpdateOrderOptions options)
        {
           if(options == null) {
                return false;
            }
            if (string.IsNullOrWhiteSpace(orderId)) {
                return false;
            }
            var order = FindOrderById(orderId);
            if(order == null) {
                return false;
            }    
            if (order.OrderStatus == OrderStatus.Execute) {
                return false;
            }
            if(options.TobeCancel == false) {
                return false;
            }
            var custom = customerService.GetCustomerById(order.CustomerId);

            if(order.OrderStatus == OrderStatus.Pending && options.TobeCancel == true) {
                order.OrderStatus = OrderStatus.Cansel;

                Console.WriteLine("press 1 for cancel order or 2 for cancel and  add new order ");
                var x =int.Parse(Console.ReadLine());
                if (x == 1) {
                    OrdersList.Remove(order);
                }else if(x == 2) {
                    OrdersList.Remove(order);
                    var opt = new AddOrderOptions(){
						
					}; 
                    OrdersList.Add(
                    AddOrder(custom.CustomerId, opt));
                }    
            }
            return true;
        }

        public static decimal GetTotalAmount(List<Product> prodList)
        {
            decimal totam = 0.00M;
            foreach( var p in prodList) {
                totam = totam + p.Price;
            }
            return totam;
        }

        public void GetOrderById(string oId)
        {
           var pdc = FindOrderById(oId);
            Console.WriteLine($"Order Details : Customer {customerService.GetCustomerById(pdc.CustomerId)}" +
                $"Products {pdc.OrderProductList}  ID {pdc.OrderId}  Status {pdc.OrderStatus}" +
                $"TotalAmmount {pdc.TotalMount}");
            
        }
    }
}
