using ControleCompras.Models;
using ControleCompras.Util;

namespace ControleCompras.Services
{
	public class AnalyzeService : IAnalyzeService
	{
		public AnalyzeService(INotaService notaService)
		{
			_notaService = notaService;
		}

		private INotaService _notaService { get; set; }


		public async Task<List<AnalyseNota>> Analyze(List<Product> products)
		{
			var listAnalyseNota = new List<AnalyseNota>();

			var productNames = products.Select(n => n.Name);

			var notas = await GetNotes(productNames);

			var grupSupermarket = notas.GroupBy(x => x.Supermarket);
			foreach (var grupotNota in grupSupermarket)
			{
				var nota = grupotNota.ToList();
				RemoveNonSelectedProducts(nota, productNames);
				AddProductsToAnalysisList(nota, grupotNota, listAnalyseNota);
			}

			return listAnalyseNota;
		}

		private async Task<IEnumerable<Nota>> GetNotes(IEnumerable<string> productNames)
		{
			var notas = await _notaService.GetByProducts(productNames);
			if (notas.Any() is false) throw new Exception(String.Format(Msg.NotFound, "Nota"));

			return notas;
		}

		private void RemoveNonSelectedProducts(List<Nota> nota, IEnumerable<string> productNames)
		{
			nota.ForEach(x => x.NotaItens.RemoveAll(r => productNames.Contains(r.Product) is false));
		}

		private void AddProductsToAnalysisList(List<Nota> nota, IGrouping<string, Nota> grupotNota, List<AnalyseNota> listAnalyseNota)
		{
			var analyseNota = new AnalyseNota { Supermarket = grupotNota.Key, AnalyseProduct = new List<AnalyseProduct>() };

			foreach (var item in nota.OrderByDescending(o => o.Date))
			{
				foreach (var notaItem in item.NotaItens)
				{
					if (listAnalyseNota.Where(w => w.Supermarket == grupotNota.Key).SelectMany(s => s.AnalyseProduct).Any(a => a.Product == notaItem.Product) is false)
					{
						var analyseProduct = new AnalyseProduct();
						analyseProduct.Product = notaItem.Product;
						analyseProduct.Value = notaItem.ValorUnitario;

						if (analyseNota.AnalyseProduct.Any(a => a.Product == notaItem.Product) is false)
						{
							analyseNota.AnalyseProduct.Add(analyseProduct);
						}
					}
				}
			}

			if (analyseNota.AnalyseProduct.Any())
				listAnalyseNota.Add(analyseNota);
		}
	}
}
