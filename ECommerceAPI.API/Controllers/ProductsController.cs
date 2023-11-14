using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    readonly IProductWriteRepository _productWriteRepository;
    readonly IProductReadRepository _productReadRepository;

    public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
    {
        _productWriteRepository = productWriteRepository;
        _productReadRepository = productReadRepository;
    }
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_productReadRepository.GetAll(false));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        Product product = await _productReadRepository.GetByIdAsync(id, false);
        return Ok(product);
    }
    [HttpPost]
    public async Task<IActionResult> Post(VM_Create_Product model)
    {
        //if (ModelState.IsValid) { }
        await _productWriteRepository.AddAsync(new()
        {
            Name = model.Name,
            Price = model.Price,
            Stock = model.Stock
        });
        await _productWriteRepository.SaveAsync();
        return StatusCode((int)HttpStatusCode.Created);
    }
    [HttpPut]
    public async Task<IActionResult> Put(VM_Update_Product model)
    {
        Product updatedProduct = await _productReadRepository.GetByIdAsync(model.Id.ToString());
        updatedProduct.Price = model.Price;
        updatedProduct.Stock = model.Stock;
        updatedProduct.Name = model.Name;
        await _productWriteRepository.SaveAsync();
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _productWriteRepository.Remove(await _productReadRepository.GetByIdAsync(id));
        return StatusCode((int)HttpStatusCode.NoContent);
    }
}
