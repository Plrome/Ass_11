using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ass_11.Data.Entities;


public class Category : BaseEntity
{
    [Required, MaxLength(50)]
    public string? Name { get; set; }

    public ICollection<Product>? Products { get; set; }
}