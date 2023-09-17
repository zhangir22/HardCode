namespace HardCodeProject.Models.Interface
{
    public interface ICatalog
    {
        List<ProductViewModel> _Products { get; set; }
        List<CategoryViewModel> _Categories { get; set; }
    }
}
