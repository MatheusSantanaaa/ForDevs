using ForDevs.Domain.Core.DomainObjects;
using ForDevs.Domain.Enums;

namespace ForDevs.Domain.Models
{
    public class Cliente : Entity, IAggregateRoot
    {
        public string NomeDoCliente { get; private set; }
        public string NomeContato { get; private set; }
        public string? Cnpj { get; private set; }
        public Categoria Categoria { get; private set; }
        public DateTime DateDeCriacao { get; private set; }
        public ICollection<AvaliacaoCliente>? AvaliacaoClientes { get; private set; }

        public DateTime? UltimaAvaliacao
        {
            get => ObterUltimaAvaliacao();
        }

        public DateTime ObterUltimaAvaliacao()
        {
            return AvaliacaoClientes?.Count > 0
                     ? AvaliacaoClientes.OrderByDescending(x => x.DataDeCriacao).FirstOrDefault().DataDeCriacao
                     : DateTime.MinValue;
        }

        public Categoria ObterCategoriaPorNota(decimal nota) =>
            nota switch
            {
                >= 9 => Categoria.Promotor,
                >= 7 and <= 8 => Categoria.Neutro,
                _ => Categoria.Detrator
            };

        public Cliente(string nomeDoCliente,
                       string nomeContato,
                       string cnpj)
        {
            NomeDoCliente = nomeDoCliente;
            NomeContato = nomeContato;
            Cnpj = cnpj;
            DateDeCriacao = DateTime.Now;
        }

        public void Atualizar(string nomeDoCliente,
                              string nomeContato,
                              string cnpj)
        {
            NomeContato = nomeContato;
            NomeDoCliente = nomeDoCliente;
            Cnpj = cnpj;
            Categoria = Categoria.Nenhum;
        }

        public void AtualizarCategoria(Categoria categoria)
        {
            Categoria = categoria;
        }

        public static class Factory
        {
            public static Cliente CriarCliente(string nomeContato,
                                             string nomeDoDevedor,
                                             string cnpj) => new(nomeContato,
                                                                   nomeDoDevedor,
                                                                   cnpj);
        }
    }
}
