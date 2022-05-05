
namespace ControleCompras.Models
{
	public class EntityBase
	{
		public Guid Id { get; set; }

		public DateTime Date { get; set; }

		public EntityBase()
		{
			Date = DateTime.Now;
			Id = Guid.NewGuid();
		}
	}
}
