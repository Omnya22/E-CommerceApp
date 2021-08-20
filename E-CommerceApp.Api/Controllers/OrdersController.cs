using E_CommerceApp.Core.Interfaces;
using E_CommerceApp.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_CommerceApp.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("GetOrder")]
        public async Task<ActionResult> GetById(int id)
        {
            var order = await _unitOfWork.Orders.FindAsync(o => o.Id == id, new[] { "Product" });
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpGet]
        [Route("GetAllOrders")]
        public ActionResult GetOrders()
        {
            var orders =_unitOfWork.OrderProducts.GetAll(new[] {"Orders"});
            return Ok(orders);
        }


        [HttpPost]
        [Route("AddOrder")]
        public async Task<ActionResult> AddOrder(Order model)
        {
            if (model == null)
                return NotFound();
            if (ModelState.IsValid)
            {
                var Status = new OrderStatus
                {
                    IsWaited = true,
                    IsAccepted = false,
                    IsRejected = false
                };
                model.OrderStatus = Status;
         
                var result = await _unitOfWork.Orders.AddAsync(model);
                _unitOfWork.Done();

                if (result != null)
                    return Ok(result);
            }
            return BadRequest();
        }


        [HttpPut]
        [Route("UpdateOrder")]
        public async Task<ActionResult> UpdateOrder(Order model)
        {
            if (model == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                var order = await _unitOfWork.Orders.GetByIdAsync(model.Id);
                if (order == null)
                    return NotFound();
                else
                {
                    var Status = new OrderStatus
                    {
                        IsWaited = model.OrderStatus.IsWaited,
                        IsAccepted = model.OrderStatus.IsAccepted,
                        IsRejected = model.OrderStatus.IsRejected
                    };

                    order.OrderStatus = Status;
                    //order.Products = model.Products;

                    _unitOfWork.Orders.Update(order);

                    _unitOfWork.Done();
                    return Ok(model);
                }
            }
            else
                return BadRequest("check entered value!");
        }

        [HttpDelete]
        [Route("DeleteOrder")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order == null)
                return NotFound();
            else
            {
                _unitOfWork.Orders.Delete(order);
                _unitOfWork.Done();
                return Ok(order);
            }
        }

    }
}
