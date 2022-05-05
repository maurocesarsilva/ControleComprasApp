using ControleCompras.Models;
using ControleCompras.Repository;
using ControleCompras.Util;

namespace ControleCompras.Services;

public class NotaService : INotaService
{
	private readonly INotaRepository _notaRepository;

	public NotaService(INotaRepository notaRepository)
	{
		_notaRepository = notaRepository;
	}

	public async Task Insert(Nota nota)
	{
		if (nota.Id == default)
		{
			nota.Id = Guid.NewGuid();
			var notaResult = await _notaRepository.GetByDescription(nota.Description);

			if (notaResult is not null) throw new Exception(String.Format(Msg.ExisteRegister, "Descrição"));

			await _notaRepository.Insert(nota);
		}
		else
		{
			await _notaRepository.Update(nota);
		}
	}

	public async Task<IEnumerable<Nota>> Get()
	{
		return await _notaRepository.Get();
	}

	public async Task Delete(Guid id)
	{
		await _notaRepository.Delete(id);
	}

	public async Task<IEnumerable<Nota>> GetByProducts(IEnumerable<string> products)
	{
		return await _notaRepository.GetByProducts(products);
	}
}
