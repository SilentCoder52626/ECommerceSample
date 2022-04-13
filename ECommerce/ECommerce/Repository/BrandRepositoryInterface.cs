using ECommerce.Entity;

namespace ECommerce.Repository
{
    public interface BrandRepositoryInterface
    {
        IQueryable<Brand> GetQueryable();
        Task<Brand?> GetById(long id);
        Task<Brand?> GetByName(string name);
        Task<IList<Brand>> GetAll();
        Task Insert(Brand user);
        Task Update(Brand user);
    }
}
