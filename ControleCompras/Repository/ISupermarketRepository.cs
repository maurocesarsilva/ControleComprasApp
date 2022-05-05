using ControleCompras.Models;
using ControleCompras.Repository.Config;

namespace ControleCompras.Repository
{
	public interface ISupermarketRepository : ISQLiteConfig<Supermarket>
	{
		Task<Supermarket> GetByName(string nome);
	}
}
