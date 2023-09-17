using HardCodeProject.Models.Interface;

namespace HardCodeProject.Models
{
    public class Repository:ICatalog
    {
        public List<ProductViewModel> _Products { get; set; }
        public List<CategoryViewModel> _Categories { get; set; }

        public Repository(List<ProductViewModel> products, List<CategoryViewModel> categories)
        {
            _Products = products;
            _Categories = categories;
        }
    }
}
