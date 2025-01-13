using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.RequestHelpers;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class ProductsController(StoreContext context, IMapper mapper, ImageService imageService) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<PagedList<Product>>> GetProducts([FromQuery] ProductParams productParams)
    {
        var query = context.Products
            .Sort(productParams.OrderBy)
            .Search(productParams.SearchTerm)
            .Filter(productParams.Brands, productParams.Types)
            .AsQueryable();

        var products =
            await PagedList<Product>.ToPagedList(query, productParams.PageNumber, productParams.PageSize);

        Response.AddPaginationHeader(products.MetaData);

        return products;
    }
    
    [HttpGet("filters")]
    public async Task<IActionResult> GetFilters()
    {
        var brands = await context.Products.Select(p => p.Brand).Distinct().ToListAsync();
        var types = await context.Products.Select(p => p.Type).Distinct().ToListAsync();

        return Ok(new { brands, types });
    }

    /*[Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct([FromForm]CreateProductDto productDto)
    {
        var product = mapper.Map<Product>(productDto);

        if (productDto.File is not null)
        {
            var imageResult = await imageService.AddImageAsync(productDto.File);

            if (imageResult.Error is not null)
                return BadRequest(new ProblemDetails { Title = imageResult.Error.Message });

            product.PictureUrl = imageResult.SecureUrl.ToString();
            product.PublicId = imageResult.PublicId;
        }
        
        context.Products.Add(product);

        var result = await context.SaveChangesAsync() > 0;

        if (result) return CreatedAtRoute("GetProduct", new { Id = product.Id }, product);

        return BadRequest(new ProblemDetails { Title = "Problem creating a new product" });
    }*/
    //TODO: relocate to create product slice

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<ActionResult<Product>> UpdateProduct([FromForm]UpdateProductDto productDto)
    {
        var product = await context.Products.FindAsync(productDto.Id);
        if (product is null) return NotFound();

        mapper.Map(productDto, product);

        if (productDto.File is not null)
        {
            var imageResult = await imageService.AddImageAsync(productDto.File);
            
            if (imageResult.Error is not null)
                return BadRequest(new ProblemDetails { Title = imageResult.Error.Message });

            if (!string.IsNullOrEmpty(product.PublicId)) 
                await imageService.DeleteImageAsync(product.PublicId);
            
            product.PictureUrl = imageResult.SecureUrl.ToString();
            product.PublicId = imageResult.PublicId;
        }
        
        var result = await context.SaveChangesAsync() > 0;

        if (result) return Ok(product);

        return BadRequest(new ProblemDetails { Title = "The problem was occured by update product" });
    }
}