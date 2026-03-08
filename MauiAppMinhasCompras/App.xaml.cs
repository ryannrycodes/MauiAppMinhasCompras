using MauiAppMinhasCompras.Helpers;

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        static SQLiteDatabaseHelper _db; // Variável estática para conexão com banco


        public static SQLiteDatabaseHelper Db // Propriedade pública para acessar o banco
        {
            get
            {
                if (_db == null)
                {
                    string path = Path.Combine( // Cria o caminho do banco no dispositivo
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData),
                        "banco_sqlite_compras.db3"); // Nome do arquivo do banco

                    _db = new SQLiteDatabaseHelper(path); // Cria a conexão com o banco

                }

                return _db; // Retorna a conexão com o banco
            }
        }

        public App()
        {
            InitializeComponent(); // Inicializa os componentes do aplicativo

            MainPage = new NavigationPage(new Views.ListaProduto());
        }
    }
}