using ControleCompras.Util;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleCompras.Models
{
	public class Nota : EntityBase
	{
		public Nota()
		{
			NotaItens ??= new();
		}

		[Required(ErrorMessageResourceType = typeof(Msg), ErrorMessageResourceName = ErrorMessageConstant.Required)]
		[MaxLength(100, ErrorMessageResourceType = typeof(Msg), ErrorMessageResourceName = ErrorMessageConstant.NumMaxChar)]
		public string Description { get; set; }

		[Required(ErrorMessageResourceType = typeof(Msg), ErrorMessageResourceName = ErrorMessageConstant.Required)]
		public string Supermarket { get; set; }

		[SQLite.Ignore] 
		public List<NotaItens> NotaItens { get; set; }
	}

	public class NotaItens : EntityBase
	{
		[Required(ErrorMessageResourceType = typeof(Msg), ErrorMessageResourceName = ErrorMessageConstant.Required)]
		public string Product { get; set; }

		[Required(ErrorMessageResourceType = typeof(Msg), ErrorMessageResourceName = ErrorMessageConstant.Required)]
		[Range(0.01, int.MaxValue, ErrorMessageResourceType = typeof(Msg), ErrorMessageResourceName = ErrorMessageConstant.ValueInvalid)]
		public decimal Quantity { get; set; }

		public decimal Valor { get => ValorUnitario * Quantity; }

		[Required(ErrorMessageResourceType = typeof(Msg), ErrorMessageResourceName = ErrorMessageConstant.Required)]
		[Range(0.01, int.MaxValue, ErrorMessageResourceType = typeof(Msg), ErrorMessageResourceName = ErrorMessageConstant.ValueInvalid)]
		public decimal ValorUnitario { get; set; }

		public Guid NotaId { get; set; }
	}
}
