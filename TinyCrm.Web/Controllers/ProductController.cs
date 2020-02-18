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
using TinyCrm.Core.Model.Options;
using TinyCrm.Web.Extensions;

namespace TinyCrm.Web.Controllers
{
    public class ProductController: Controller
    {
       
        private TinyCrm.Core.Data.TinyCrmDbContext context_;
        private Core.Services.IProductService product_;

        public ProductController(TinyCrmDbContext context, Core.Services.IProductService product)
        {
            
            context_ = context;
            product_ = product;
        }


        public IActionResult Index()
        {
            var prodList = context_
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
            var result = product_.AddProductAsync(
                model?.CreateOptions);

            if (result == null) {
                model.ErrorText = "Oops. Something went wrong";

                return View(model);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(
            [FromBody] AddProductOptions options)
        {
            var result = await product_.AddProductAsync
                (options);


            return result.AsStatusResult();

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

        [HttpPut("product/{id}")]
        public  IActionResult UpdateProduct(string id ,
            [FromBody] UpdateProductOptions options)
        {
            var result =  product_.UpdateProduct
              (id,options);

            return result.AsStatusResult();

        }







    }
}
