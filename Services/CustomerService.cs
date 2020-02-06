using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCrm.Model;
using TinyCrm.Model.Options;

namespace TinyCrm.Services
{
    public class CustomerService : ICustomerService
    {
        // the list store all customers
        public static List<Customer> CustomerList = new List<Customer>();

        public bool CreateCustomer(AddCustomerOptions optios)
        {
            if (optios == null) {
                return false;
            }
            if (string.IsNullOrWhiteSpace(optios.FirstName)) {
                return false;
            }
            if (string.IsNullOrWhiteSpace(optios.LastName)) {
                return false;
            }
            if (string.IsNullOrWhiteSpace(optios.EmailAddress)) {
                return false;
            }
            if (string.IsNullOrWhiteSpace(optios.VatNumber) || CustomerList.Where(s => s.VatNumber.Equals(optios.VatNumber)) == null) {
                return false;
            }

            var newCustomer = new Customer(optios.VatNumber, optios.EmailAddress)
            {
                Email = optios.EmailAddress,
                FirstName = optios.FirstName,
                LastName = optios.LastName,
                VatNumber = optios.VatNumber,
                CustomerId = AddCustomerOptions.RandomGeneratorCustomerId(),
                DateCreated = DateTimeOffset.Now.Date,

            };

            if (CustomerList.Contains(newCustomer)) {
                return false;
            }
            CustomerList.Add(newCustomer);
            return true;
        }

        public List<Customer> SearchCustomer(SearchCustomerOptions option)
        {
            var activeCustomerList = new List<Customer>(CustomerList);
            activeCustomerList = activeCustomerList
                .Where(c => c.Status == true).ToList();


            if (option == null) {
                    return null;
              }
           
            if (!string.IsNullOrWhiteSpace(option.Id)) {
                activeCustomerList = activeCustomerList
                                     .Where(c => c.CustomerId.Equals(option.Id))
                                     .ToList();
               }
            
            if (!string.IsNullOrWhiteSpace(option.Phone)) {
                activeCustomerList = activeCustomerList
                                  .Where(c => c.Phone.Equals(option.Phone))
                                  .ToList();
            }
           
            if (!string.IsNullOrWhiteSpace(option.Email)) {
                activeCustomerList = activeCustomerList
                                     .Where(c => c.Email.Equals(option.Email))
                                     .ToList();
            }
            
            if (!string.IsNullOrWhiteSpace(option.LastName)) {
                activeCustomerList = activeCustomerList
                                      .Where(c => c.LastName.Equals(option.LastName))
                                      .ToList();
            }
             
             if (!string.IsNullOrWhiteSpace(option.VatNumber)) {
                activeCustomerList = activeCustomerList
                                 .Where(c => c.VatNumber.Equals(option.VatNumber))
                                 .ToList();
            }
             
              if (option.DateCreated != null) {
                activeCustomerList = activeCustomerList
                                   .Where(c => c.DateCreated == option.DateCreated)
                                   .ToList();
            }


            return activeCustomerList;
                 

        }

        public bool UpdateCustomer(string customerId, UpdateCustomerOptions options)
        {
            if (options == null) {
                return false;
            }
            var customer = GetCustomerById(customerId);
            if(customer == null) {
                return false;
            }
            if(!string.IsNullOrWhiteSpace(options.Email)) {
                customer.Email = options.Email;
            }
            if (!string.IsNullOrWhiteSpace(options.FirstName)) {
                customer.FirstName = options.FirstName;
            }
            if (!string.IsNullOrWhiteSpace(options.LastName)) {
                customer.LastName = options.LastName;
            }
            if (options.VatNumber != null) {
                customer.VatNumber = options.VatNumber;
            }
            if(options.Status != null) {
                customer.Status = options.Status;
            }

            return true;
        }

        public Customer GetCustomerById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) {
                return null;
            }
            return CustomerList.Where(s => s.CustomerId.Equals(id))
                 .SingleOrDefault();
        }
    }
}
