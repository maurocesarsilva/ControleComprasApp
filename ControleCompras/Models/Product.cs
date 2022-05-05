using System.ComponentModel.DataAnnotations;

namespace ControleCompras.Models
{
	public class Product : EntityBase
	{
		[Required(ErrorMessage ="Nome Obrigatório")]
		[MaxLength(100, ErrorMessage ="Campo {0} deve possuir no maximo 20 caracteres")]
		public string Name { get; set; }
	}
}
