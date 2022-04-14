using ECommerce.Entity;

namespace ECommerce.Repository
{
    public interface TagRepositoryInterface
    {
        IQueryable<Tag> GetQueryable();
        Task<Tag?> GetById(long id);
        Task<Tag?> GetByName(string name);
        Task<IList<Tag>> GetAll();
        Task Insert(Tag user);
        Task Update(Tag user);
    }
}
