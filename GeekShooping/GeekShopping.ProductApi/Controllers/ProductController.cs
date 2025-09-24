﻿//using GeekShopping.ProductApi.Data.ValuerObjects;
//using GeekShopping.ProductApi.Model;
//using GeekShopping.ProductApi.Repository;
//using Microsoft.AspNetCore.Mvc;

//namespace GeekShopping.ProductApi.Controllers
//{
//    [Route("api/v1/[controller]")]
//    [ApiController]
//    public class ProductController : ControllerBase
//    {
//        private IProductRepository _repository;

//        public ProductController(IProductRepository repository)
//        {
//            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
//        }
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<ProductVO>>>FindAll()
//        {
//            var products = await _repository.FindAll();
//            return Ok(products); 
//        }
//        [HttpPost]
//        public async Task<ActionResult<ProductVO>>Create([FromBody] ProductVO productVo)
//        {
//            if (productVo == null) return BadRequest();
//            var product = await _repository.Create(productVo);
//            return Ok(product);
//        }
//        [HttpPut]
//        public async Task<ActionResult<ProductVO>> Update([FromBody]  ProductVO productVo)
//        {
//            if (productVo == null) return BadRequest();
//            var product = await _repository.Update(productVo);
//            return Ok(product);
//        }
//        [HttpDelete("{id}")]
//        public async Task<ActionResult> Delete(long id)
//        {
//            var status = await _repository.Delete(id);
//            if (!status) return BadRequest();
//            return Ok(status);
//        }

//    }
//}

using GeekShopping.ProductApi.Data.ValuerObjects;
using GeekShopping.ProductApi.Model;
using GeekShopping.ProductApi.Repository;
using GeekShopping.ProductApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductVO>>> FindAll()
        {
            var products = await _repository.FindAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ProductVO>> FindById(long id)
        {
            try
            {
                var product = await _repository.FindById(id);

                if (product == null)
                {
                    return NotFound(new { message = "Produto não encontrado" });
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor", error = ex.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProductVO>> Create([FromBody] ProductVO productVo)
        {
            if (productVo == null) return BadRequest();
            var product = await _repository.Create(productVo);
            return Ok(product);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ProductVO>> Update([FromBody] ProductVO productVo)
        {
            if (productVo == null) return BadRequest();
            var product = await _repository.Update(productVo);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult> Delete(long id)
        {
            var status = await _repository.Delete(id);
            if (!status) return BadRequest();
            return Ok(status);
        }
    }
}