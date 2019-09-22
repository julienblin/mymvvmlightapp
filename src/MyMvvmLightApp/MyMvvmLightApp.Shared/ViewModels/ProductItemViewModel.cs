using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using AsyncAwaitBestPractices.MVVM;
using GalaSoft.MvvmLight;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyMvvmLightApp.Models;
using MyMvvmLightApp.Services;

namespace MyMvvmLightApp.ViewModels
{
	public class ProductItemViewModel : ViewModelBase
	{
		private readonly IProductsService _productsService;

		public Product Product { get; set; }
		public ICommand UpdateProductCommand { get; set; }
		public ICommand DeleteProductCommand { get; set; }

		public ProductItemViewModel(IServiceProvider serviceProvider, Product product)
		{
			_productsService = serviceProvider.GetRequiredService<IProductsService>();

			Product = product;

			UpdateProductCommand = new AsyncCommand(UpdateProduct);
			DeleteProductCommand = new AsyncCommand(DeleteProduct);
		}

		private async Task UpdateProduct()
		{
			var updatedProduct = new Product
			{
				Identifier = Product.Identifier,
				Name = Product.Name,
				Price = Product.Price + 100
			};

			await _productsService.Update(CancellationToken.None, Product, updatedProduct);
		}

		private async Task DeleteProduct()
		{
			await _productsService.Delete(CancellationToken.None, Product);
		}
	}
}
