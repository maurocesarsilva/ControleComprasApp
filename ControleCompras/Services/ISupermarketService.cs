using ControleCompras.Models;

namespace ControleCompras.Services
{
	public interface ISupermarketService
	{
		Task<IEnumerable<Supermarket>> Get();

		Task<Supermarket> Get(Guid id);

		Task Save(Supermarket supermercados);

		Task Delete(Guid id);
	}
}
