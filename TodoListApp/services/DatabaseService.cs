using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TodoListApp.Models;

namespace TodoListApp.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Tarea>().Wait();
        }

        public Task<List<Tarea>> GetTareasAsync() => _database.Table<Tarea>().ToListAsync();
        public Task<int> SaveTareaAsync(Tarea tarea) => _database.InsertAsync(tarea);
        public Task<int> UpdateTareaAsync(Tarea tarea) => _database.UpdateAsync(tarea);
        public Task<int> DeleteTareaAsync(Tarea tarea) => _database.DeleteAsync(tarea);
    }
}
