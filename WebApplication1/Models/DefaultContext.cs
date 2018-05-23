namespace WebApplication1.Models
{
	using Microsoft.EntityFrameworkCore;

	public class DefaultContext : DbContext
	{
		public DefaultContext(DbContextOptions<DefaultContext> options)
			: base(options)
		{
		}

		public DbSet<StatusWniosku> StatusWniosku { get; set; }

		public DbSet<Worker> Worker { get; set; }

		public DbSet<FavouriteNumber> FavouriteNumber { get; set; }

		public DbSet<Baby> Baby { get; set; }

		public DbSet<KnownWords> KnownWords { get; set; }

		public DbSet<CompanyInfo> CompanyInfo { get; set; }

		public DbSet<Car> Car { get; set; }

		public DbSet<WithSingleIDProperty> WithSingleIDProperty { get; set; }

		public DbSet<SteeringWheel> SteeringWheel { get; set; }

		public DbSet<CarRadio> CarRadio { get; set; }

		public DbSet<Tire> Tire { get; set; }

		public DbSet<Wheel> Wheel { get; set; }

		public DbSet<Seat> Seat { get; set; }

		public DbSet<Enterprise> Enterprise { get; set; }

		public DbSet<LineSegment> LineSegment { get; set; }

		public DbSet<Book> Book { get; set; }

		public DbSet<Professor> Professor { get; set; }

		public DbSet<Writer> Writer { get; set; }

		public DbSet<BookWriter> BookWriter { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {

			modelBuilder.Entity<Car>()
				.HasKey(c => new { c.Brand, c.Model, c.Version });			modelBuilder.Entity<WithSingleIDProperty>().Property(b => b.StatusID).HasDefaultValueSql("1");
			modelBuilder.Entity<LineSegment>().OwnsOne(p => p.X);
			modelBuilder.Entity<LineSegment>().OwnsOne(p => p.Y);

		}
	}
}