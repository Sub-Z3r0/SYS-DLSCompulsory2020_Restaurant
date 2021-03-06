﻿using System;
using System.Collections.Generic;
using CustomerAPI.Data;
using CustomerAPI.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SharedModels;

namespace CustomerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        ICustomerRepository repository;
        //IServiceGateway<Customer> productServiceGateway;
        //IMessagePublisher messagePublisher;

        public CustomerController(IRepository<Customer> repos
            //IServiceGateway<Customer> gateway,
            //IMessagePublisher publisher
            )
        {
            repository = repos as ICustomerRepository;
            //productServiceGateway = gateway;
            //messagePublisher = publisher;
        }

        // GET orders
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return repository.GetAll();
        }

        //// GET orders/5
        //[HttpGet("{id}", Name = "GetOrder")]
        //public IActionResult Get(int id)
        //{
        //    var item = repository.Get(id);
        //    if (item == null)
        //    {
        //        return NotFound();
        //    }
        //    return new ObjectResult(item);
        //}

        //// POST orders
        //[HttpPost]
        //public IActionResult Post([FromBody]Customer order)
        //{
        //    return BadRequest();
        //    //if (order == null)
        //    //{
        //    //    return BadRequest();
        //    //}

        //    //if (ProductItemsAvailable(order))
        //    //{
        //    //    try
        //    //    {
        //    //        // Publish OrderStatusChangedMessage. If this operation
        //    //        // fails, the order will not be created
        //    //        messagePublisher.PublishOrderStatusChangedMessage(
        //    //            order.customerId, order.OrderLines, "completed");

        //    //        // Create order.
        //    //        order.Status = Order.OrderStatus.completed;
        //    //        var newOrder = repository.Add(order);
        //    //        return CreatedAtRoute("GetOrder", new { id = newOrder.Id }, newOrder);
        //    //    }
        //    //    catch
        //    //    {
        //    //        return StatusCode(500, "An error happened. Try again.");
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    // If there are not enough product items available.
        //    //    return StatusCode(500, "Not enough items in stock.");
        //    //}
        //}

        ////private bool ProductItemsAvailable(Order order)
        ////{
        ////    foreach (var orderLine in order.OrderLines)
        ////    {
        ////        // Call product service to get the product ordered.
        ////        var orderedProduct = productServiceGateway.Get(orderLine.ProductId);
        ////        if (orderLine.Quantity > orderedProduct.ItemsInStock - orderedProduct.ItemsReserved)
        ////        {
        ////            return false;
        ////        }
        ////    }
        ////    return true;
        ////}

        //// PUT orders/5/cancel
        //// This action method cancels an order and publishes an OrderStatusChangedMessage
        //// with topic set to "cancelled".
        //[HttpPut("{id}/cancel")]
        //public IActionResult Cancel(int id)
        //{
        //    throw new NotImplementedException();

        //    // Add code to implement this method.
        //}

        //// PUT orders/5/ship
        //// This action method ships an order and publishes an OrderStatusChangedMessage.
        //// with topic set to "shipped".
        //[HttpPut("{id}/ship")]
        //public IActionResult Ship(int id)
        //{
        //    throw new NotImplementedException();

        //    // Add code to implement this method.
        //}

        //// PUT orders/5/pay
        //// This action method marks an order as paid and publishes an OrderPaidMessage
        //// (which have not yet been implemented). The OrderPaidMessage should specify the
        //// Id of the customer who placed the order, and a number that indicates how many
        //// unpaid orders the customer has (not counting cancelled orders). 
        //[HttpPut("{id}/pay")]
        //public IActionResult Pay(int id)
        //{
        //    throw new NotImplementedException();

        //    // Add code to implement this method.
        //}

    }
}
