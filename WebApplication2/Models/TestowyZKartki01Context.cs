namespace WebApplication2.Models
{
	using Microsoft.EntityFrameworkCore;

	public class TestowyZKartki01Context : DbContext
	{
		public TestowyZKartki01Context(DbContextOptions<TestowyZKartki01Context> options)
			: base(options)
		{
		}

		public DbSet<StatusWniosku> StatusWniosku { get; set; }

		public DbSet<DzienTygodnia> DzienTygodnia { get; set; }

		public DbSet<Wniosek> Wniosek { get; set; }

		public DbSet<CzlonekKlubu> CzlonekKlubu { get; set; }

		public DbSet<Osoba> Osoba { get; set; }

		public DbSet<Adres> Adres { get; set; }

		public DbSet<Miejscowosc> Miejscowosc { get; set; }

		public DbSet<Wojewodztwo> Wojewodztwo { get; set; }

		public DbSet<Grafik> Grafik { get; set; }

		public DbSet<Termin> Termin { get; set; }

		public DbSet<Kurs> Kurs { get; set; }

		public DbSet<Zajecia> Zajecia { get; set; }

		public DbSet<Sala> Sala { get; set; }

		public DbSet<PoziomZaawansowania> PoziomZaawansowania { get; set; }

		public DbSet<Dyscyplina> Dyscyplina { get; set; }

		public DbSet<Instruktor> Instruktor { get; set; }

		public DbSet<Wyposażenie> Wyposażenie { get; set; }

		public DbSet<PrzystosowanieSali> PrzystosowanieSali { get; set; }

		public DbSet<DyscyplinaZPoziomem> DyscyplinaZPoziomem { get; set; }

		public DbSet<SzczegolyKwalifikacji> SzczegolyKwalifikacji { get; set; }

		public DbSet<BrakujaceWyposazeniePrzystosowanieSali> BrakujaceWyposazeniePrzystosowanieSali { get; set; }

		public DbSet<WymaganeWyposazenieDyscyplina> WymaganeWyposazenieDyscyplina { get; set; }

		public DbSet<CertyfikowaneKwalifikacjeInstruktor> CertyfikowaneKwalifikacjeInstruktor { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {

			modelBuilder.Entity<Termin>()
				.HasKey(c => new { c.Dzien, c.GodzinaRozpoczecia });			modelBuilder.Entity<Wniosek>().Property(b => b.StatusID).HasDefaultValueSql("1");

		}
	}
}