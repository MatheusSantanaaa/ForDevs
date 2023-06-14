using ForDevs.Domain.Core.DomainObjects;
using ForDevs.Domain.Enums;

namespace ForDevs.Domain.Models
{
    public class Avaliacao : Entity, IAggregateRoot
    {
        public DateTime DataDeReferencia { get; private set; }
        public ICollection<AvaliacaoCliente> AvaliacaoClientes { get; private set; }

        public int TotalDeParticipantes
        {
            get => AvaliacaoClientes == null ? 0 : AvaliacaoClientes.Count();
        }

        public Avaliacao() {}
        public Avaliacao(DateTime dataDeReferencia)
        {
            DataDeReferencia = dataDeReferencia;
        }

        public int QuantidadeDePromotores
        {
            get => ObterQuantidadeDePromotores();
        }

        public int QuantidadeDeNeutros
        {
            get => ObterQuantidadeDeNeutros();
        }

        public int QuantidadeDeDetratores
        {
            get => ObterQuantidadeDeDetratores();
        }
        public decimal ResultadoGeral
        {
            get => CalcularResultadoGeral();
        }

        public decimal CalcularResultadoGeral()
        {
            if(QuantidadeDePromotores is 0 && QuantidadeDeDetratores is 0 & TotalDeParticipantes is 0)
            {
                return (decimal)0;
            }
            decimal resultado = ((decimal)(QuantidadeDePromotores - QuantidadeDeDetratores) / TotalDeParticipantes) * 100;
            return Math.Round(resultado, 2);
        }

        public void Atualizar(DateTime dataDeRerencia)
        {
            DataDeReferencia = dataDeRerencia;
        }

        public void AdicionarAvaliacaoClientes(ICollection<AvaliacaoCliente>? avaliacaoClientes) => AvaliacaoClientes = avaliacaoClientes;

        public int ObterQuantidadeDePromotores()
        {
            return AvaliacaoClientes.Count(x => x.Cliente.Categoria.Equals(Categoria.Promotor));
        }

        public int ObterQuantidadeDeNeutros()
        {
            return AvaliacaoClientes.Count(x => x.Cliente.Categoria.Equals(Categoria.Neutro));
        }

        public int ObterQuantidadeDeDetratores()
        {
            return AvaliacaoClientes.Count(x => x.Cliente.Categoria.Equals(Categoria.Detrator));
        }

        public static class Factory
        {
            public static Avaliacao CriarAvaliacao(DateTime dataDeReferencia) => new(dataDeReferencia);
        }
    }
}
