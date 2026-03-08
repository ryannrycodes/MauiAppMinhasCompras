namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage // Página de listagem de produtos
{
    public ListaProduto()
    {
        InitializeComponent();
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e) 
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto()); // Abre a tela para cadastrar um novo produto

        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK"); // Mostra mensagem de erro caso aconteça algum problema
            
        }
    }
}