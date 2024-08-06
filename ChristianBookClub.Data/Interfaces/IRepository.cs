using ChristianBookClub.Data.Entities;

namespace ChristianBookClub.Data.Interfaces
{
    public interface IRepository<T> where T : BaseEntity<T>
    {
		ApplicationDbContext Context { get; }

        void Create(T item);
        void Delete(long id);
        void Update(long id, T item);
        IQueryable<T> GetAll();
        T GetById(long id);
    }
}