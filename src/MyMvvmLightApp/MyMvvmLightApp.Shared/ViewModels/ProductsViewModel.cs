using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using AsyncAwaitBestPractices.MVVM;
using GalaSoft.MvvmLight;
using Microsoft.Extensions.DependencyInjection;
using MyMvvmLightApp.Models;
using MyMvvmLightApp.Services;

namespace MyMvvmLightApp.ViewModels
{
	public class ProductsViewModel : ViewModelBase
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly IProductsService _productsService;

		private Product _product;
		public Product Product
		{
			get => _product;
			set { Set(ref _product, value); }
		}

		private string _productStatus;
		public string ProductStatus
		{
			get => _productStatus;
			set => Set(ref _productStatus, value);
		}

		private Store _store;
		public Store Store
		{
			get => _store;
			set { Set(ref _store, value); }
		}

		private ObservableCollection<ProductViewModel> _products;
		public ObservableCollection<ProductViewModel> Products
		{
			get => _products;
			set => Set(ref _products, value);
		}

		public ICommand CreateProductCommand { get; }

		public ProductsViewModel(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
			_productsService = serviceProvider.GetRequiredService<IProductsService>();

			_store = new Store();
			_product = new Product();
			_product.PropertyChanged += OnProductChanged;
			_store.Products.CollectionChanged += OnProductsChanged;

			CreateProductCommand = new AsyncCommand(CreateProduct);
		}

		private async Task<Product[]> GetProducts(CancellationToken ct)
		{
			return await _productsService.GetAll(ct);
		}

		private async Task CreateProduct()
		{
			await _product.Save(Store, _productsService);

			Product.Name = string.Empty;
			Product.Price = string.Empty;
		}

		private void OnProductChanged(object sender, PropertyChangedEventArgs e)
		{
			var productName = Product.Name?.Trim();

			ProductStatus = productName?.Length > 0
				? $"A product with the name '{productName}' will be created."
				: "Fill the form below to create a new product.";
		}

		private void OnProductsChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			// TODO To sync ViewModel collections, look here; ObservableViewModelCollection
		}
	}
}
