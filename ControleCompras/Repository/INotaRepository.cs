using ControleCompras.Models;
using ControleCompras.Repository.Config;

namespace ControleCompras.Repository;

public interface INotaRepository : ISQLiteConfig<Nota>
{
	Task<Nota> GetByDescription(string description);
	Task<IEnumerable<Nota>> GetNota(IEnumerable<Guid> notaId);
}
