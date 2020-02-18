using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Mvc;
using TinyCrm.Core;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using TinyCrm.Web.Extensions;

namespace TinyCrm.Web.Controllers
{
    public class CustomerController : Controller
    {
       
        private TinyCrmDbContext Context { get; set; }
        private Core.Services.ICustomerService customers_;

        public CustomerController(TinyCrmDbContext context, Core.Services.ICustomerService customers)
        {

            Context = context;
            customers_ = customers;
        }

        public IActionResult Index()
        {
            var customerList = Context
                .Set<Customer>()
                .Take(100)
                .ToList();

            return View(customerList);
        }

        public IActionResult List()
        {
            var customerList = Context
                .Set<Customer>()
                .Select(c => new { c.Email, c.VatNumber })
                .Take(100)
                .ToList();

            return Json(customerList);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(
            Models.CreateCustomerViewModel model)
        {
            var result = customers_.CreateCustomerAsync(
                model?.CreateOptions);

            if (result == null) {
                model.ErrorText = "Oops. Something went wrong";

                return View(model);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer( [FromBody]CreateCustomerOptions options)
        {
            var result =  await customers_.CreateCustomerAsync
                (options);




            return result.AsStatusResult();
                    
        }

        public IActionResult Search(
           Models.SearchCustomerViewModel model)
        {
            
            var result = customers_.SearchCustomers(
                model?.SearchOptions);

            if (result == null) {
                model.ErrorText = "Oops. Something went wrong";

                return View(model);
            } else {
                model.Customers = result.ToList();
                return View(model);
            }
           
        }
    }
}