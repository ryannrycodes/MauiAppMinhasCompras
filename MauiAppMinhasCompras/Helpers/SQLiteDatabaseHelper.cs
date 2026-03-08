using SQLite;
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper // Classe responsável por manipular o banco
    {
        readonly SQLiteAsyncConnection _conn; // Conexão assíncrona com o banco

        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait(); // Cria a tabela Produto caso ela ainda não exista
        }
        
        public Task<int> Insert(Produto p) 
        {
            return _conn.InsertAsync(p); // Insere um novo produto no banco
        }

        public Task<List<Produto>> Update(Produto p) 
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?"; // Comando SQL para atualizar um produto

            return _conn.QueryAsync<Produto>(sql, p.Descricao, p.Quantidade, p.Preco, p.Id); // Executa a atualização no banco

        }

        public Task<int> Delete(int id) 
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id); // Deleta um produto pelo Id
        }

        public Task<List<Produto>> GetAll() 
        {
            return _conn.Table<Produto>().ToListAsync(); // Retorna todos os produtos cadastrados
        }

        public Task<List<Produto>> Search(String q) 
        {
            string sql = "SELECT * Produto WHERE descricao LIKE '%" + q + "%'"; // Busca produtos que contenham o texto informado

            return _conn.QueryAsync<Produto>(sql);
        }

    }
}