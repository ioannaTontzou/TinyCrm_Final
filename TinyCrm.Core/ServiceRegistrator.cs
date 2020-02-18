using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Services;

using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;

namespace TinyCrm.Core
{
   public class ServiceRegistrator : Module
    {
        public static void RegisterServices(ContainerBuilder builder)
        {
            if(builder == null) {
                throw new ArgumentNullException();
            }

            builder
                .RegisterType<ProductService>()
                .InstancePerLifetimeScope()
                .As<IProductService>();

            builder
               .RegisterType<CustomerService>()
               .InstancePerLifetimeScope()
               .As<ICustomerService>();

            builder
               .RegisterType<OrderService>()
               .InstancePerLifetimeScope()
               .As<IOrderService>();
            builder
               .RegisterType<TinyCrm.Core.Data.TinyCrmDbContext>()
               .InstancePerLifetimeScope()
               .AsSelf();

            
        }

        public static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            RegisterServices(builder);
            return builder.Build();
        }

        protected override void Load(ContainerBuilder builder)
        {
            RegisterServices(builder);
        }



    }
}
