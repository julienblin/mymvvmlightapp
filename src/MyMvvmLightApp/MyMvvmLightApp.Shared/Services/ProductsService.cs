using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyMvvmLightApp.Models;

namespace MyMvvmLightApp.Services
{
	public class ProductsService : IProductsService
	{
		private readonly ILogger<ProductsService> _logger;
		private readonly HttpClient _httpClient;

		public ProductsService(ILogger<ProductsService> logger, IHttpClientFactory httpClientFactory)
		{
			_logger = logger;
			_httpClient = httpClientFactory.CreateClient("MyHost");
		}

		public Task<Product> Create(CancellationToken ct, Product product)
		{
			throw new NotImplementedException();
		}

		public Task Delete(CancellationToken ct, Product product)
		{
			throw new NotImplementedException();
		}

		public Task<Product> Get(CancellationToken ct, string identifier)
		{
			throw new NotImplementedException();
		}

		public Task<Product[]> GetAll(CancellationToken ct)
		{
			throw new NotImplementedException();
		}

		public Task<Product> Update(CancellationToken ct, Product product, Product updatedProduct)
		{
			throw new NotImplementedException();
		}
	}

	public class MockProductsService : IProductsService
	{
		private readonly ILogger<ProductsService> _logger;
		private readonly List<Product> _products;

		public MockProductsService(ILogger<ProductsService> logger)
		{
			_logger = logger;
			_products = new List<Product>();
		}

		public async Task<Product> Create(CancellationToken ct, Product product)
		{
			_products.Add(product);

			return product;
		}

		public async Task Delete(CancellationToken ct, Product product)
		{
			_products.Remove(product);
		}

		public async Task<Product> Get(CancellationToken ct, string identifier)
		{
			return _products.SingleOrDefault(p => p.Identifier == identifier);
		}

		public async Task<Product[]> GetAll(CancellationToken ct)
		{
			return _products.ToArray();
		}

		public async Task<Product> Update(CancellationToken ct, Product product, Product updatedProduct)
		{
			var productFromList = _products.Single(p => p.Identifier == product.Identifier);

			productFromList.Name = updatedProduct.Name;
			productFromList.Price = updatedProduct.Price;

			return productFromList;
		}
	}
}
