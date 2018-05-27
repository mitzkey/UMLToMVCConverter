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

		public DbSet<PrzystosowaneSalePrzeznaczenie> PrzystosowaneSalePrzeznaczenie { get; set; }

		public DbSet<BrakujaceWyposazeniePrzystosowanieSali> BrakujaceWyposazeniePrzystosowanieSali { get; set; }

		public DbSet<WymaganeWyposazenieDyscyplina> WymaganeWyposazenieDyscyplina { get; set; }

		public DbSet<PoziomyDyscypliny> PoziomyDyscypliny { get; set; }

		public DbSet<CertyfikowaneKwalifikacjeInstruktor> CertyfikowaneKwalifikacjeInstruktor { get; set; }

		public DbSet<KwalifikacjeUprawnieni> KwalifikacjeUprawnieni { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {

			modelBuilder.Entity<Termin>()
				.HasKey(c => new { c.Dzien, c.GodzinaRozpoczecia });


			modelBuilder.Entity<CzlonekKlubu>()
		        .HasOne(t => t.WniosekPrzyjetyNaPodstawie)
		        .WithOne(t => t.CzlonekKlubuPrzyjetyNaPodstawie)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<Adres>()
		        .HasOne(t => t.Miejscowosc)
		        .WithMany()
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<Miejscowosc>()
		        .HasOne(t => t.WojewodztwoMiejscowosci)
		        .WithMany(t => t.MiejscowoscMiejscowosci)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<Termin>()
		        .HasOne(t => t.Grafik)
		        .WithMany(t => t.Terminy)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);



			modelBuilder.Entity<Kurs>()
		        .HasOne(t => t.Grafik)
		        .WithMany(t => t.Kursy)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);



			modelBuilder.Entity<Zajecia>()
		        .HasOne(t => t.Termin)
		        .WithMany(t => t.Zajecia)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<Zajecia>()
		        .HasOne(t => t.Kurs)
		        .WithMany(t => t.Zajecia)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);



			modelBuilder.Entity<Zajecia>()
		        .HasOne(t => t.Sala)
		        .WithMany(t => t.Zajecia)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<PrzystosowanieSali>()
		        .HasOne(t => t.Poziom)
		        .WithMany(t => t.PrzystosowanieSali)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<PrzystosowaneSalePrzeznaczenie>()
		        .HasOne(t => t.PrzystosowaneSale)
		        .WithMany(t => t.Przeznaczenie)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<PoziomyDyscypliny>()
		        .HasOne(t => t.Dyscypliny)
		        .WithMany(t => t.Poziomy)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<KwalifikacjeUprawnieni>()
		        .HasOne(t => t.Uprawnieni)
		        .WithMany(t => t.Kwalifikacje)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Wniosek>().Property(b => b.StatusID).HasDefaultValueSql("1");

		}
	}
}