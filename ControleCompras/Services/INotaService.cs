using ControleCompras.Models;

namespace ControleCompras.Services;

public interface INotaService
{
	Task InsertOrUpdate(Nota nota);
	Task<IEnumerable<Nota>> Get();
	Task Delete(Guid id);
	Task<IEnumerable<Nota>> GetByProducts(IEnumerable<string> products);
}
