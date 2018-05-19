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

		public Nullable<System.DateTime> DateOfBirth { get; set; }

		public System.String Name { get; set; }

		public void DoSomething(Nullable<System.Int32> x,Nullable<System.Int32> y) {
			throw new NotImplementedException();
		}
	}
}