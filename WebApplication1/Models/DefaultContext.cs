namespace WebApplication1.Models
{
	using Microsoft.EntityFrameworkCore;

	public class DefaultContext : DbContext
	{
		public DefaultContext(DbContextOptions<DefaultContext> options)
			: base(options)
		{
		}

		public DbSet<FavouriteNumber> FavouriteNumber { get; set; }

		public DbSet<Worker> Worker { get; set; }

		public DbSet<KnownWords> KnownWords { get; set; }

		public DbSet<Baby> Baby { get; set; }

		public DbSet<Point> Point { get; set; }

		public DbSet<CompanyInfo> CompanyInfo { get; set; }

		public DbSet<Car> Car { get; set; }

		public DbSet<WithSingleIDProperty> WithSingleIDProperty { get; set; }

		public DbSet<Wheel> Wheel { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<Car>()
				.HasKey(c => new { c.Brand, c.Model, c.Version });
		}
	}
}