namespace TechOilFrontend.ViewModels
{
    public enum Tipo
    {
        Administrador = 1,
        Consultor = 2
    }
    public class UsuariosViewModel
    {
        public string Nombre { get; set; }
        public int Dni { get; set; }
        public Tipo Tipo { get; set; }
        public string Clave { get; set; }
        public string Usuario_Email { get; set; }
    }
}
