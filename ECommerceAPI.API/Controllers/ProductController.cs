using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    readonly IProductWriteRepository _productWriteRepository;
    readonly IProductReadRepository _productReadRepository;

    public ProductController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
    {
        _productWriteRepository = productWriteRepository;
        _productReadRepository = productReadRepository;
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        var products = _productReadRepository.GetAll();
        return Ok(products);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        Product product = await _productReadRepository.GetByIdAsync(id);
        return Ok(product);
    }
    [HttpPost]
    public async Task Create()
    {
        await _productWriteRepository.AddAsync(new() { Name = "C Product", Price = 1.50F, Stock = 10 });
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Product product)
    {
        Product updatedProduct = await _productReadRepository.GetByIdAsync(product.Id.ToString());
        updatedProduct.Price = product.Price;
        updatedProduct.Stock = product.Stock;
        updatedProduct.Name = product.Name;
        _productWriteRepository.Update(updatedProduct);
         return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        Product p = await _productReadRepository.GetByIdAsync(id);
        _productWriteRepository.Remove(p);
        return Ok();
    }
}
