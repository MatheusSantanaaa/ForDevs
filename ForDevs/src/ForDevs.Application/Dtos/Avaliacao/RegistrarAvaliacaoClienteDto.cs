namespace ForDevs.Application.Dtos.Avaliacao
{
    public class RegistrarAvaliacaoClienteDto
    {
        public Guid ClienteId { get; set; }
        public double Nota { get; set; }
        public string MotivoNota { get; set; }
    }
}
