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
			_logger.LogDebug($"Creating product '{product}'.");

			product.Identifier = Guid.NewGuid().ToString();

			_products.Add(product);

			_logger.LogInformation($"Created product '{product}'.");

			return product;
		}

		public async Task Delete(CancellationToken ct, Product product)
		{
			_logger.LogDebug($"Deleting product '{product}'.");

			_products.Remove(product);

			_logger.LogInformation($"Deleted product '{product}'.");
		}

		public async Task<Product> Get(CancellationToken ct, string identifier)
		{
			_logger.LogDebug($"Getting product with identifier '{identifier}'.");

			var product = _products.SingleOrDefault(p => p.Identifier == identifier);

			_logger.LogInformation($"Got product '{product}'.");

			return product;
		}

		public async Task<Product[]> GetAll(CancellationToken ct)
		{
			_logger.LogDebug($"Getting all products.");

			var products = _products.ToArray();

			_logger.LogInformation($"Got '{products.Length}' products.");

			return products;
		}

		public async Task<Product> Update(CancellationToken ct, Product product, Product updatedProduct)
		{
			_logger.LogDebug($"Updating product '{product}' to '{updatedProduct}'.");

			var productFromList = _products.Single(p => p.Identifier == product.Identifier);

			productFromList.Name = updatedProduct.Name;
			productFromList.Price = updatedProduct.Price;

			_logger.LogInformation($"Updated product '{product}'.");

			return productFromList;
		}
	}
}
