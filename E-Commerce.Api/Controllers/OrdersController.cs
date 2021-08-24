using E_Commerce.Api.DataAccess;
using E_Commerce.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        private IMongoCollection<Order> _collection;

        public OrdersController(AppDbContext context)
        {
            _context = context;
            _collection = _context.GetCollection<Order>("Orders");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetOrder")]
        public async Task<ActionResult> GetById(string id)
        {
            var order = await _collection.FindAsync(p => p.Id == id);
            if (order == null)
                return NotFound();
            return Ok(order.FirstOrDefaultAsync().Result);
        }

        [HttpGet]
        [Authorize("Admin")]
        [Route("GetAllOrders")]
        public async Task<ActionResult> GetOrders()
        {
            var orders = await _collection.FindAsync(_ => true);
            return Ok(orders.ToListAsync().Result);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("AddOrder")]
        public async Task<ActionResult> AddOrder(Order model)
        {
            if (model == null)
                return NotFound();
            if (ModelState.IsValid)
            {
                model.IsWaited = true;
                model.IsAccepted = false;
                model.IsRejected = false;


                await _collection.InsertOneAsync(model);
                var result = _collection.Find(p => p.Id == model.Id).FirstOrDefault();
                if (result != null)
                    return Ok(result);
            }
            return BadRequest();
        }


        [HttpPut]
        [Authorize("Admin")]
        [Route("UpdateOrder")]
        public async Task<ActionResult> UpdateOrder(Order model)
        {
            if (model == null)
                return NotFound();
            if (ModelState.IsValid)
            {
                var order = await _collection.FindAsync(p => p.Id == model.Id);
                if (order == null)
                    return NotFound();
                else
                {
                    await _collection.ReplaceOneAsync(p => p.Id == model.Id, model);
                    return Ok(model);
                }
            }
            else
                return BadRequest("check entered value!");
        }

        [HttpDelete]
        [Authorize("Admin")]
        [Route("DeleteOrder")]
        public async Task<ActionResult> DeleteOrder(string id)
        {
            var order = _collection.Find(p => p.Id == id).FirstOrDefault();
            if (order == null)
                return NotFound();
            else
            {
                await _collection.DeleteOneAsync(P => P.Id == id);
                return Ok(order);
            }
        }

    }
}
