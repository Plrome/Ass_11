using Ass_11.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ass_11.Data.Repositories;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<IEnumerable<Category>> GetAllIncludedAsync();
    Task<Category?> GetOneAsync(int id);
    
}

public class CategoryRepository : GenericRepository<Category>,ICategoryRepository
{
    public CategoryRepository(MyDbContext context) : base(context)
    {
    }

   

    public async Task<IEnumerable<Category>> GetAllIncludedAsync()
    {
        return await _entities.Include(c => c.Products).ToListAsync();
    }

    public async Task<Category?> GetOneAsync(int id)
    {
        return await _entities.Include(c=>c.Products).SingleOrDefaultAsync(s=>s.Id==id);
    }
    
}