namespace Test.Models
{
	using Microsoft.EntityFrameworkCore;

	public class TestContext : DbContext
	{
		public TestContext(DbContextOptions<TestContext> options)
			: base(options)
		{
		}

		public DbSet<FavouriteNumber> FavouriteNumberSet { get; set; }

		public DbSet<Worker> WorkerSet { get; set; }

		public DbSet<KnownWords> KnownWordsSet { get; set; }

		public DbSet<Baby> BabySet { get; set; }

		public DbSet<Point> PointSet { get; set; }

		public DbSet<CompanyInfo> CompanyInfoSet { get; set; }
	}
}