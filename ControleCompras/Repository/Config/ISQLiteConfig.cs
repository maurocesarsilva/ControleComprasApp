using ControleCompras.Models;

namespace ControleCompras.Repository.Config
{
	public interface ISQLiteConfig<T> where T : EntityBase
	{
		Task<IEnumerable<T>> Get();

		Task<T> Get(Guid id);

		Task Insert(T obj);

		Task Update(T obj);

		Task Delete(Guid id);
	}
}
