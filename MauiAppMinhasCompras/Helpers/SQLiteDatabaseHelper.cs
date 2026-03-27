using SQLite;
using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
        }

        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p);
        }

        public Task<int> Update(Produto p)
        {
            return _conn.UpdateAsync(p);
        }

        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        public Task<List<Produto>> Search(string q)
        {
            return _conn.Table<Produto>()
                        .Where(p => p.Descricao.Contains(q))
                        .ToListAsync();
        }

        public Task<List<Produto>> GetByCategoria(string categoria)
        {
            return _conn.Table<Produto>()
                        .Where(p => p.Categoria == categoria)
                        .ToListAsync();
        }

        public async Task<List<(string Categoria, double Total)>> RelatorioCategoria()
        {
            var lista = await GetAll();

            var resultado = lista
                .GroupBy(p => p.Categoria)
                .Select(g => (Categoria: g.Key, Total: g.Sum(p => p.Total)))
                .ToList();

            return resultado;
        }
    }
}