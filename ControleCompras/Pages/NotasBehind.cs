using ControleCompras.Models;
using ControleCompras.Services;
using ControleCompras.Shared.Componentes;
using ControleCompras.Util;
using Microsoft.AspNetCore.Components;

namespace ControleCompras.Pages
{
	public class NotasBehind : ComponentBase
	{
		[Inject]
		private INotaService _notaService { get; set; }

		protected Alert Alert { get; set; }

		protected Modal ModalNota { get; set; }

		protected Modal ModalDelete { get; set; }

		protected Nota Nota { get; set; }

		protected IEnumerable<Nota> ListNotas;

		protected List<NotaItens> ListNotaItens { get; set; }

		protected override void OnInitialized()
		{
			if (Alert is not null) Alert.CloseMessage();

			ListNotas = new List<Nota>();
			ListNotaItens = new();
			Nota = new();
		}

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			try
			{
				if (firstRender is false) return;

				ListNotas = await _notaService.Get();
				StateHasChanged();
			}
			catch (Exception ex)
			{
				Alert.ShowErrorMessage(ex.Message);
			}
		}

		protected void OpenModal()
		{
			Nota = new();
			ModalNota.Open();
		}

		protected void ConfirmDelete(Nota nota)
		{
			Nota = nota;
			ModalDelete.Open();
		}
		
		protected void Show(Nota nota)
		{
			Nota = nota;
			ListNotaItens = nota.NotaItens.ToList();

			ModalNota.Open();
		}

		protected async Task Deletar(Guid id)
		{
			try
			{
				await _notaService.Delete(id);
				await Reload();
				ModalDelete.Close();
				Alert.ShowSuccessMessage(Msg.Delete);
			}
			catch (Exception ex)
			{
				Alert.ShowErrorMessage(ex.Message);
			}
		}

		public async Task Reload()
		{
			ListNotas = await _notaService.Get();
			StateHasChanged();
			Nota = new();
		}
	}
}
