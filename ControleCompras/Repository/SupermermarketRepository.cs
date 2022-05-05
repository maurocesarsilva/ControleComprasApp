using ControleCompras.Models;
using ControleCompras.Repository.Config;

namespace ControleCompras.Repository
{
	public class SupermermarketRepository : SQLiteConfig<Supermarket>, ISupermarketRepository
	{
		public async Task<Supermarket> GetByName(string nome)
		{
			await Task.CompletedTask;
			return Database.Table<Supermarket>().FirstOrDefault(f => f.Name == nome);
		}
	}
}
