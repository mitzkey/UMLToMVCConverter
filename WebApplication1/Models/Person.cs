namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public abstract class Person {


		public int ID {get; set;}

		public Nullable<DateTime> DateOfBirth { get; set; }

		public String Name { get; set; }

		public void DoSomething(Nullable<Int32> x,Nullable<Int32> y) {
			throw new NotImplementedException();
		}
	}
}