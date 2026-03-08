using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage // Tela de cadastro de produto
{
    public NovoProduto()
    {
        InitializeComponent();
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Produto p = new Produto // Cria um novo objeto Produto
            {
                Descricao = txt_descricao.Text, // Recebe a descrição digitada
                Quantidade = Convert.ToDouble(txt_quantidade.Text), // Converte o texto da quantidade para número
                Preco = Convert.ToDouble(txt_preco.Text) // Converte o texto do preço para número
            };

            await App.Db.Insert(p); // Insere o produto no bd
            await DisplayAlert("Sucesso!", "Registro Inserido", "OK"); // Mostra mensagem de sucesso

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK"); // Mostra mensagem de erro caso aconteça algum problema
        }
    }
}