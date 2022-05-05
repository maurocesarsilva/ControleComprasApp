using ControleCompras.Models;
using ControleCompras.Repository.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCompras.Repository
{
	public interface INotaItensRepository : ISQLiteConfig<NotaItens>
	{
		Task<IEnumerable<NotaItens>> GetNotaItensByNota(IEnumerable<Guid> notaId);
		Task<IEnumerable<NotaItens>> GetByProducts(IEnumerable<string> products);

		Task DeletarNotaItensByNota(Guid notaId);
	}
}
