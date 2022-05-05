using ControleCompras.Models;
using ControleCompras.Repository;
using ControleCompras.Util;

namespace ControleCompras.Services
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;
		public ProductService(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task Delete(Guid id)
		{
			await _productRepository.Delete(id);
		}

		public async Task<IEnumerable<Product>> Get()
		{
			return (await _productRepository.Get()).OrderBy(o => o.Name);
		}

		public async Task<Product> Get(Guid id)
		{
			return await _productRepository.Get(id);
		}

		public async Task Save(Product product)
		{
			if (product.Id == default)
			{
				var supermarketResult = await _productRepository.GetByName(product.Name);

				if (supermarketResult is not null) { throw new Exception(String.Format(Msg.ExisteRegister, "Supermercado")); }

				await _productRepository.Insert(product);
			}
			else
			{
				await _productRepository.Update(product);
			}
		}
	}
}
