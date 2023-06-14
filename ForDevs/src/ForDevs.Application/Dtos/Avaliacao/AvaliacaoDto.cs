namespace ForDevs.Application.Dtos.Avaliacao
{
    public class AvaliacaoDto
    {
        public Guid Id { get; set; }
        public DateTime DataDeReferencia { get; set; }
        public int TotalDeParticipantes { get; set; }
        public decimal ResultadoGeral { get; set; }
        public int QuantidadeDePromotores { get; set; }
        public int QuantidadeDeNeutros { get; set; }
        public int QuantidadeDeDetratores { get; set; }
        public ICollection<AvaliacaoClienteDto> AvaliacaoClientes { get; set; }
    }
}
