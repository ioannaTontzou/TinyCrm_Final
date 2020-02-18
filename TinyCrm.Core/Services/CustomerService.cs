using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    public class CustomerService : ICustomerService

    {
        private readonly Data.TinyCrmDbContext context_;

        public CustomerService(Data.TinyCrmDbContext context)
        {
            context_ = context ??
                throw new ArgumentException(nameof(context));
        }

        public async Task<ApiResult<Customer>> CreateCustomerAsync(CreateCustomerOptions options)
        {
            if (options == null) {
                return new ApiResult<Customer>(
                    StatusCodecs.BADREQUEST, "null options"
                    );
               
            }

            if (string.IsNullOrWhiteSpace(options.VatNumber) ||
              string.IsNullOrWhiteSpace(options.Email)) {
                return new ApiResult<Customer>(
                    StatusCodecs.BADREQUEST,"null email"
                    );
                
            }

            if (options.VatNumber.Length > 9) {
                return new ApiResult<Customer>(
                    StatusCodecs.BADREQUEST, "not valid vatnumber");
              
            }
            if (string.IsNullOrWhiteSpace(options.CountryCode)) {
                return new ApiResult<Customer>(
                    StatusCodecs.BADREQUEST,"null country");
               
            }

            var exists = await SearchCustomers(
                new SearchCustomerOptions()
                {
                    VatNumber = options.VatNumber
                }).AnyAsync();

            if (exists) {
                return new ApiResult<Customer>
                    (StatusCodecs.Conflict, "customer exist");
               
            }

            var customer = new Customer()
            {
                VatNumber = options.VatNumber,
                Firstname = options.FirstName,
                Lastname = options.LastName,
                Email = options.Email,
                Phone = options.Phone,
                IsActive = true,
                CoutryCode = options.CountryCode

            };

           await context_.AddAsync(customer);
            try {
              await  context_.SaveChangesAsync();
            } catch (Exception) {
                return new ApiResult<Customer>(StatusCodecs.InternalServerError,"problem save data");
               
            }

            return ApiResult<Customer>.CreateSuccess(customer);
                     
        }

        public IQueryable<Customer> SearchCustomers(
            SearchCustomerOptions options)
        {
            if (options == null) {
                return null;
            }

            var query = context_
                .Set<Customer>()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(options.VatNumber)) {
                query = query.Where(c =>
                    c.VatNumber == options.VatNumber);
            }

            if (!string.IsNullOrWhiteSpace(options.Email)) {
                query = query.Where(c =>
                    c.Email == options.Email);
            }

            if (options.CustomerId >0) {
                query = query.Where(c =>
                    c.Id == options.CustomerId);
            }
            if (!string.IsNullOrWhiteSpace(options.FirstName)) {
                query = query.Where(c =>
                    c.Firstname == options.FirstName);
            }

            if (!string.IsNullOrWhiteSpace(options.LastName)) {
                query = query.Where(c =>
                    c.Lastname == options.LastName);
            }

            if (!string.IsNullOrWhiteSpace(options.Phone)) {
                query = query.Where(c =>
                    c.Phone == options.Phone);
            }


            return query.Take(500);
        }

       

        public async Task<ApiResult<Customer>> UpdateCustomerAsync(int customerId, UpdateCustomerOptions options)
        {
            if (options == null) {
                return new ApiResult<Customer>(
                    StatusCodecs.BADREQUEST, "null options"
                    );
            }
            var customer = await GetCustomerByIdAsync(customerId);
            if (customer == null) {
                return new ApiResult<Customer>(
                    StatusCodecs.NotFOUND, "not exist"
                    );
            }
            if (!string.IsNullOrWhiteSpace(options.Email)) {
                return new ApiResult<Customer>(
                    StatusCodecs.BADREQUEST, "null options email"
                    );
            }
            if (!string.IsNullOrWhiteSpace(options.FirstName)) {
                customer.Data.Firstname = options.FirstName;
            }
            if (!string.IsNullOrWhiteSpace(options.LastName)) {
                customer.Data.Lastname = options.LastName;
               
            }
            if (options.VatNumber != null) {
                customer.Data.VatNumber = options.VatNumber;
            }

            if (options.isActive != null) {
                customer.Data.IsActive = options.isActive;
            }

            return ApiResult<Customer>.CreateSuccess(customer.Data);
        }

        public async Task<ApiResult<Customer>> GetCustomerByIdAsync(int id)
        {
            if (id == 0) {
                return new ApiResult<Customer>(
                    StatusCodecs.BADREQUEST, "not valid id");
            }

            var result = await context_
                .Set<Customer>()
                .Where(s => s.Id == id)
                 .SingleOrDefaultAsync();

            if (result == null) {
                return new ApiResult<Customer>(
                     StatusCodecs.NotFOUND, "not found");
            }

            return ApiResult<Customer>.CreateSuccess(result);


        }

    }
}
