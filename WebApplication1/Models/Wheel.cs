namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;

	public class Wheel {

		public int ID {get; set;}

		public Nullable<Double> Perimeter { get; set; }

		public virtual Car Car { get; set; }
	}
}