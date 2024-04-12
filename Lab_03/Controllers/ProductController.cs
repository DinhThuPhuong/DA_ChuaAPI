using Lab_03.Models;
using Lab_03.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lab_03.Controllers
{

    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        //public IActionResult Products(int id)
        //{
        //    var productsInCategory = _productRepository.GetProductByCategoryAsync(id);
        //    //var categories = _categoryRepository.GetAll();

        //   // ViewData["Categories"] = categories; // Truyền danh sách danh mục sản phẩm vào ViewData

        //    return View(productsInCategory);
        //}

        // Hiển thị danh sách sản phẩm
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            foreach (var product in products)
            {
                if (product.CategoryId != null)
                {
                    product.Category = await _categoryRepository.GetByIdAsync(product.CategoryId);
                }
            }
            return View(products);
        }
        public IActionResult Display1(int id)
        {
            var product = _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound(); // Trả về trang 404 nếu không tìm thấy sản phẩm
            }

            return View(product); // Trả về view hiển thị thông tin chi tiết sản phẩm
        }
        // Hiển thị thông tin chi tiết sản phẩm
        public async Task<IActionResult> Display(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        public async Task<IActionResult> SearchResult(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                // Nếu từ khóa tìm kiếm rỗng, trả về trang chính
                return RedirectToAction("Index");
            }

            // Gọi hàm tìm kiếm sản phẩm dựa trên keyword từ repository
            var searchResults = await _productRepository.SearchAsync(keyword);
            foreach (var product in searchResults)
            {
                if (product.CategoryId != null)
                {
                    product.Category = await _categoryRepository.GetByIdAsync(product.CategoryId);
                }
            }

            // Trả về view kết quả tìm kiếm và truyền dữ liệu tìm kiếm vào view
            return View("SearchResult", searchResults);
        }
    }
}