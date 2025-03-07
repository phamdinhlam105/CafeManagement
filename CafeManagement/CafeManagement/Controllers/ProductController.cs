﻿using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Helpers;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Interfaces.Services;
using CafeManagement.Mappers;
using CafeManagement.Models;
using CafeManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductMapper _productMapper;
        public ProductController(IProductService productService, IProductMapper productMapper)
        {
            _productService = productService;
            _productMapper = productMapper;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var products = (await _productService.GetAll()).ToList();
            return Ok(products.Select(c => _productMapper.MapToResponse(c)).ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            return Ok(_productMapper.MapToResponse(await _productService.GetById(id)));
        }

     
        [HttpPost]
        [Authorize(Roles =Role.Manager)]
        public async Task<ActionResult> Add([FromBody] ProductRequest product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var item = _productMapper.MapToEntity(product);
            await _productService.Add(item);
            return CreatedAtAction(nameof(GetById), new { item.Id });
        }

        [HttpPut("{id}")]
        [Authorize(Roles =Role.Manager)]
        public async Task<ActionResult> Edit(Guid id,[FromBody] ProductRequest product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingProduct = await _productService.GetById(id);
            if (existingProduct == null)
                return NotFound();
            _productMapper.UpdateEntityFromRequest(existingProduct, product);
            await _productService.Update(existingProduct);
            return Ok(existingProduct);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles =Role.Manager)]
        public async Task<ActionResult> Delete(Guid id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
                return NotFound();

            await _productService.Delete(product);
            return Ok();
        }
    }
}

