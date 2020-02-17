using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Customer CreateCustomer(CreateCustomerOptions options)
        {
            if (options == null) {
                return null;
            }

            if (string.IsNullOrWhiteSpace(options.VatNumber) ||
              string.IsNullOrWhiteSpace(options.Email)) {
                return null;
            }

            if (options.VatNumber.Length > 9) {
                return null;
            }
            if (string.IsNullOrWhiteSpace(options.CountryCode)) {
                return null;
            }

            var exists = SearchCustomers(
                new SearchCustomerOptions()
                {
                    VatNumber = options.VatNumber
                }).Any();

            if (exists) {
                return null;
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

            context_.Add(customer);
            try {
                context_.SaveChanges();
            } catch (Exception) {
                return null;
            }

            return customer;
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

       

        public bool UpdateCustomer(int customerId, UpdateCustomerOptions options)
        {
            if (options == null) {
                return false;
            }
            var customer = GetCustomerById(customerId);
            if (customer == null) {
                return false;
            }
            if (!string.IsNullOrWhiteSpace(options.Email)) {
                customer.Email = options.Email;
            }
            if (!string.IsNullOrWhiteSpace(options.FirstName)) {
                customer.Firstname = options.FirstName;
            }
            if (!string.IsNullOrWhiteSpace(options.LastName)) {
                customer.Lastname = options.LastName;
            }
            if (options.VatNumber != null) {
                customer.VatNumber = options.VatNumber;
            }
            if (options.isActive != null) {
                customer.IsActive = options.isActive;
            }

            return true;
        }

        public Customer GetCustomerById(int id)
        {
            if (id == 0) {
                return null;
            }
            return context_
                .Set<Customer>()
                .Where(s => s.Id == id)
                 .SingleOrDefault();
        }


    }
}
