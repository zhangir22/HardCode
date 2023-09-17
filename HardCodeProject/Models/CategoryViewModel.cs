using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HardCodeProject.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Name Category")]
        public string Name { get; set; }
    }
}
