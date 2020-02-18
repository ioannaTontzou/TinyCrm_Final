using System;
using Xunit;
using Autofac;

using TinyCrm.Core.Data;

namespace TinyCrm.Tests
{
    public partial class ProductServiceTests : IClassFixture<TinyCrmFixture>
    {
        private readonly Core.Services.IProductService psvc_;
        private readonly TinyCrmDbContext context_;

        public ProductServiceTests(TinyCrmFixture fixture)
        {
            context_ = fixture.DbContext;
            psvc_ = fixture.Container.Resolve<Core.Services.IProductService>();
        }

        [Fact]
        public void GetProductById_Success()
        {
            var product = psvc_.GetProductById("123762");

            Assert.NotNull(product);
            Assert.Equal(1.20M, product.Price);
        }

        [Fact]
        public void GetProductById_Failure_Null_ProductId()
        {
            var product = psvc_.GetProductById("     ");
            Assert.Null(product);

            product = psvc_.GetProductById(null);
            Assert.Null(product);
        }
    }
}
