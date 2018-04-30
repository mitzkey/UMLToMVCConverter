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

		public DbSet<SteeringWheel> SteeringWheel { get; set; }

		public DbSet<CarRadio> CarRadio { get; set; }

		public DbSet<Tire> Tire { get; set; }

		public DbSet<Wheel> Wheel { get; set; }

		public DbSet<Seat> Seat { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {

			modelBuilder.Entity<Car>()
				.HasKey(c => new { c.Brand, c.Model, c.Version });

			modelBuilder.Entity<Car>()
		        .HasOne(typeof(SteeringWheel))
		        .WithOne("Car")
		        .HasForeignKey("SteeringWheel", "CarBrand", "CarModel", "CarVersion")
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);



			modelBuilder.Entity<Car>()
		        .HasOne(typeof(CarRadio))
		        .WithOne("Car")
		        .HasForeignKey("CarRadio", "CarBrand", "CarModel", "CarVersion")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);



			modelBuilder.Entity<Car>()
		        .HasMany(typeof(Tire))
		        .WithOne("Car")
		        .HasForeignKey("CarBrand", "CarModel", "CarVersion")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);



			modelBuilder.Entity<Car>()
		        .HasMany(typeof(Seat))
		        .WithOne("Car")
		        .HasForeignKey("CarBrand", "CarModel", "CarVersion")
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);


		}
	}
}