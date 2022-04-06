using AutoMapper;
using DevReviews.API.Entities;
using DevReviews.API.Models;
using DevReviews.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DevReviews.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DevReviewsDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductsController(DevReviewsDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        //GET api/products
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _dbContext.Products;
            //var productsViewModel = products.Select(p => new ProductViewModel(p.Id, p.Title, p.Price));
            var productsViewModel = _mapper.Map<List<ProductDetailsViewModel>>(products);
            return Ok(productsViewModel);
        }

        //GET api/products/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _dbContext.Products.SingleOrDefault(p => p.Id == id);
            //Se não achar retornar NotFound()
            if (product == null)
            {
                return NotFound("Produto não encontrado");
            }

            //var reviewsViewModel = product.Reviews.Select(r => new ProductReviewViewModel(r.Id, r.Author,
            //    r.Rating, r.Comments, r.RegisteredAt))
            //    .ToList();

            //var productDetails = new ProductDetailsViewModel(product.Id, product.Title, product.Description, product.Price, product.RegisteredAt, reviewsViewModel);

            var productDetails = _mapper.Map<ProductDetailsViewModel>(product);
            return Ok(productDetails);

        }

        //POST api/products
        [HttpPost]
        public IActionResult Post([FromBody] AddProductInputModel model)
        {
            //se tiver erros retornar BadRequest
            var product = new Product(model.Title, model.Description, model.Price);
            _dbContext.Products.Add(product);

            //constroi uma url para consultar o objeto criado
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, model);
        }

        [HttpPut("{id}")]
        public IActionResult put(int id, [FromBody] UpdateProductInputModel model)
        {
            //se tiver erros de validação retornar BadRequest
            //Se não existir produto com o id especificado, retornar NotFound();
            if (model.Description.Length > 50)
            {
                return BadRequest();
            }

            var product = _dbContext.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound("Produto não encontrado");
            }
            product.Update(model.Description, model.Price);

            return NoContent();
        }
    }
}
