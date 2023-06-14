using ForDevs.Domain.Core.DomainObjects;

namespace ForDevs.Domain.Models
{
    public class AvaliacaoCliente : Entity
    {
        public Guid AvaliacaoId { get; private set; }
        public Avaliacao Avaliacao { get; private set; }
        public Guid ClienteId { get; private set; }
        public Cliente Cliente { get; private set; }
        public DateTime DataDeCriacao { get; private set; }
        public double Nota { get; private set; }
        public string MotivoNota { get; private set; }

        public AvaliacaoCliente() {}
        public AvaliacaoCliente(Guid avaliacaoId, Guid clienteId, double nota, string motivoNota)
        {
            AvaliacaoId = avaliacaoId;
            ClienteId = clienteId;
            DataDeCriacao = DateTime.Now;
            Nota = nota;
            MotivoNota = motivoNota;
        }
        public static class Factory
        {
            public static AvaliacaoCliente CriarAvaliacaoCliente(
                Guid avaliacaoId,
                Guid clienteId,
                double nota,
                string motivoNota) => new AvaliacaoCliente(avaliacaoId, clienteId, nota, motivoNota);
        }
    }

}
