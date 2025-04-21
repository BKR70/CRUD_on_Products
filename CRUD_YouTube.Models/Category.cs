using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRUD_YouTube.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Name")]
        public string? Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 200)]
        public int DisplayOrder { get; set; }

    }
}
