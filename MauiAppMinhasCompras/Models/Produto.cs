using SQLite;
namespace MauiAppMinhasCompras.Models
{
    public class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } // Identificador único do produto
        public string Descricao { get; set; } // Nome ou descrição do produto
        public double Quantidade { get; set; } // Quantidade do produto
        public double Preco { get; set; } // Preço unitário do produto[
        public double Total { get => Quantidade * Preco; } // Total Produto
    }
} 