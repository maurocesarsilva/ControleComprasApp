using ControleCompras.Models;
using SQLite;

namespace ControleCompras.Repository.Config
{
	public interface ISQLiteConfig<T> where T : EntityBase
	{
		public SQLiteConnection Database { get; set; }

		Task<IEnumerable<T>> Get();

		Task<T> Get(Guid id);

		Task Insert(T obj);

		Task InsertOrReplace(T obj);

		Task InsertMany(IEnumerable<T> obj);

		Task Update(T obj);

		Task UpdateMany(IEnumerable<T> obj);

		Task Delete(Guid id);
	}
}
