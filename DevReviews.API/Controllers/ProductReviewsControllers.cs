using DevReviews.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevReviews.API.Controllers
{
    [Route("api/products/{productId}/productreviews")]
    [ApiController]
    public class ProductReviewsControllers : ControllerBase
    {
        //GET api/products/id/productreviews/5
        [HttpGet("{id}")]
        public IActionResult GetById(int productId, int id)
        {//se nao existir retornar NotFound()
            return Ok();
        }

        [HttpPost]
        public IActionResult Post(int productId, AddProductReviewInputModel model)
        {//se estiver com formato invalido retornar BadRequest()
            return CreatedAtAction(nameof(GetById), new { id = 1, productId = 2 }, model);
        }

    }
}
