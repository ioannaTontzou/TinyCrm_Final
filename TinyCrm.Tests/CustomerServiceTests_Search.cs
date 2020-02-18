using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Services;

using System.Linq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Autofac;
using System;

namespace TinyCrm.Tests
{
    public partial class CustomerServiceTests_Search : IClassFixture<TinyCrmFixture>
    {
        private readonly ICustomerService csvc_;
        private readonly TinyCrmDbContext context;

        public CustomerServiceTests_Search(TinyCrmFixture fixture)
        {
            context = fixture.DbContext;
            csvc_ = fixture.Container.Resolve<ICustomerService>();
        }


        [Fact]
        public void SearchCustomer_Success()
        {
            var options = new SearchCustomerOptions()
            {
                VatNumber = "555555555"
            };
        
            var customer = csvc_.SearchCustomers(
                    new SearchCustomerOptions()
                    {
                        VatNumber = options.VatNumber
                    }).SingleOrDefault();

            Assert.NotNull(customer);

            Assert.Equal(options.VatNumber, customer.VatNumber);
            Assert.True(customer.IsActive);
        }


    }
  }

