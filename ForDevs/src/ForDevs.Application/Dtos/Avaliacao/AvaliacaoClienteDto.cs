namespace ForDevs.Application.Dtos.Avaliacao
{
    public class AvaliacaoClienteDto
    {
        public Guid ClienteId { get; set; }
        public double Nota { get; set; }
        public string MotivoNota { get; set; }
    }
}
