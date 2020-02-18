﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
   public class OrderService : IOrderService
    {
        private readonly ICustomerService customers_;
        private readonly Data.TinyCrmDbContext context_;
        private readonly IProductService product_;

        public OrderService(
            ICustomerService customers,
            IProductService product,
            Data.TinyCrmDbContext context)
        {
            context_ = context;
            customers_ = customers;
            product_ = product;
        }

        public ApiResult<Order> CreateOrder(int customerId,
            ICollection<string> productIds)
        {
            if (customerId <= 0) {
                return new ApiResult<Order>(StatusCodecs.BADREQUEST,
                    "not valid customer id");
            }

            if (productIds == null ||
              productIds.Count == 0) {
                return new ApiResult<Order>(StatusCodecs.BADREQUEST,
                    "not valid product list");
            }

            var customer = customers_.SearchCustomers(
                new SearchCustomerOptions()
                {
                    CustomerId = customerId
                })
                .Where(c => c.IsActive)
                .SingleOrDefault();

            if (customer == null) {
                return new ApiResult<Order>(StatusCodecs.BADREQUEST,
                    "not valid customer ");
            }


            foreach (var p in productIds) {
                var result =  product_.GetProductById(p); 

                if(result.ErrorCode != StatusCodecs.OK) {
                    return result.GetApi<Order>();
                }
                return new ApiResult<Order>()
                {
                    ErrorCode = StatusCodecs.OK,
                    ErrorText = "ok "
                };
            }

            var products = context_
               .Set<Product>()
               .Where(p => productIds.Contains(p.Id))
               .ToList();

            if (products.Count != productIds.Count) {

                return new ApiResult<Order>(StatusCodecs.BADREQUEST,
                    "not valid product id");
            }

            var order = new Order()
            {
                Customer = customer
            };

            foreach (var p in products) {
                order.Products.Add(
                    new OrderProduct()
                    {
                        ProductId = p.Id
                    });
            }

            context_.Add(order);

            try {
                context_.SaveChanges();
            } catch (Exception) {
                return null;
            }

            return ApiResult<Order>.CreateSuccess(order);
        }
    }
}
