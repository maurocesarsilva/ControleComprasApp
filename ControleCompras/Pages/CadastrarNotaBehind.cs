using ControleCompras.Models;
using ControleCompras.Repository;
using ControleCompras.Services;
using ControleCompras.Shared.Componentes;
using ControleCompras.Util;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace ControleCompras.Pages
{
	public class CadastrarNotaBehind : ComponentBase
	{
		[Inject]
		private IProductService _productService { get; set; }

		[Inject]
		private ISupermarketService _supermarketService { get; set; }

		[Inject]
		private INotaService _notaService { get; set; }

		[Parameter]
		public Modal ModalEdicao { get; set; }

		[Parameter]
		public Alert AlertEdicao { get; set; }

		[Parameter]
		public Nota Nota { get; set; }

		[Parameter]
		public EventCallback ReloadEvent { get; set; }

		protected Alert Alert { get; set; }

		protected Modal Modal { get; set; }

		protected Modal ModalSupermarket { get; set; }
		protected Modal ModalProduct { get; set; }

		protected NotaItens NotaItens;

		protected IEnumerable<Product> ListProducts;

		protected IEnumerable<Supermarket> ListSupermarket;

		protected override async Task OnInitializedAsync()
		{
			if (Alert is not null) Alert.CloseMessage();

			ListProducts = new List<Product>();
			ListSupermarket = new List<Supermarket>();
			Nota ??= new();
			NotaItens = new();
		}

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			try
			{
				await LoadInitial();
				StateHasChanged();
			}
			catch (Exception ex)
			{
				Alert.ShowErrorMessage(ex.Message);
			}
		}

		private async Task LoadInitial()
		{
			ListProducts = await _productService.Get();
			ListSupermarket = await _supermarketService.Get();
		}

		protected void Save(EditContext e)
		{
			try
			{
				var nota = e.Model as NotaItens;

				if (nota is null) throw new Exception(Msg.SaveErro);

				var notaFind = Nota.NotaItens.FirstOrDefault(x => x.Product == nota.Product || x.Valor == nota.Valor);

				if (notaFind is not null) throw new Exception(String.Format(Msg.ExisteRegister, "Produto"));

				Nota.NotaItens.Add(nota);
			}
			catch (Exception ex)
			{
				Alert.ShowErrorMessage(ex.Message);
			}
			finally
			{
				Modal.Close();
			}
		}

		protected void Clear()
		{
			Nota = new();
			Modal.Close();
			StateHasChanged();
		}

		protected void RemoveList(NotaItens nota)
		{
			Nota.NotaItens.RemoveAll(x => x.Product == nota.Product && x.Valor == nota.Valor);
			StateHasChanged();
		}

		protected void PrepareModalToSave()
		{
			try
			{
				NotaItens = new();
				Modal.Open();
			}
			catch (Exception ex)
			{
				Alert.ShowErrorMessage(ex.Message);
			}
		}

		protected async void SaveNota(EditContext e)
		{
			try
			{
				var nota = e.Model as Nota;

				if (Nota.NotaItens.Any() is false) throw new Exception(Msg.NotInclude);
				if (string.IsNullOrWhiteSpace(nota.Description)) throw new Exception(String.Format(Msg.ExisteRegister, "Descrição"));

				nota.NotaItens = Nota.NotaItens;

				await _notaService.InsertOrUpdate(nota);
				Nota = new();
				await ReloadEvent.InvokeAsync();
				AlertEdicao.ShowSuccessMessage(Msg.Save);

				Modal.Close();
				if (ModalEdicao is not null) ModalEdicao.Close();
			}
			catch (Exception ex)
			{
				Alert.ShowErrorMessage(ex.Message);
			}
			finally
			{
				StateHasChanged();
			}
		}

		protected void OpenModalSupermarket()
		{
			ModalSupermarket.Open();
		}

		protected void OpenModalProduct()
		{
			ModalProduct.Open();
		}
	}
}
