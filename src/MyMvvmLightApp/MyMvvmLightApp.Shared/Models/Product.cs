using System;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace MyMvvmLightApp.Models
{
	public class Product : ObservableObject
	{
		private string _identifier;
		public string Identifier
		{
			get => _identifier;
			set { Set(ref _identifier, value); }
		}

		private string _name;
		public string Name
		{
			get => _name;
			set { Set(ref _name, value); }
		}

		private string _price;
		public string Price
		{
			get => _price;
			set { Set(ref _price, value); }
		}

		public async Task Save()
		{
			Name = Name?.Trim();
			Price = Price?.Trim();

			if (Name?.Length == 0
				|| !decimal.TryParse(Price, out var productPrice)
				|| productPrice < 0)
			{
				throw new InvalidOperationException("Invalid price.");
			}
		}

		public override string ToString() => $"[Identifier: '{Identifier}', Name: '{Name}', Price: '{Price}']";
	}
}
