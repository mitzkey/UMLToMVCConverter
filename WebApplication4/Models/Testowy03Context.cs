namespace WebApplication4.Models
{
	using Microsoft.EntityFrameworkCore;

	public class Testowy03Context : DbContext
	{
		public Testowy03Context(DbContextOptions<Testowy03Context> options)
			: base(options)
		{
		}

		public DbSet<Tryb> Tryb { get; set; }

		public DbSet<Widok> Widok { get; set; }

		public DbSet<Slownik> Slownik { get; set; }

		public DbSet<Eksperyment> Eksperyment { get; set; }

		public DbSet<Emocja> Emocja { get; set; }

		public DbSet<Ankieta> Ankieta { get; set; }

		public DbSet<EmocjaPodstawowa> EmocjaPodstawowa { get; set; }

		public DbSet<EksperymentLokalny> EksperymentLokalny { get; set; }

		public DbSet<PrzedmiotBadania> PrzedmiotBadania { get; set; }

		public DbSet<Wymiar> Wymiar { get; set; }

		public DbSet<Skala> Skala { get; set; }

		public DbSet<Pozycja> Pozycja { get; set; }

		public DbSet<JednostkaLeksykalna> JednostkaLeksykalna { get; set; }

		public DbSet<PrzykladUzyc> PrzykladUzyc { get; set; }

		public DbSet<JednostkaWSlowniku> JednostkaWSlowniku { get; set; }

		public DbSet<Odpowiedz> Odpowiedz { get; set; }

		public DbSet<WymiarWBadaniu> WymiarWBadaniu { get; set; }

		public DbSet<Ocena> Ocena { get; set; }

		public DbSet<BadaneEmocjeEksperyment> BadaneEmocjeEksperyment { get; set; }

		public DbSet<ZestawTreningowySlownik> ZestawTreningowySlownik { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {


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



			modelBuilder.Entity<JednostkaWSlowniku>()
		        .HasOne(t => t.Slownik)
		        .WithOne(t => t.Jednostki)
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



			modelBuilder.Entity<Ocena>()
		        .HasOne(t => t.Odpowiedz)
		        .WithOne(t => t.PrzedmiotBadaniaOcena)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<Ocena>()
		        .HasOne(t => t.PrzedmiotBadania)
		        .WithOne(t => t.OdpowiedzOcena)
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