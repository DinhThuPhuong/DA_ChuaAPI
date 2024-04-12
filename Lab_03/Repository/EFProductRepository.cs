using Lab_03.Models;
using Microsoft.EntityFrameworkCore;
using Lab_03.Data;

namespace Lab_03.Repository
{
    public class EFProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public EFProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Product>> GetProductByCategoryAsync(int id)
        {
            var list = _context.Products.Where(p => p.CategoryId == id).ToList();
            return list;
        }
        public async Task<IEnumerable<Product>> SearchAsync(string keyword)
        {
            // Thực hiện tìm kiếm sản phẩm trong cơ sở dữ liệu
            var searchResults = await _context.Products
                .Where(p => p.Name.Contains(keyword)) // Tìm sản phẩm có tên chứa từ khóa
                .ToListAsync();

            return searchResults;
        }




    }
}
