using ControleCompras.Models;
using ControleCompras.Repository;
using ControleCompras.Services;
using ControleCompras.Shared.Componentes;
using ControleCompras.Util;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;

namespace ControleCompras.Pages
{
	public class CadastrarListaBehind : ComponentBase
	{
		[Inject]
		private IProductService _productService { get; set; }

		[Inject]
		private IAnalyzeService _analyzeService { get; set; }

		protected Alert Alert { get; set; }
		protected List<Product> ListProducts { get; set; }
		protected List<Product> ListProductsTabela { get; set; }
		protected List<Product> ListProductSelect { get; set; }
		protected List<AnalyseNota> ListAnalyse { get; set; }
		protected string TextSearch { get; set; }
		protected string BestPurchaseOption { get; set; }

		protected override async Task OnInitializedAsync()
		{
			if (Alert is not null) Alert.CloseMessage();
			ListProducts = new List<Product>();
			ListProductsTabela = new List<Product>();
			ListProductSelect = new List<Product>();
			ListAnalyse = new List<AnalyseNota>();
		}
		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			try
			{
				if (firstRender is false) return;

				await LoadInitial();
			}
			catch (Exception ex)
			{
				Alert.ShowErrorMessage(ex.Message);
			}
		}

		private async Task LoadInitial()
		{
			ListProducts = (await _productService.Get())?.ToList();
			ListProductsTabela = ListProducts;
			StateHasChanged();
		}

		protected void ChackSelectEvent(Product product, ChangeEventArgs args)
		{
			if (Convert.ToBoolean(args.Value))
			{
				ListProductSelect.Add(product);
			}
			else
			{
				ListProductSelect.Remove(product);
			}
		}

		protected void Search()
		{
			if (TextSearch is null) return;

			ListProductsTabela = ListProducts.Where(s => s.Name.ToUpper().Contains(TextSearch.ToUpper())).ToList();
		}

		protected async void Analyze()
		{
			try
			{
				if (ListProductSelect.Any() is false) throw new Exception(Msg.NoRecordSelected);
				ListAnalyse = await _analyzeService.Analyze(ListProductSelect);
				BestPurchaseOption = ListAnalyse.FirstOrDefault(x => x.ValorNota == ListAnalyse.Min(m => m.ValorNota)).Supermarket;
				StateHasChanged();
			}
			catch (Exception ex)
			{
				Alert.ShowErrorMessage(ex.Message);
			}
		}

		protected string VerifyCheck(Product product)
		{
			return ListProductSelect.Contains(product) ? "checked" : null;
		}

		protected async Task Clear()
		{
			await LoadInitial();
			ListProductSelect.Clear();
			ListAnalyse.Clear();
			TextSearch = String.Empty;
			BestPurchaseOption = String.Empty;
		}
	}
}
