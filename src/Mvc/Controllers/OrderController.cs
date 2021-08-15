using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mvc.Infrastructure.Data;
using Mvc.ViewModels.Order;
using System;
using System.Linq;

namespace Mvc.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;
        private readonly AppDbContext _context;
        public OrderController(ILogger<HomeController> logger, IMediator mediator, AppDbContext context)
        {
            _logger = logger;
            _mediator = mediator;
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateOrderViewModel();
            PopulateCreateViewModel(model);
            return View(model);
        }

        // Server side rendering.
        // In case of client side, we can use 
        // "return BadRequest(ModelState)" and "return Ok()" statements.
        [HttpPost]
        public IActionResult Create(CreateOrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PopulateCreateViewModel(model);
                return View(model);
            }
            try
            {
                model.Command.VoucherPercentageDiscount = 10;

                var response = _mediator.Send(model.Command).Result;

                _logger.LogInformation($"Order {response} created.");

                TempData["success"] = "true";

                return RedirectToAction("Create"); // PRG (post/redirect/get) pattern
            }
            // Domain errors are displayed to the user, but other errors are not.
            catch (Exception e) when (e is DomainException || e is AggregateException && e.InnerException is DomainException)
            {
                ModelState.AddModelError("", e.Message);
                PopulateCreateViewModel(model);
                return View(model);
            }
        }

        private void PopulateCreateViewModel(CreateOrderViewModel model)
        {
            model.Customer = _context.Customers.Find(1); // Han Solo Sample Customer
            model.Products = _context.Products.ToList();
        }
    }
}
