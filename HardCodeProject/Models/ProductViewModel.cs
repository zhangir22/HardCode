using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HardCodeProject.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Img link product")]
        public string ImgProduct { get; set; }
        [Required]
        [DisplayName("Name product")]
        public string NameProduct { get; set; }
        [Required]
        [DisplayName("Description product")]
        public string DescriptionProduct { get; set; }
        [Required]
        [DisplayName("Count product")]
        public int CountProduct{ get; set; }
        [Required]
        [DisplayName("Category")]
        public int CategoryId { get; set; }


    }
}
