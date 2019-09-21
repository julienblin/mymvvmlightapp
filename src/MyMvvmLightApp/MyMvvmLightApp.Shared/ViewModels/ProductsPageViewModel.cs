using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using AsyncAwaitBestPractices.MVVM;
using GalaSoft.MvvmLight;
using Microsoft.Extensions.Logging;
using MyMvvmLightApp.Models;
using MyMvvmLightApp.Services;

namespace MyMvvmLightApp.ViewModels
{
	public class ProductsPageViewModel : ViewModelBase
	{
		private ILogger<ProductsPageViewModel> _logger;
		private IProductsService _productsService;

		private string _productName;
		public string ProductName
		{
			get => _productName;
			set { Set(ref _productName, value); UpdateProductStatus(); }
		}

		private string _productPrice;
		public string ProductPrice
		{
			get => _productPrice;
			set { Set(ref _productPrice, value); UpdateProductStatus(); }
		}

		private string _productStatus;
		public string ProductStatus
		{
			get => _productStatus;
			set => Set(ref _productStatus, value);
		}

		public ProductsPageViewModel(ILogger<ProductsPageViewModel> logger, IProductsService productsService)
		{
			_logger = logger;
			_productsService = productsService;

			CreateProductCommand = new AsyncCommand(CreateProduct);

			UpdateProductStatus();
		}

		public ICommand CreateProductCommand { get; }

		private async Task<Product[]> GetProducts(CancellationToken ct)
		{
			return await _productsService.GetAll(ct);
		}

		private async Task CreateProduct()
		{
			var productName = ProductName?.Trim();
			var productPriceString = ProductPrice?.Trim();

			if (productName?.Length == 0
				|| !decimal.TryParse(productPriceString, out var productPrice)
				|| productPrice < 0)
			{
				return;
			}

			var product = new Product
			{
				Name = productName,
				Price = productPrice
			};

			_logger.LogDebug($"Creating product {product}.");

			await _productsService.Create(CancellationToken.None, product);

			ProductName = string.Empty;
			ProductPrice = string.Empty;

			_logger.LogInformation($"Created product {product}.");
		}

		private void UpdateProductStatus()
		{
			var productName = ProductName?.Trim();

			ProductStatus = productName?.Length > 0
				? $"A product with the name '{productName}' will be created."
				: "Fill the form below to create a new product.";
		}
	}
}
