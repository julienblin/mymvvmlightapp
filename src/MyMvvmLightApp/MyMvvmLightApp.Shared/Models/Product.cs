using System;

namespace MyMvvmLightApp.Models
{
	public class Product
	{
		public string Identifier { get; set; }

		public string Name { get; set; }

		public decimal Price { get; set; }

		public override string ToString() => $"[Identifier: '{Identifier}', Name: '{Name}', Price: '{Price}']";
	}
}
