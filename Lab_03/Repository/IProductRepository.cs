using Lab_03.Models;

namespace Lab_03.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> SearchAsync(string keyword);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task<IEnumerable<Product>> GetProductByCategoryAsync(int id);

    }

  

}
