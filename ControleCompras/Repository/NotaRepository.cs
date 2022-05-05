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

	public async Task<IEnumerable<Nota>> GetNota(IEnumerable<Guid> notaId)
	{
		await Task.CompletedTask;
		return Database.Table<Nota>().Where(f => notaId.Contains(f.Id));
	}
}
