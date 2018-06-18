namespace WebApplication3.Models
{
	using Microsoft.EntityFrameworkCore;

	public class Testowy02Context : DbContext
	{
		public Testowy02Context(DbContextOptions<Testowy02Context> options)
			: base(options)
		{
		}

		public DbSet<StatusEdycji> StatusEdycjiSet { get; set; }

		public DbSet<StatusZgloszenia> StatusZgloszeniaSet { get; set; }

		public DbSet<TypJednostki> TypJednostkiSet { get; set; }

		public DbSet<TypJednostkiOrganizacyjnej> TypJednostkiOrganizacyjnejSet { get; set; }

		public DbSet<StatusRecenzji> StatusRecenzjiSet { get; set; }

		public DbSet<StatusPropozycji> StatusPropozycjiSet { get; set; }

		public DbSet<EdycjaKonkursu> EdycjaKonkursuSet { get; set; }

		public DbSet<ZgloszeniePracy> ZgloszeniePracySet { get; set; }

		public DbSet<SlowaKluczowe> SlowaKluczoweSet { get; set; }

		public DbSet<Autor> AutorSet { get; set; }

		public DbSet<Nagroda> NagrodaSet { get; set; }

		public DbSet<JednostkaNaukowa> JednostkaNaukowaSet { get; set; }

		public DbSet<Recenzent> RecenzentSet { get; set; }

		public DbSet<Ekspert> EkspertSet { get; set; }

		public DbSet<ObszarBadan> ObszarBadanSet { get; set; }

		public DbSet<Telefon> TelefonSet { get; set; }

		public DbSet<JednostkaOrganizacyjna> JednostkaOrganizacyjnaSet { get; set; }

		public DbSet<Skrot> SkrotSet { get; set; }

		public DbSet<DaneAdresowe> DaneAdresoweSet { get; set; }

		public DbSet<Propozycja> PropozycjaSet { get; set; }

		public DbSet<StatusZatrudnienia> StatusZatrudnieniaSet { get; set; }

		public DbSet<Recenzja> RecenzjaSet { get; set; }

		public DbSet<AutorzyPraca> AutorzyPracaSet { get; set; }

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



			modelBuilder.Entity<JednostkaNaukowa>()
		        .HasOne(t => t.Nadrzedna)
		        .WithMany(t => t.JednostkaNaukowaPodjednostki)
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



			modelBuilder.Entity<ZgloszeniePracy>()
		        .HasOne(t => t.Nagroda)
		        .WithMany(t => t.Prace)
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