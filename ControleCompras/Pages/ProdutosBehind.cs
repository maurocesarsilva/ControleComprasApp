using ControleCompras.Repository;
using ControleCompras.Services;
using ControleCompras.Shared.Componentes;
using ControleCompras.Util;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace ControleCompras.Pages
{
	public class ProdutosBehind : ComponentBase
	{
		[Inject]
		private IProductService _productService { get; set; }

		protected IEnumerable<Models.Product> ListProducts;

		protected Models.Product Product;

		protected string Title { get; set; }
		protected Alert Alert { get; set; }
		protected Modal Modal { get; set; }
		protected Modal ModalDelete { get; set; }

		protected override async Task OnInitializedAsync()
		{
			ListProducts = new List<Models.Product>();
			await Reload();
			if (Alert is not null) Alert.CloseMessage();
		}

		protected async Task Get()
		{
			ListProducts = await _productService.Get();
		}
		protected async Task Save(EditContext e)
		{
			try
			{
				var product = e.Model as Models.Product;

				await _productService.Save(product);
				Alert.ShowSuccessMessage(Msg.Save);
				await Reload();
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

		protected void ConfirmDelete(Models.Product product)
		{
			Product = product;
			ModalDelete.Open();
		}

		protected async Task Deletar(Guid id)
		{
			try
			{
				await _productService.Delete(id);
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
				Product = new();
				Title = "Cadastrar Novo Produto";
				Modal.Open();
			}
			catch (Exception ex)
			{
				Alert.ShowErrorMessage(ex.Message);
			}
		}

		protected void UpdateObjectToEdit(Models.Product produtos)
		{
			Product = produtos;
			Title = "Editar Produto";
			Modal.Open();
		}
		private async Task Reload()
		{
			Product = new();
			await Get();
		}
	}
}
