using ControleCompras.Models;
using ControleCompras.Repository;
using ControleCompras.Util;

namespace ControleCompras.Services;

public class NotaService : INotaService
{
	private readonly INotaRepository _notaRepository;
	private readonly INotaItensRepository _notaItensRepository;

	public NotaService(INotaRepository notaRepository, INotaItensRepository notaItensRepository)
	{
		_notaRepository = notaRepository;
		_notaItensRepository = notaItensRepository;
	}

	public async Task InsertOrUpdate(Nota nota)
	{
		if (nota.Id == default)
		{
			await Insert(nota);
		}
		else
		{
			await Update(nota);
		}
	}

	private async Task Insert(Nota nota)
	{
		nota.Id = Guid.NewGuid();
		var notaResult = await _notaRepository.GetByDescription(nota.Description);

		if (notaResult is not null) throw new Exception(String.Format(Msg.ExisteRegister, "Descrição"));

		await _notaRepository.Insert(nota);
		await AssociarIdNotaItens(nota);
	}

	private async Task Update(Nota nota)
	{
		await _notaRepository.Update(nota);
		await AssociarIdNotaItens(nota);
	}

	private async Task AssociarIdNotaItens(Nota nota)
	{
		foreach (var item in nota.NotaItens)
		{
			item.NotaId = nota.Id;
			item.Id = item.Id == default ? Guid.NewGuid() : item.Id;
			await _notaItensRepository.InsertOrReplace(item);
		}
	}

	public async Task<IEnumerable<Nota>> Get()
	{
		var notas = await _notaRepository.Get();

		if (notas.Any() is false) return notas;

		var notasItens = await _notaItensRepository.GetNotaItensByNota(notas.Select(s => s.Id));
		var mapNotaItens = GetMapNotaItens(notasItens);
		MapNotaItensToNota(mapNotaItens, notas);

		return notas;
	}

	public async Task Delete(Guid id)
	{
		await _notaItensRepository.DeletarNotaItensByNota(id);
		await _notaRepository.Delete(id);
	}

	public async Task<IEnumerable<Nota>> GetByProducts(IEnumerable<string> products)
	{
		var notasItens = await _notaItensRepository.GetByProducts(products);

		if (notasItens.Any() is false) new Nota();

		var mapNotaItens = GetMapNotaItens(notasItens);
		var notas = (await _notaRepository.GetNota(notasItens.Select(s => s.NotaId)))?.ToList();

		for (int i = 0; i < notas.Count(); i++)
		{
			if (mapNotaItens.ContainsKey(notas[i].Id))
			{
				notas[i].NotaItens = mapNotaItens[notas[i].Id];
			}
		}

		return notas;
	}
	private Dictionary<Guid, List<NotaItens>> GetMapNotaItens(IEnumerable<NotaItens> notasItens)
	{
		return notasItens.GroupBy(g => g.NotaId).ToDictionary(d => d.Key, d => d.ToList());
	}

	private void MapNotaItensToNota(Dictionary<Guid, List<NotaItens>> mapNotaItens, IEnumerable<Nota> notas)
	{
		foreach (var item in notas)
		{
			if (mapNotaItens.ContainsKey(item.Id))
			{
				item.NotaItens = mapNotaItens[item.Id];
			}
		}
	}
}
