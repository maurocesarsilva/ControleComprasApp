using ControleCompras.Models;

namespace ControleCompras.Services
{
	public interface IAnalyzeService
	{
		Task<List<AnalyseNota>> Analyze(List<Product> products);
	}
}
