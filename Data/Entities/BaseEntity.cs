using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ass_11.Data.Entities;
public class BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime? CreatedTime { get; set; }
    public string? Creator { get; set; }
    public DateTime? ModifiedTime { get; set; }
    public string? Modifier { get; set; }

}