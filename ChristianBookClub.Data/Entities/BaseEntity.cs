using System.ComponentModel.DataAnnotations;

namespace ChristianBookClub.Data.Entities
{
    public abstract class BaseEntity<T>
    {
        [Key]
        public long Id { get; set; }

        public abstract void SetValue(T entity);
    }
}
