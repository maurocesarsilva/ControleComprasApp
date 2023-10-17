using ControleCompras.Models;
using ControleCompras.Repository.Config;

namespace ControleCompras.Repository
{
	public class ProductRepository : SQLiteConfig<Product>, IProductRepository
	{
		public async Task<Product> GetByName(string nome)
		{
			await Task.CompletedTask;

			return Database.Table<Product>().FirstOrDefault(f => f.Name == nome);
		}
	}
}
