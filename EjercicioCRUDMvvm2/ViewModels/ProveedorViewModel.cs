using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EjercicioCRUDMvvm2.Models;
using EjercicioCRUDMvvm2.Data;
using Microsoft.Maui.Controls;
using System.IO;

namespace EjercicioCRUDMvvm2.ViewModels
{
    public class ProveedorViewModel : BindableObject
    {
        private Database _database;
        private Proveedor _proveedor;

        public ObservableCollection<Proveedor> Proveedores { get; set; }

        public Proveedor Proveedor
        {
            get { return _proveedor; }
            set
            {
                _proveedor = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }

        public ProveedorViewModel()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EjercicioCRUDMvvm2.db3");
            _database = new Database(dbPath);
            Proveedores = new ObservableCollection<Proveedor>();
            Proveedor = new Proveedor();
            SaveCommand = new Command(OnSave);

            LoadProveedores();
        }

        private async void LoadProveedores()
        {
            var proveedores = await _database.GetProveedoresAsync();
            foreach (var proveedor in proveedores)
            {
                Proveedores.Add(proveedor);
            }
        }

        private async void OnSave()
        {
            if (string.IsNullOrWhiteSpace(Proveedor.Nombre) ||
                string.IsNullOrWhiteSpace(Proveedor.Direccion) ||
                string.IsNullOrWhiteSpace(Proveedor.Telefono) ||
                string.IsNullOrWhiteSpace(Proveedor.Email) ||
                string.IsNullOrWhiteSpace(Proveedor.Rfc))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Todos los campos son obligatorios.", "OK");
                return;
            }

            await _database.SaveProveedorAsync(Proveedor);
            Proveedores.Add(Proveedor);
            Proveedor = new Proveedor();
        }
    }
}