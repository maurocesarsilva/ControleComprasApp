namespace ControleCompras.Models
{
	public class AnalyseNota
	{
		public string Supermarket { get; set; }
		public decimal ValorNota { get => AnalyseProduct.Sum(s => s.Value); }
		public List<AnalyseProduct> AnalyseProduct { get; set; }
	}
}
