namespace WebApplication2.Models
{
	using Microsoft.EntityFrameworkCore;

	public class TestowyZKartki01Context : DbContext
	{
		public TestowyZKartki01Context(DbContextOptions<TestowyZKartki01Context> options)
			: base(options)
		{
		}

		public DbSet<StatusWniosku> StatusWnioskuSet { get; set; }

		public DbSet<DzienTygodnia> DzienTygodniaSet { get; set; }

		public DbSet<Wniosek> WniosekSet { get; set; }

		public DbSet<CzlonekKlubu> CzlonekKlubuSet { get; set; }

		public DbSet<Osoba> OsobaSet { get; set; }

		public DbSet<Adres> AdresSet { get; set; }

		public DbSet<Miejscowosc> MiejscowoscSet { get; set; }

		public DbSet<Wojewodztwo> WojewodztwoSet { get; set; }

		public DbSet<Grafik> GrafikSet { get; set; }

		public DbSet<Termin> TerminSet { get; set; }

		public DbSet<Kurs> KursSet { get; set; }

		public DbSet<Zajecia> ZajeciaSet { get; set; }

		public DbSet<Sala> SalaSet { get; set; }

		public DbSet<PoziomZaawansowania> PoziomZaawansowaniaSet { get; set; }

		public DbSet<Dyscyplina> DyscyplinaSet { get; set; }

		public DbSet<Instruktor> InstruktorSet { get; set; }

		public DbSet<Wyposażenie> WyposażenieSet { get; set; }

		public DbSet<PrzystosowanieSali> PrzystosowanieSaliSet { get; set; }

		public DbSet<DyscyplinaZPoziomem> DyscyplinaZPoziomemSet { get; set; }

		public DbSet<SzczegolyKwalifikacji> SzczegolyKwalifikacjiSet { get; set; }

		public DbSet<BrakujaceWyposazeniePrzystosowanieSali> BrakujaceWyposazeniePrzystosowanieSaliSet { get; set; }

		public DbSet<WymaganeWyposazenieDyscyplina> WymaganeWyposazenieDyscyplinaSet { get; set; }

		public DbSet<CertyfikowaneKwalifikacjeInstruktor> CertyfikowaneKwalifikacjeInstruktorSet { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {

			modelBuilder.Entity<Termin>()
				.HasKey(c => new { c.Dzien, c.GodzinaRozpoczecia });


			modelBuilder.Entity<CzlonekKlubu>()
		        .HasOne(t => t.WniosekPrzyjetyNaPodstawie)
		        .WithOne(t => t.CzlonekKlubuPrzyjetyNaPodstawie)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<Wniosek>()
		        .HasOne(t => t.AdresZameldowania)
		        .WithOne()
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<Wniosek>()
		        .HasOne(t => t.AdresDoKorespondencji)
		        .WithOne()
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



			modelBuilder.Entity<PrzystosowanieSali>()
		        .HasOne(t => t.Dyscyplina)
		        .WithMany(t => t.PrzystosowaneSale)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<PrzystosowanieSali>()
		        .HasOne(t => t.Sala)
		        .WithMany(t => t.Przeznaczenie)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<DyscyplinaZPoziomem>()
		        .HasOne(t => t.Dyscyplina)
		        .WithMany(t => t.Poziomy)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<DyscyplinaZPoziomem>()
		        .HasOne(t => t.PoziomZaawansowania)
		        .WithMany(t => t.Dyscypliny)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<SzczegolyKwalifikacji>()
		        .HasOne(t => t.Instruktor)
		        .WithMany(t => t.Kwalifikacje)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<SzczegolyKwalifikacji>()
		        .HasOne(t => t.DyscyplinaZPoziomem)
		        .WithMany(t => t.Uprawnieni)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Wniosek>().Property(b => b.StatusID).HasDefaultValueSql("1");

		}
	}
}