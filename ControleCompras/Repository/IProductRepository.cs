using ControleCompras.Models;
using ControleCompras.Repository.Config;

namespace ControleCompras.Repository
{
	public interface IProductRepository : ISQLiteConfig<Product>
	{
		Task<Product> GetByName(string nome);
	}
}
