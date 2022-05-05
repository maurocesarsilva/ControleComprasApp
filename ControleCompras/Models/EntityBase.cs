
using SQLite;

namespace ControleCompras.Models
{
	public class EntityBase
	{
		[PrimaryKey]
		public Guid Id { get; set; }

		public DateTime Date { get; set; }

		public EntityBase()
		{
			Date = DateTime.Now;
		}
	}
}
