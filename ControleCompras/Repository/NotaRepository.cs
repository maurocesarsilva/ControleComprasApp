using ControleCompras.Models;
using ControleCompras.Repository.Config;

namespace ControleCompras.Repository;

public class NotaRepository : SQLiteConfig<Nota>, INotaRepository
{
	public async Task<Nota> GetByDescription(string description)
	{
		await Task.CompletedTask;
		return Database.Table<Nota>().FirstOrDefault(f => f.Description == description);
	}

	public async Task<IEnumerable<Nota>> GetByProducts(IEnumerable<string> products)
	{
		await Task.CompletedTask;
		return Database.Table<Nota>().ToList().Where(w => w.NotaItens.Where(n => products.Contains(n.Product)).Any());
	}
}
