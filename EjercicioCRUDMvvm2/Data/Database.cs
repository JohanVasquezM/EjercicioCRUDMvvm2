using SQLite;
using EjercicioCRUDMvvm2.Models;
using System.Threading.Tasks;

namespace EjercicioCRUDMvvm2.Data
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Proveedor>().Wait();
        }

        public Task<List<Proveedor>> GetProveedoresAsync()
        {
            return _database.Table<Proveedor>().ToListAsync();
        }

        public Task<Proveedor> GetProveedorAsync(int id)
        {
            return _database.Table<Proveedor>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveProveedorAsync(Proveedor proveedor)
        {
            if (proveedor.Id != 0)
            {
                return _database.UpdateAsync(proveedor);
            }
            else
            {
                return _database.InsertAsync(proveedor);
            }
        }

        public Task<int> DeleteProveedorAsync(Proveedor proveedor)
        {
            return _database.DeleteAsync(proveedor);
        }
    }
}
