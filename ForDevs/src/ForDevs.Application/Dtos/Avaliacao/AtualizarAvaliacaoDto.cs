namespace ForDevs.Application.Dtos.Avaliacao
{
    public class AtualizarAvaliacaoDto
    {
        public Guid Id { get; set; }
        public DateTime DataDeReferencia { get; set; }
        public ICollection<AvaliacaoClienteDto> AvaliacaoClientes { get; set; }
    }
}
