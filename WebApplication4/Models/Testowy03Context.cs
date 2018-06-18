namespace WebApplication4.Models
{
	using Microsoft.EntityFrameworkCore;

	public class Testowy03Context : DbContext
	{
		public Testowy03Context(DbContextOptions<Testowy03Context> options)
			: base(options)
		{
		}

		public DbSet<Tryb> TrybSet { get; set; }

		public DbSet<Widok> WidokSet { get; set; }

		public DbSet<Slownik> SlownikSet { get; set; }

		public DbSet<Eksperyment> EksperymentSet { get; set; }

		public DbSet<Emocja> EmocjaSet { get; set; }

		public DbSet<Ankieta> AnkietaSet { get; set; }

		public DbSet<EmocjaPodstawowa> EmocjaPodstawowaSet { get; set; }

		public DbSet<EksperymentLokalny> EksperymentLokalnySet { get; set; }

		public DbSet<PrzedmiotBadania> PrzedmiotBadaniaSet { get; set; }

		public DbSet<Wymiar> WymiarSet { get; set; }

		public DbSet<Skala> SkalaSet { get; set; }

		public DbSet<Pozycja> PozycjaSet { get; set; }

		public DbSet<JednostkaLeksykalna> JednostkaLeksykalnaSet { get; set; }

		public DbSet<PrzykladUzyc> PrzykladUzycSet { get; set; }

		public DbSet<JednostkaWSlowniku> JednostkaWSlownikuSet { get; set; }

		public DbSet<Odpowiedz> OdpowiedzSet { get; set; }

		public DbSet<WymiarWBadaniu> WymiarWBadaniuSet { get; set; }

		public DbSet<Ocena> OcenaSet { get; set; }

		public DbSet<BadaneEmocjeEksperyment> BadaneEmocjeEksperymentSet { get; set; }

		public DbSet<ZestawTreningowySlownik> ZestawTreningowySlownikSet { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {


			modelBuilder.Entity<EmocjaPodstawowa>()
		        .HasOne(t => t.EksperymentLokalny)
		        .WithMany(t => t.EmocjaPodstawowa)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<Eksperyment>()
		        .HasOne(t => t.Slownik)
		        .WithMany(t => t.Eksperymenty)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<Ankieta>()
		        .HasOne(t => t.Eksperyment)
		        .WithMany(t => t.Ankiety)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<Eksperyment>()
		        .HasOne(t => t.SkalaEmocji)
		        .WithMany()
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<WymiarWBadaniu>()
		        .HasOne(t => t.Skala)
		        .WithMany()
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<Pozycja>()
		        .HasOne(t => t.Skala)
		        .WithMany(t => t.Pozycje)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);



			modelBuilder.Entity<Ocena>()
		        .HasOne(t => t.WybranaWartosc)
		        .WithMany()
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<JednostkaWSlowniku>()
		        .HasOne(t => t.JednostkaLeksykalna)
		        .WithMany(t => t.Slowniki)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<Slownik>()
		        .HasOne(t => t.Jednostki)
		        .WithOne(t => t.Slownik)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<Odpowiedz>()
		        .HasOne(t => t.JednostkaLeksykalna)
		        .WithMany(t => t.Ankiety)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<Odpowiedz>()
		        .HasOne(t => t.Ankieta)
		        .WithMany(t => t.Jednostki)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<WymiarWBadaniu>()
		        .HasOne(t => t.Wymiar)
		        .WithMany(t => t.EksperymentWymiarWBadaniu)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<WymiarWBadaniu>()
		        .HasOne(t => t.Eksperyment)
		        .WithMany(t => t.BadaneWymiary)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<Odpowiedz>()
		        .HasOne(t => t.PrzedmiotBadaniaOcena)
		        .WithOne(t => t.Odpowiedz)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<PrzedmiotBadania>()
		        .HasOne(t => t.OdpowiedzOcena)
		        .WithOne(t => t.PrzedmiotBadania)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<BadaneEmocjeEksperyment>()
		        .HasOne(t => t.Eksperyment)
		        .WithMany(t => t.BadaneEmocje)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Eksperyment>().Property(b => b.WidokEmocjiID).HasDefaultValueSql("1");
			modelBuilder.Entity<Eksperyment>().Property(b => b.TrybID).HasDefaultValueSql("1");
			modelBuilder.Entity<PrzedmiotBadania>().OwnsOne(p => p.Ikona);

		}
	}
}