using Ass_11.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ass_11.Data.Repositories;

public interface IProductRepository : IGenericRepository<Product>
{
    Task DeleteProductAsync(Product entity);
    Task<Product?> GetProductAsync(int id);
}

public class ProductRepository : GenericRepository<Product>,IProductRepository
{
    public ProductRepository(MyDbContext context) : base(context)
    {
    }

    public async Task DeleteProductAsync(Product entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("entity");
        }
        _entities.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<Product?> GetProductAsync(int id)
    {
        return await _entities.SingleOrDefaultAsync(s=>s.Id==id);
    }

    
}