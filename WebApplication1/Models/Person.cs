namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public abstract class Person {

		public int ID { get; set; }


		public System.DateTime DateOfBirth { get; set; }

		[Required]
		public System.String Name { get; set; }

		public void DoSomething(System.Int32 x,System.Int32 y) {
			throw new NotImplementedException();
		}
	}
}