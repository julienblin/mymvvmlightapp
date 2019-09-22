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
	public class ProductViewModel : ViewModelBase
	{
		private readonly IProductsService _productsService;

		public Product Product { get; set; }
		public Store Store { get; set; }

		public ICommand UpdateProductCommand { get; set; }
		public ICommand DeleteProductCommand { get; set; }

		public ProductViewModel(IServiceProvider serviceProvider, Store store, Product product)
		{
			_productsService = serviceProvider.GetRequiredService<IProductsService>();

			Store = store;
			Product = product;

			UpdateProductCommand = new AsyncCommand(UpdateProduct);
			DeleteProductCommand = new AsyncCommand(DeleteProduct);
		}

		private async Task UpdateProduct()
		{
			Product.Price += 100;

			await Product.Save(_productsService);
		}

		private async Task DeleteProduct()
		{
			await Product.Delete(Store, _productsService);
		}
	}
}
