using System;
using System.Threading;
using System.Threading.Tasks;
using MyMvvmLightApp.Models;

namespace MyMvvmLightApp.Services
{
	public interface IProductsService
	{
		/// <summary>
		/// Creates a new product.
		/// </summary>
		/// <param name="ct">Cancellation token</param>
		/// <param name="product">Product to create</param>
		/// <returns>Created product</returns>
		Task<Product> Create(CancellationToken ct, Product product);

		/// <summary>
		/// Deletes the specified product.
		/// </summary>
		/// <param name="ct">Cancellation token</param>
		/// <param name="product">Product to delete</param>
		/// <returns>Task</returns>
		Task Delete(CancellationToken ct, Product product);

		/// <summary>
		/// Gets the list of products
		/// </summary>
		/// <param name="ct">Cancellation token</param>
		/// <returns>All products</returns>
		Task<Product[]> GetAll(CancellationToken ct);

		/// <summary>
		/// Gets a product of the specified identifier.
		/// </summary>
		/// <param name="ct">Cancellation token</param>
		/// <returns>Product</returns>
		Task<Product> Get(CancellationToken ct, string identifier);

		/// <summary>
		/// Updates an existing product.
		/// </summary>
		/// <param name="ct">Cancellation token</param>
		/// <param name="product">Current product</param>
		/// <param name="updatedProduct">Updated product</param>
		/// <returns>Updated product</returns>
		Task<Product> Update(CancellationToken ct, Product product, Product updatedProduct);
	}
}