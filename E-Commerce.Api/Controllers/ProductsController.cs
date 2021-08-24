using E_Commerce.Api.DataAccess;
using E_Commerce.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize("Admin")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        private IMongoCollection<Product> _collection;

        public static IWebHostEnvironment _webHostEnvironment;

        public ProductsController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _collection = _context.GetCollection<Product>("Products");
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [Route("GetProduct")]
        public async Task<ActionResult> GetById(string id)
        {
            var product = await _collection.FindAsync(p=>p.Id == id);
            if (product == null)
                return NotFound();
            return Ok(product.FirstOrDefaultAsync().Result);
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<ActionResult> GetProducts()
        {
            var products = await _collection.FindAsync(_=> true);
            return Ok(products.ToListAsync().Result);
        }


        [HttpPost]
        [Route("AddProduct")]
        public async Task<ActionResult> AddProduct(Product model)
        {
            if (model == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                if (ProdutExists(model.Name))
                    return BadRequest("This Produt is already exists.");

                await _collection.InsertOneAsync(model);
                var result = _collection.Find(p => p.Id == model.Id).FirstOrDefault();
                if (result!=null)
                    return Ok(result);
            }
            return BadRequest();
        }


        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<ActionResult> UpdateProduct(Product model)
        {
            if (model == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                var product = await _collection.FindAsync(p=>p.Id == model.Id);
                if (product == null)
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
        [Route("DeleteProduct")]
        public async Task<ActionResult> DeleteProduct(string id)
        {
            var Product = _collection.Find(p => p.Id == id).FirstOrDefault();
            if (Product == null)
                return NotFound();
            else
            {
                await _collection.DeleteOneAsync(P=>P.Id == id);
                return Ok(Product);
            }
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _webHostEnvironment.WebRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(filename);
            }
            catch
            {
                return new JsonResult("unknownProduct.jpeg");
            }
        }


        private bool ProdutExists(string name)
        {
            return _collection.Find(e => e.Name == name).FirstOrDefault() != null;
        }


    }
}
