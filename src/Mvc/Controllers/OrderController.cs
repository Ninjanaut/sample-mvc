using AutoMapper;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mvc.Application.Commands.CreateOrder;
using Mvc.Infrastructure.Data;
using Mvc.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mvc.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public OrderController(ILogger<HomeController> logger, IMediator mediator, AppDbContext context, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateOrderViewModel();
            PopulateCreateViewModel(model);
            return View(model);
        }

        // Server side rendering.
        // In case of client side, we can use Mvc.Filters.ModelAttribute, 
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
                var orderItems 
                    = new List<CreateOrderCommand.OrderItemDto>();

                model.OrderItems.ForEach(orderItem 
                    => orderItems.Add(_mapper.Map<CreateOrderCommand.OrderItemDto>(source: orderItem)));
                
                var response =
                    _mediator.Send(
                        new CreateOrderCommand(
                            model.CustomerId,
                            orderItems,
                            voucherPercentageDiscount: 10));

                _logger.LogInformation($"Order {response.Result} created.");

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
            var products = _context.Products.ToList();

            if (model.OrderItems?.Count == 0)
            {
                foreach (var product in products)
                {
                    model.OrderItems.Add(
                        new CreateOrderViewModel.OrderItemViewModel()
                        {
                            ProductName = product.Name,
                            ProductId = (int)product.Id,
                            Quantity = 1
                        });
                }
            }
        }
    }
}
