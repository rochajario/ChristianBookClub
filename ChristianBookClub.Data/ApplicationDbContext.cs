using ChristianBookClub.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChristianBookClub.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User, IdentityRole<long>, long>(options)
    {
        #region Entities
        public DbSet<User> AspNetUsers { get; set; }
        public DbSet<Seminar> Seminars { get; set; }
        public DbSet<SeminarSchedule> SeminarSchedules { get; set; }
        public DbSet<SeminarRegister> SeminarRegisters { get; set; }
        public DbSet<SeminarAttendance> SeminarAttendances { get; set; }
        #endregion

        #region Views
        public DbSet<UserHistoric> UsersHistoric { get; set; }
        public DbSet<PublicUpcomingSeminar> PublicUpcomingSeminars { get; set; }
        public DbSet<RegisteredUpcomingSeminar> RegisteredUpcomingSeminars { get; set; }
		#endregion

		protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder
                .Entity<SeminarRegister>()
                .HasIndex(entity => new { entity.SeminarId, entity.UserId })
                .IsUnique(true);

            builder
                .Entity<SeminarAttendance>()
                .HasIndex(entity => new { entity.SeminarScheduleId, entity.SeminarRegisterId })
                .IsUnique(true);

            builder
                .Entity<PublicUpcomingSeminar>()
                .ToView(nameof(PublicUpcomingSeminars))
                .HasKey(k => k.SeminarId);

			builder
				.Entity<RegisteredUpcomingSeminar>()
				.ToView(nameof(RegisteredUpcomingSeminars))
				.HasKey(k => k.SeminarId);

			builder
				.Entity<UserHistoric>()
				.ToView(nameof(UsersHistoric))
				.HasKey(k => k.SeminarId);
		}
    }
}

