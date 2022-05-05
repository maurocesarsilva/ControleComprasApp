using ControleCompras.Models;
using ControleCompras.Repository;
using ControleCompras.Util;

namespace ControleCompras.Services
{
	public class SupermarketService : ISupermarketService
	{

		private readonly ISupermarketRepository _supermercadoRepository;

		public SupermarketService(ISupermarketRepository supermercadoRepository)
		{
			_supermercadoRepository = supermercadoRepository;
		}

		public async Task<IEnumerable<Supermarket>> Get()
		{
			return (await _supermercadoRepository.Get()).OrderBy(o => o.Name);
		}

		public async Task<Supermarket> Get(Guid id)
		{
			return await _supermercadoRepository.Get(id);
		}

		public async Task Delete(Guid id)
		{
			await _supermercadoRepository.Delete(id);
		}

		public async Task Save(Supermarket supermarket)
		{
			if (supermarket.Id == default)
			{
				supermarket.Id = Guid.NewGuid();
				var supermarketResult = await _supermercadoRepository.GetByName(supermarket.Name);

				if (supermarketResult is not null) { throw new Exception(String.Format(Msg.ExisteRegister, "Supermercado")); }

				await _supermercadoRepository.Insert(supermarket);
			}
			else
			{
				await _supermercadoRepository.Update(supermarket);
			}
		}
	}
}
