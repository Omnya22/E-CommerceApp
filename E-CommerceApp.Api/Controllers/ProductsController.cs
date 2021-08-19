using E_CommerceApp.Core.Interfaces;
using E_CommerceApp.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace E_CommerceApp.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public static IWebHostEnvironment _webHostEnvironment;


        public ProductsController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [Route("GetProduct")]
        public async Task<ActionResult> GetById(int id)
        {
            var country = await _unitOfWork.Products.GetByIdAsync(id);
            if (country == null)
                return NotFound();
            return Ok(country);
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<ActionResult> GetProducts()
        {
            var countries = await _unitOfWork.Products.GetAllAsync();
            return Ok(countries);
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

                var result = await _unitOfWork.Products.AddAsync(model);
                _unitOfWork.Done();

                if (result != null)
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
                var product = await _unitOfWork.Products.GetByIdAsync(model.Id);
                if (product == null)
                    return NotFound();
                else
                {
                    product.Name = model.Name;
                    product.PhotoUrl = model.PhotoUrl;
                    product.Price = model.Price;
                    product.Description = model.Description;
                    
                    _unitOfWork.Products.Update(product);

                    _unitOfWork.Done();
                    return Ok(model);
                }
            }
            else
                return BadRequest("check entered value!");
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var Product = await _unitOfWork.Products.GetByIdAsync(id);
            if (Product == null)
                return NotFound();
            else
            {
                _unitOfWork.Products.Delete(Product);
                _unitOfWork.Done();
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

                //Check Photo's extension(Only png and jpeg/jpg)
                var allowedExtesion = new List<string> { ".png", ".jpg", ".jpeg" };
                if (!allowedExtesion.Contains(Path.GetExtension(physicalPath)))
                    return new JsonResult("Only .PNG , .JPG or .jpeg images are allowed!");

                //Check Photo'sSize < 1MB 
                if (physicalPath.Length > 1048576)
                    return new JsonResult("Poster cannot be more than 1 MB!");

                else
                {
                    using (var stream = new FileStream(physicalPath, FileMode.Create))
                    {
                        postedFile.CopyTo(stream);
                    }
                    return new JsonResult(filename);
                }
            }
            catch
            {
                return new JsonResult("unknownProduct.jpeg");
            }
        }


        private bool ProdutExists(string name)
        {
            return _unitOfWork.Products.Find(e => e.Name == name) != null;
        }

    }
}
