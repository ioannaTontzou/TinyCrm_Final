using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace TinyCrm.Tests
{
    public class TinyCrmFixture : IDisposable
    {

        public TinyCrm.Core.Data.TinyCrmDbContext DbContext { get; private set; }
        public ILifetimeScope Container { get; private set; }

        public TinyCrmFixture()
        {
            DbContext = new Core.Data.TinyCrmDbContext();
            Container = TinyCrm.Core.ServiceRegistrator.GetContainer().BeginLifetimeScope();
        }


        public void Dispose()
        {
            Container.Dispose();
        }
    }
}
