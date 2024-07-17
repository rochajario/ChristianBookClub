using ChristianBookClub.Data.Entities;
using ChristianBookClub.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChristianBookClub.Data.Repositories
{
	public abstract class BaseRepository<T>(ApplicationDbContext context) : IRepository<T> where T : BaseEntity<T>
	{
		protected readonly ApplicationDbContext _context = context;
		public ApplicationDbContext Context { get { return _context; } }

		public virtual void Create(T item)
		{
			_context.Set<T>().Add(item);
			_context.SaveChanges();
		}

		public virtual IQueryable<T> GetAll()
		{
			return _context.Set<T>().AsQueryable();
		}

		public virtual T GetById(long id)
		{
			return _context.Set<T>().Single(x => x.Id.Equals(id));
		}

		public virtual void Update(long id, T item)
		{
			var entity = GetById(id);
			if (entity is not null)
			{
				entity.SetValue(item);
				_context.Entry(entity).State = EntityState.Modified;
				_context.SaveChanges();
			}
		}

		public virtual void Delete(long id)
		{
			var entity = GetById(id);
			if (entity is not null)
			{
				_context.Set<T>().Remove(entity);
				_context.SaveChanges();
			}
		}
	}
}
