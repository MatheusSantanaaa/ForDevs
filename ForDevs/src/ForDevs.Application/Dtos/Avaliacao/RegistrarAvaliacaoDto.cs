namespace ForDevs.Application.Dtos.Avaliacao
{
    public class RegistrarAvaliacaoDto
    {
        public DateTime DataDeReferencia { get; set; }
        public ICollection<AvaliacaoClienteDto> AvaliacaoClientes { get; set; }
    }
}
