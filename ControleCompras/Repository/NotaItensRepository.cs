using ControleCompras.Models;
using ControleCompras.Repository.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCompras.Repository
{
	public class NotaItensRepository : SQLiteConfig<NotaItens>, INotaItensRepository
	{
		public async Task<IEnumerable<NotaItens>> GetByProducts(IEnumerable<string> products)
		{
			await Task.CompletedTask;
			return Database.Table<NotaItens>().Where(x => products.Contains(x.Product));
		}

		public async Task<IEnumerable<NotaItens>> GetNotaItensByNota(IEnumerable<Guid> notaId)
		{
			await Task.CompletedTask;
			return Database.Table<NotaItens>().Where(x => notaId.Contains(x.NotaId));
		}

		public async Task DeletarNotaItensByNota(Guid notaId)
		{
			await Task.CompletedTask;
			Database.Execute($"DELETE FROM NotaItens WHERE NotaId = @notaId ", notaId );
		}

	}
}
