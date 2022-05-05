using ControleCompras.Models;
using ControleCompras.Repository;
using ControleCompras.Services;
using ControleCompras.Shared.Componentes;
using ControleCompras.Util;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace ControleCompras.Pages
{
	public class IndexBehind : ComponentBase
	{
		[Inject]
		private ISupermarketService _supermarketService { get; set; }

		protected IEnumerable<Supermarket> ListSupermarket;

		protected Supermarket Supermarket;

		protected string Title { get; set; }
		protected Alert Alert { get; set; }
		protected Modal ModalNewSupermarket { get; set; }
		protected Modal ModalDelete { get; set; }

		protected override async Task OnInitializedAsync()
		{
			ListSupermarket = new List<Supermarket>();
			await Reload();
			if (Alert is not null) Alert.CloseMessage();
		}

		protected async Task Get()
		{
			ListSupermarket = await _supermarketService.Get();
		}
		protected async Task Save(EditContext e)
		{
			try
			{
				var supermarket = e.Model as Supermarket;

				await _supermarketService.Save(supermarket);
				Alert.ShowSuccessMessage(Msg.Save);

				await Reload();

			}
			catch (Exception ex)
			{
				Alert.ShowErrorMessage(ex.Message);
			}
			finally
			{
				ModalNewSupermarket.Close();
			}
		}

		protected void ConfirmDelete(Supermarket supermarket)
		{
			Supermarket = supermarket;
			ModalDelete.Open();
		}
		protected async Task Delete(Guid id)
		{
			try
			{
				await _supermarketService.Delete(id);
				await Reload();
				ModalDelete.Close();
				Alert.ShowSuccessMessage(Msg.Delete);
			}
			catch (Exception ex)
			{
				Alert.ShowErrorMessage(ex.Message);
			}
			finally
			{
				ModalDelete.Close();
			}
}

		protected void PrepareModalToSave()
		{
			try
			{
				Supermarket = new();
				Title = "Cadastrar Novo Supermercado";
				ModalNewSupermarket.Open();
			}
			catch (Exception ex)
			{
				Alert.ShowErrorMessage(ex.Message);
			}
		}

		protected void UpdateObject(Supermarket supermercados)
		{
			Supermarket = supermercados;
			Title = "Editar Supermercado";
			ModalNewSupermarket.Open();
		}

		private async Task Reload()
		{
			Supermarket = new();
			await Get();
		}
	}
}
