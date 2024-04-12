using Lab_03.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace Lab_03.Models
{
    public class Navbar:ViewComponent
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public Navbar(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public IViewComponentResult Invoke()
        {
            var listTask = _categoryRepository.GetAllAsync();
            listTask.Wait(); // Chờ cho tác vụ hoàn thành

            var list = listTask.Result;
            return View(list);
        }
    }
}
