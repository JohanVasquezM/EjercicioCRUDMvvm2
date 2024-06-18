namespace EjercicioCRUDMvvm2.Models
{
    public class Proveedor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Rfc { get; set; } // Registro Federal de Contribuyentes (RFC)
    }
}