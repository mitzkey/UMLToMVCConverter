namespace WebApplication1.Models
{
	using Microsoft.EntityFrameworkCore;

	public class DefaultContext : DbContext
	{
		public DefaultContext(DbContextOptions<DefaultContext> options)
			: base(options)
		{
		}

		public DbSet<StatusWniosku> StatusWnioskuSet { get; set; }

		public DbSet<Worker> WorkerSet { get; set; }

		public DbSet<FavouriteNumber> FavouriteNumberSet { get; set; }

		public DbSet<Baby> BabySet { get; set; }

		public DbSet<KnownWords> KnownWordsSet { get; set; }

		public DbSet<CompanyInfo> CompanyInfoSet { get; set; }

		public DbSet<Car> CarSet { get; set; }

		public DbSet<WithSingleIDProperty> WithSingleIDPropertySet { get; set; }

		public DbSet<SteeringWheel> SteeringWheelSet { get; set; }

		public DbSet<CarRadio> CarRadioSet { get; set; }

		public DbSet<Tire> TireSet { get; set; }

		public DbSet<Wheel> WheelSet { get; set; }

		public DbSet<Seat> SeatSet { get; set; }

		public DbSet<Enterprise> EnterpriseSet { get; set; }

		public DbSet<LineSegment> LineSegmentSet { get; set; }

		public DbSet<Book> BookSet { get; set; }

		public DbSet<Professor> ProfessorSet { get; set; }

		public DbSet<Writer> WriterSet { get; set; }

		public DbSet<BookWriter> BookWriterSet { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {

			modelBuilder.Entity<Car>()
				.HasKey(c => new { c.Brand, c.Model, c.Version });


			modelBuilder.Entity<Car>()
		        .HasOne(t => t.SuperRadio)
		        .WithOne(t => t.RadiosCar)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);



			modelBuilder.Entity<Tire>()
		        .HasOne(t => t.Car)
		        .WithMany(t => t.Tire)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);



			modelBuilder.Entity<Seat>()
		        .HasOne(t => t.Car)
		        .WithMany(t => t.Seat)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);



			modelBuilder.Entity<Worker>()
		        .HasOne(t => t.Enterprise)
		        .WithMany(t => t.Worker)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<Book>()
		        .HasOne(t => t.Author)
		        .WithMany(t => t.TextBook)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<Professor>()
		        .HasOne(t => t.FavouriteBook)
		        .WithMany()
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<WithSingleIDProperty>().Property(b => b.StatusID).HasDefaultValueSql("1");
			modelBuilder.Entity<LineSegment>().OwnsOne(p => p.A);
			modelBuilder.Entity<LineSegment>().OwnsOne(p => p.B);

		}
	}
}