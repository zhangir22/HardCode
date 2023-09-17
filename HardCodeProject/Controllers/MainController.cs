using Microsoft.AspNetCore.Mvc;
using HardCodeProject.Models;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Text;
using HardCodeProject.Models.ViewModels;

namespace HardCodeProject.Controllers
{
    public class MainController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7125/api");
        private readonly HttpClient _client;
        private List<ProductViewModel> _products;
        private List<CategoryViewModel> _categories;
        private bool sortIndex;
        private void Initialize()
        {
            HttpResponseMessage productResponse = _client.GetAsync(_client.BaseAddress + "/Products/GetProducts").Result;
            HttpResponseMessage categoryResponse = _client.GetAsync(_client.BaseAddress + "/Categories/GetCategories")
                    .Result;
           

            if (productResponse.IsSuccessStatusCode && categoryResponse.IsSuccessStatusCode)
            {
                string dataProduct = productResponse.Content.ReadAsStringAsync().Result;
                string dataCategory = categoryResponse.Content.ReadAsStringAsync().Result;

                _products = JsonConvert.DeserializeObject<List<ProductViewModel>>(dataProduct);
                _categories = JsonConvert.DeserializeObject<List<CategoryViewModel>>(dataCategory);

            }
        }
        public MainController()
        {
            sortIndex = false;
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            Initialize();
        }
 
        #region ProductActions
        public IActionResult Index()
        {
           
            ViewBag.Categories = _categories;
        
            var products = from p in _products select p;
            
            return View(products.ToList());
        }

        [HttpPost]
        public IActionResult Index(string filter)
        {
            ViewBag.Categories = _categories;
            var lstProduct = from p in _products select p;
            switch (filter)
            {

                case "ProductName":
                    lstProduct = lstProduct.OrderBy(p => p.NameProduct).ToList();
                    break;

                case "DescriptionProduct":
                    lstProduct = lstProduct.OrderBy(p => p.DescriptionProduct).ToList();
                    break;
                case "CountProduct":
                    lstProduct = lstProduct.OrderBy(p => p.CountProduct).ToList();
                    break;
                case "CategoryProduct":
                    lstProduct = lstProduct.OrderBy(p => p.CategoryId).ToList();
                    break;
                default:
                    lstProduct = lstProduct.OrderByDescending(p => p.NameProduct).ToList();
                    break;

            }
            return View(lstProduct.ToList());
        }


        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Message = _categories;
            return View(new ViewProduct());
        }

        [HttpPost]
        public IActionResult Create([Bind("Img,Name,Description,Count,Category")] ViewProduct product)
        {
            if (product == null)
                return NotFound();
            else
            {
                ProductViewModel newProduct = new ProductViewModel()
                {
                    ImgProduct = product.Img,
                    NameProduct = product.Name,
                    DescriptionProduct = product.Description,
                    CountProduct = product.Count,
                    CategoryId = product.Category,
                };

                var data = JsonConvert.SerializeObject(newProduct);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage productCreate = _client.PostAsync
                    (_client.BaseAddress + $"/Products/PostProduct",content).Result;

                if (productCreate.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return NotFound();
            }
        }

        public IActionResult CloserLook(int id)
        {
            HttpResponseMessage productResponse = _client.GetAsync(_client.BaseAddress + $"/Products/GetProduct/{id}").Result;
            if(productResponse.IsSuccessStatusCode) 
            {
                var data = productResponse.Content.ReadAsStringAsync().Result;
                ProductViewModel myProduct = JsonConvert.DeserializeObject<ProductViewModel>(data);
                return View(myProduct);
            }
            return NotFound();
        }
        #endregion

        #region CategoriesActions
        public IActionResult IndexCategory()
        {
            var categories = from c in _categories select c;
            return View(categories.ToList());
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory([Bind("Name")]ViewCategory category) 
        {
            if(category == null)
                return NotFound();
            CategoryViewModel newCategory = new CategoryViewModel
            {
                Name = category.Name,
            };
            var data = JsonConvert.SerializeObject(newCategory);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage categoryCreate = _client.PostAsync(_client.BaseAddress + "/Categories/PostCategory", content).Result;
            if (categoryCreate.IsSuccessStatusCode)
            {
                return RedirectToAction("IndexCategory");
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {

            var category = _categories.FirstOrDefault(u => u.Id == id);
            if (category != null)
            {
                HttpResponseMessage categoryDelete = _client.DeleteAsync(_client.BaseAddress + $"/Categories/DeleteCategory/{id}").Result;
                if (categoryDelete.IsSuccessStatusCode)
                {
                    return RedirectToAction("IndexCategory");
                }
            }

            return NotFound();
        }

        #endregion
    }
}
