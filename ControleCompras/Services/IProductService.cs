using ControleCompras.Models;

namespace ControleCompras.Services
{
	public interface IProductService
	{
		Task<IEnumerable<Product>> Get();

		Task<Product> Get(Guid id);

		Task Save(Product supermercados);

		Task Delete(Guid id);
	}
}
