using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TodoListApp.Models;
using TodoListApp.Services;

namespace TodoListApp.ViewModels
{
    public partial class TareaViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;
        public ObservableCollection<Tarea> Tareas { get; } = new ObservableCollection<Tarea>();

        [ObservableProperty]
        string nombre;
        [ObservableProperty]
        string estado;

        public TareaViewModel()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "tareas.db");
            _databaseService = new DatabaseService(dbPath);
            CargarTareas();
        }

        private async void CargarTareas()
        {
            var tareas = await _databaseService.GetTareasAsync();
            foreach (var tarea in tareas)
            {
                Tareas.Add(tarea);
            }
        }

        [RelayCommand]
        public async Task AgregarTarea()
        {
            var tarea = new Tarea { Nombre = Nombre, Estado = Estado };
            await _databaseService.SaveTareaAsync(tarea);
            Tareas.Add(tarea);
            Nombre = string.Empty;
        }

        [RelayCommand]
        public async Task EliminarTarea(Tarea tarea)
        {
            await _databaseService.DeleteTareaAsync(tarea);
            Tareas.Remove(tarea);
        }

        [RelayCommand]
        public async Task EditarTarea(Tarea tarea)
        {
            tarea.Nombre = Nombre;
            tarea.Estado = Estado;
            await _databaseService.UpdateTareaAsync(tarea);
            CargarTareas(); // Recargar para reflejar los cambios
        }
    }
}
