using ControleCompras.Models;
using SQLite;

namespace ControleCompras.Repository.Config
{
	public abstract class SQLiteConfig<T> : ISQLiteConfig<T> where T : EntityBase, new() 
	{
		public SQLiteConnection Database { get; set; }

		public SQLiteConfig()
		{
			var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "controleCompras.db");
			Database = new SQLiteConnection(dbPath);
			Database.CreateTable<T>();
		}

		public async Task<IEnumerable<T>> Get()
		{
			await Task.CompletedTask;
			return Database.Table<T>().ToList();
		}

		public async Task<T> Get(Guid id) 
		{
			await Task.CompletedTask;
			return Database.Table<T>().FirstOrDefault(f => f.Id == id);
		}

		public async Task Insert(T obj)
		{
			await Task.CompletedTask;
			Database.Insert(obj);
		}

		public async Task InsertOrReplace(T obj)
		{
			await Task.CompletedTask;
			Database.InsertOrReplace(obj);
		}

		public async Task InsertMany(IEnumerable<T> obj)
		{
			await Task.CompletedTask;
			Database.InsertAll(obj);
		}

		public async Task Update(T obj)
		{
			await Task.CompletedTask;
			Database.Update(obj);
		}

		public async Task UpdateMany(IEnumerable<T> obj)
		{
			await Task.CompletedTask;
			Database.UpdateAll(obj);
		}

		public async Task Delete(Guid id)
		{
			await Task.CompletedTask;
			var nameTable = typeof(T).Name;
			Database.Execute($"DELETE FROM {nameTable} WHERE Id = @id ", id);
		}
	}
}
