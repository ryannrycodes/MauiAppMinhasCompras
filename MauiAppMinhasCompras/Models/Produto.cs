using SQLite;

namespace MauiAppMinhasCompras.Models
{
    public class Produto
    {
        string _descricao;

        [PrimaryKey, AutoIncrement] // Define o Id como chave primária com auto incremento no banco
        public int Id { get; set; }

        public string Descricao
        {
            get => _descricao;
            set
            {
                // Validação: impede que a descrição seja nula
                if (value == null)
                {
                    throw new Exception("Por favor, preencha a descrição");
                }

                _descricao = value;
            }
        }

        public double Quantidade { get; set; }
        public double Preco { get; set; }

        // Propriedade calculada: não é armazenada no banco,
        // retorna automaticamente Quantidade * Preco
        public double Total
        {
            get => Quantidade * Preco;
        }
    }
}