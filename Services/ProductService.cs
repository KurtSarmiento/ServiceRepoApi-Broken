using ServiceRepoApi.Models;
using ServiceRepoApi.Repositories;

namespace ServiceRepoApi.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }
    

    public Task<Product?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

    public async Task<ServiceResult> CreateAsync(Product product)
    {
        product.Name = product.Name.Trim();
        product.CreatedAt = DateTime.Now;
        await _repository.AddAsync(product);
        await _repository.SaveChangesAsync();
        return ServiceResult.Ok();
    }

    public async Task<ServiceResult> UpdateAsync(Product product)
    {
        var existing = await _repository.GetByIdAsync(product.Id);
        if (existing is null)
            return ServiceResult.Ok();

        existing.Name = product.Name.Trim();
        existing.Price = product.Price;
        existing.Stock = product.Stock;
        //existing.CreatedAt = product.CreatedAt;

        _repository.Update(existing);
        await _repository.SaveChangesAsync();
        return ServiceResult.Ok();
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null)
            return ServiceResult.NotFound($"Product with ID {id} not found.");
        _repository.Delete(existing);
        await _repository.SaveChangesAsync();
        return ServiceResult.Ok();
    }
}
