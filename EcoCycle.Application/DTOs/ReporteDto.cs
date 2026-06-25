namespace EcoCycle.Application.DTOs
{
    public class ReporteGeneralDto
    {
        public int TotalMateriales { get; set; }
        public int TotalPublicaciones { get; set; }
        public int TotalCentros { get; set; }
        public int TotalUsuarios { get; set; }
        public int TotalPuntos { get; set; }
        public List<PublicacionDto> UltimasPublicaciones { get; set; } = new();
    }
}
