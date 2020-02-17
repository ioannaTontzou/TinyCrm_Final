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
    public partial class CustomerServiceTests : IClassFixture<TinyCrmFixture>
    {
       
            private readonly ICustomerService csvc_;
            private readonly TinyCrmDbContext context;

            public CustomerServiceTests(TinyCrmFixture fixture)
            {
                context = fixture.DbContext;
                csvc_ = fixture.Container.Resolve<ICustomerService>();
            }
            [Fact]
            public void CreateCustomer_Success()
            {
                var options = new CreateCustomerOptions()
                {
                    VatNumber = $"123{DateTime.UtcNow.Millisecond:D6}",
                    Email = "dsSASASAmm",
                    FirstName = "Alexis",
                    LastName = "athop",
                    Phone = "344234",
                    isActive = true
                    
                };

                var result = csvc_.CreateCustomer(options);

                Assert.NotNull(result);

                var customer = csvc_.SearchCustomers(
                    new SearchCustomerOptions()
                    {
                        VatNumber = options.VatNumber
                    }).SingleOrDefault();

                Assert.NotNull(customer);

                Assert.Equal(options.Email, customer.Email);
                Assert.Equal(options.Phone, customer.Phone);
                Assert.True(customer.IsActive);
            }

            
        }
}
