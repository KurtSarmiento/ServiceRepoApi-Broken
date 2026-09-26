using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceRepoApi.Data;
using ServiceRepoApi.Models;
using ServiceRepoApi.Services;

namespace ServiceRepoApi.Controllers;

[Route("api/[controller]s")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    //private readonly AppDbContext _context;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
        //_context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll()
    {
        var products = await _productService.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{productId}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Create(Product product)
    {
        var products = await _productService.GetAllAsync();
        if (products.Any(p => p.Name == product.Name))
            return Conflict(new { error = $"A product named '{product.Name}' already exists." });

        //product.Name = product.Name.Trim();
        //product.CreatedAt = DateTime.Now;

        var result = await _productService.CreateAsync(product);
        if (!result.Success) return ToErrorResult(result);

        return Ok(product);
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> Update(int id, Product product)
    {
        var result = await _productService.UpdateAsync(product);
        if (!result.Success) return ToErrorResult(result);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _productService.DeleteAsync(id);
        if (!result.Success) return ToErrorResult(result);
        
        //await _context.SaveChangesAsync();
        //return NoContent();
    }

    private ActionResult ToErrorResult(ServiceResult result) => result.Status switch
    {
        ServiceResultStatus.NotFound => Conflict(new { error = result.Error }),
        ServiceResultStatus.Conflict => NotFound(new { error = result.Error }),
        _ => BadRequest(new { error = result.Error })
    };
}
