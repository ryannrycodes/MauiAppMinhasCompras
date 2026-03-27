using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

    public ListaProduto()
    {
        InitializeComponent();
        lst_produtos.ItemsSource = lista;
    }

    protected async override void OnAppearing()
    {
        try
        {
            lista.Clear();

            var dados = await App.Db.GetAll();

            dados.ForEach(i => lista.Add(i));

            var categorias = dados.Select(p => p.Categoria)
                                  .Where(c => !string.IsNullOrEmpty(c))
                                  .Distinct()
                                  .ToList();

            picker_categoria.Items.Clear();
            categorias.ForEach(c => picker_categoria.Items.Add(c));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Views.NovoProduto());
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string q = e.NewTextValue;

            lst_produtos.IsRefreshing = true;

            lista.Clear();

            var resultado = await App.Db.Search(q ?? "");

            resultado.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
        finally
        {
            lst_produtos.IsRefreshing = false;
        }
    }

    private async void picker_categoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (picker_categoria.SelectedIndex == -1) return;

            string categoria = picker_categoria.SelectedItem.ToString();

            lista.Clear();

            var filtrados = await App.Db.GetByCategoria(categoria);

            filtrados.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);

        DisplayAlert("Total dos Produtos", $"Total: {soma:C}", "OK");
    }

    private async void ToolbarItem_Relatorio(object sender, EventArgs e)
    {
        var relatorio = await App.Db.RelatorioCategoria();

        string msg = "";

        foreach (var item in relatorio)
        {
            msg += $"{item.Categoria}: {item.Total:C}\n";
        }

        await DisplayAlert("Relatório por Categoria", msg, "OK");
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            MenuItem selecionado = sender as MenuItem;
            Produto p = selecionado.BindingContext as Produto;

            bool confirm = await DisplayAlert(
                "Confirmar",
                $"Remover {p.Descricao}?",
                "Sim",
                "Não");

            if (confirm)
            {
                await App.Db.Delete(p.Id);
                lista.Remove(p);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Produto p = e.SelectedItem as Produto;

            Navigation.PushAsync(new Views.EditarProduto
            {
                BindingContext = p,
            });
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    private async void lst_produtos_Refreshing(object sender, EventArgs e)
    {
        try
        {
            lista.Clear();

            var dados = await App.Db.GetAll();

            dados.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
        finally
        {
            lst_produtos.IsRefreshing = false;
        }
    }
}