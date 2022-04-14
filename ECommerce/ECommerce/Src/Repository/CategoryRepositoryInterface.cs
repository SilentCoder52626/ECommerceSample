using ECommerce.Entity;

namespace ECommerce.Repository
{
    public interface CategoryRepositoryInterface
    {
        IQueryable<Category> GetQueryable();
        Task<Category?> GetById(long id);
        Task<Category?> GetByName(string name);
        Task<IList<Category>> GetAll();
        Task Insert(Category user);
        Task Update(Category user);
    }
}
