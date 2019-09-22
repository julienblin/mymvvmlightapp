using System;
using System.Threading;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using MyMvvmLightApp.Services;

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

		public async Task<Product> Save(Store store, IProductsService productsService)
		{
			Name = Name?.Trim();
			Price = Price?.Trim();

			if (Name?.Length == 0
				|| !decimal.TryParse(Price, out var productPrice)
				|| productPrice < 0)
			{
				throw new InvalidOperationException("Invalid price.");
			}

			if (Identifier == null)
			{
				var createdProduct = await productsService.Create(CancellationToken.None, this);

				store.Products.Add(createdProduct);

				return createdProduct;
			}

			return await productsService.Update(CancellationToken.None, this, this);
		}

		public async Task Delete(Store store, IProductsService productsService)
		{
			await productsService.Delete(CancellationToken.None, this);

			store.Products.Remove(this);
		}

		public override string ToString() => $"[Identifier: '{Identifier}', Name: '{Name}', Price: '{Price}']";
	}
}
