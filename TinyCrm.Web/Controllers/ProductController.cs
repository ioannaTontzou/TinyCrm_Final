using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TinyCrm.Web.Models;
using TinyCrm.Core;
using TinyCrm.Core.Services;
using Autofac;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;

namespace TinyCrm.Web.Controllers
{
    public class ProductController: Controller
    {
        private  IContainer Container { get; set; }
        private TinyCrm.Core.Data.TinyCrmDbContext context;
        private Core.Services.IProductService product_;

        public ProductController()
        {
            Container = ServiceRegistrator.GetContainer();
            context = Container.Resolve<TinyCrmDbContext>();
            product_ = Container.Resolve<Core.Services.IProductService>();
        }


        public IActionResult Index()
        {
            var prodList = context
                       .Set<Product>()
                       .Take(100)
                       .ToList();

            return  View(prodList);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(
            Models.CreateProductViewModel model)
        {
            var result = product_.AddProduct(
                model?.CreateOptions);

            if (result == null) {
                model.ErrorText = "Oops. Something went wrong";

                return View(model);
            }

            return Ok();
        }

        public IActionResult Search(
           Models.SearchProductViewModel model)
        {

            var result = product_.SearchProduct(
                model?.SearchOptions);

            if (result == null) {
                model.ErrorText = "Oops. Something went wrong";

                return View(model);
            } else {
                model.Products = result.ToList();
                return View(model);
            }

        }






    }
}
