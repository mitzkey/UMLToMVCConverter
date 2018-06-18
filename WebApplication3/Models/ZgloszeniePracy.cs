namespace WebApplication3.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class ZgloszeniePracy {



		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public System.Int32 Numer { get; set; }

		public System.DateTime DataZgloszenia { get; set; }

		[Required]
		public StatusZgloszenia Status { get; set; }		

		[ForeignKey("StatusZgloszenia")]
        public int StatusID { get; set; }
		

		public System.DateTime DataObrony { get; set; }

		[Required]
		public System.String Tytul { get; set; }

		[Required]
		public System.String ObszarBadan { get; set; }

		public virtual ICollection<SlowaKluczowe> SlowaKluczowe { get; set; }

		[Required]
		public System.String NajwiekszeOsiagnieciaWlasneWPracy { get; set; }

		[Required]
		public System.String ElementyUzyteczneDlaNaukiPraktyki { get; set; }

		[Required]
		public System.String KierunekDalszychPrac { get; set; }

		[Required]
		public System.String PowodOdrzucenia { get; set; }

		public System.DateTime DataPrzekazaniaInformacjiOOdrzuceniu { get; set; }

		[NotMapped]
		public System.Boolean Krytyczna { get { throw new NotImplementedException(); } private set {} }

		public System.Double SredniaOcenaRecenzentow { get; set; }

		public System.Double SredniaOcenaKomisji { get; set; }

		[InverseProperty("Praca")]
		[ForeignKey("UczelniaNazwaKwalifikowana")]
		public virtual JednostkaNaukowa Uczelnia { get; set; }

		[Required]
		public System.String UczelniaNazwaKwalifikowana { get; set; }

		[InverseProperty("NadzorowanePrace")]
		[ForeignKey("PromotorID")]
		public virtual Ekspert Promotor { get; set; }

		[Required]
		public Nullable<System.Int32> PromotorID { get; set; }

		[InverseProperty("ZgloszonePrace")]
		[ForeignKey("EdycjaNumer")]
		public virtual EdycjaKonkursu Edycja { get; set; }

		[Required]
		public System.Int32 EdycjaNumer { get; set; }

		[InverseProperty("Prace")]
		[ForeignKey("NagrodaID")]
		public virtual Nagroda Nagroda { get; set; }

		[Required]
		public Nullable<System.Int32> NagrodaID { get; set; }

		[InverseProperty("ZgloszeniePracy")]
		public virtual ICollection<Propozycja> ProponowaniRecenzenci { get; set; }

		[InverseProperty("ZgloszeniePracy")]
		public virtual ICollection<Recenzja> Recenzenci { get; set; }

		[InverseProperty("Praca")]
		public virtual ICollection<AutorzyPraca> Autorzy { get; set; }
	}
}