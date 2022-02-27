using Ass_11.Data.Repositories;
using Ass_11.Models;
using Ass_11.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ass_11.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{

    private readonly ILogger<CategoryController> _logger;

    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;

    public CategoryController(ILogger<CategoryController> logger, ICategoryRepository categoryRepository, IProductRepository productRepository)
    {
        _logger = logger;
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var entities = await _categoryRepository.GetAllIncludedAsync();
        var result = from item in entities
                     select new CategoryViewModel
                     {
                         Id = item.Id,
                         Name = item.Name,
                         Products = from p in item.Products
                                    select new ProductViewModel
                                    {
                                        Id = p.Id,
                                        Name = p.Name,
                                        Manufacture = p.Manufacture
                                    }
                     };

        return new JsonResult(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOneAsync(int id)
    {
        var entities = await _categoryRepository.GetOneAsync(id);
        if (entities == null) return NotFound();

        return new JsonResult(new CategoryViewModel
        {
            Id = entities.Id,
            Name = entities.Name,
            Products = from p in entities.Products
                       select new ProductViewModel
                       {
                           Id = p.Id,
                           Name = p.Name,
                           Manufacture = p.Manufacture
                       }
        });
    }


    [HttpPost]
    public async Task<IActionResult> CreateAsync(CategoryCreateModel model)
    {
        try
        {

            var entity = new Data.Entities.Category
            {
                Name = model.Name,
                Products = (from p in model.Products
                            select new Data.Entities.Product
                            {
                                Name = p.Name,
                                Manufacture = p.Manufacture
                            }).ToList()
            };
            var result = await _categoryRepository.InsertAsync(entity);
            return new JsonResult(new CategoryViewModel
            {
                Id = result.Id,
                Name = result.Name,
                Products = from p in result.Products
                           select new ProductViewModel
                           {
                               Id = p.Id,
                               Name = p.Name,
                               Manufacture = p.Manufacture
                           }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, CategoryCreateModel model)
    {
        try
        {
            var entity = await _categoryRepository.GetOneAsync(id);
            if (entity == null) return NotFound();

            entity.Name = model.Name;
            entity.Products = (from p in model.Products
                               select new Data.Entities.Product
                               {
                                   Name = p.Name,
                                   Manufacture = p.Manufacture
                               }).ToList();


            var result = await _categoryRepository.UpdateAsync(entity);
            return new JsonResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            var result = await _productRepository.GetProductAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            else{
                await _productRepository.DeleteProductAsync(entity);
            }
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
