namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;

	public class Point {

		public int ID {get; set;}

		public Nullable<Int32> X { get; set; }

		public Nullable<Int32> Y { get; set; }
	}
}