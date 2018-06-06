namespace WebApplication3.Models
{
	using Microsoft.EntityFrameworkCore;

	public class Testowy02Context : DbContext
	{
		public Testowy02Context(DbContextOptions<Testowy02Context> options)
			: base(options)
		{
		}

		public DbSet<StatusEdycji> StatusEdycji { get; set; }

		public DbSet<StatusZgloszenia> StatusZgloszenia { get; set; }

		public DbSet<TypJednostki> TypJednostki { get; set; }

		public DbSet<TypJednostkiOrganizacyjnej> TypJednostkiOrganizacyjnej { get; set; }

		public DbSet<StatusRecenzji> StatusRecenzji { get; set; }

		public DbSet<StatusPropozycji> StatusPropozycji { get; set; }

		public DbSet<EdycjaKonkursu> EdycjaKonkursu { get; set; }

		public DbSet<ZgloszeniePracy> ZgloszeniePracy { get; set; }

		public DbSet<SlowaKluczowe> SlowaKluczowe { get; set; }

		public DbSet<Autor> Autor { get; set; }

		public DbSet<Nagroda> Nagroda { get; set; }

		public DbSet<JednostkaNaukowa> JednostkaNaukowa { get; set; }

		public DbSet<Recenzent> Recenzent { get; set; }

		public DbSet<Ekspert> Ekspert { get; set; }

		public DbSet<ObszarBadan> ObszarBadan { get; set; }

		public DbSet<Telefon> Telefon { get; set; }

		public DbSet<JednostkaOrganizacyjna> JednostkaOrganizacyjna { get; set; }

		public DbSet<Skrot> Skrot { get; set; }

		public DbSet<DaneAdresowe> DaneAdresowe { get; set; }

		public DbSet<Propozycja> Propozycja { get; set; }

		public DbSet<StatusZatrudnienia> StatusZatrudnienia { get; set; }

		public DbSet<Recenzja> Recenzja { get; set; }

		public DbSet<AutorzyPraca> AutorzyPraca { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {


			modelBuilder.Entity<Autor>()
		        .HasOne(t => t.DaneAdresowe)
		        .WithOne()
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<ZgloszeniePracy>()
		        .HasOne(t => t.Uczelnia)
		        .WithMany(t => t.Praca)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<JednostkaOrganizacyjna>()
		        .HasOne(t => t.DaneAdresowe)
		        .WithOne()
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);



			modelBuilder.Entity<ZgloszeniePracy>()
		        .HasOne(t => t.Promotor)
		        .WithMany(t => t.NadzorowanePrace)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<Recenzent>()
		        .HasOne(t => t.Ekspert)
		        .WithOne(t => t.Konto)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<ZgloszeniePracy>()
		        .HasOne(t => t.Edycja)
		        .WithMany(t => t.ZgloszonePrace)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<Nagroda>()
		        .HasOne(t => t.EdycjaKonkursuPrzydzielanaWRamach)
		        .WithMany(t => t.Nagrody)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<Propozycja>()
		        .HasOne(t => t.Ekspert)
		        .WithMany(t => t.ProponowanePrace)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<Propozycja>()
		        .HasOne(t => t.ZgloszeniePracy)
		        .WithMany(t => t.ProponowaniRecenzenci)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<StatusZatrudnienia>()
		        .HasOne(t => t.Ekspert)
		        .WithMany(t => t.Zatrudnienia)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<StatusZatrudnienia>()
		        .HasOne(t => t.JednostkaOrganizacyjna)
		        .WithMany(t => t.EkspertStatusZatrudnienia)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<Recenzja>()
		        .HasOne(t => t.Recenzent)
		        .WithMany(t => t.OpiniowanePrace)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<Recenzja>()
		        .HasOne(t => t.ZgloszeniePracy)
		        .WithMany(t => t.Recenzenci)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<AutorzyPraca>()
		        .HasOne(t => t.Praca)
		        .WithMany(t => t.Autorzy)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<EdycjaKonkursu>().Property(b => b.StatusID).HasDefaultValueSql("1");
			modelBuilder.Entity<ZgloszeniePracy>().Property(b => b.StatusID).HasDefaultValueSql("1");
			modelBuilder.Entity<Propozycja>().Property(b => b.StatusID).HasDefaultValueSql("1");

		}
	}
}