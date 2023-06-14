using Bogus;
using Bogus.Extensions.Brazil;
using ForDevs.Application.Dtos.Avaliacao;
using ForDevs.Domain.Models;
using System.Collections.Generic;

namespace ForDevs.IntegrationTests.Fixtures
{
    public class AvaliacaoTestFixture
    {
        private readonly Faker<Avaliacao> Faker;
        public Guid CLIENTE_ID = Guid.Parse("922A6E9D-0DAB-40DB-B29D-2D6F7A0E1E04");
        public Guid AVALIACAO_ID = Guid.Parse("7254879D-EDC4-469C-BB41-792E8F2D0A94");
        public Guid ID_INVALIDO = Guid.Parse("8028F6D0-A78A-4D01-B379-674B7F67EB15");

        public AvaliacaoTestFixture()
        {
            Faker = new Faker<Avaliacao>("pt_BR");
        }

        public Avaliacao GerarAvaliacaoValido()
        {
            var avaliacao = Faker.CustomInstantiator(f =>
                Avaliacao.Factory.CriarAvaliacao(
                          f.Date.Past()));

            return avaliacao;
        }

        public AvaliacaoCliente ObterListaAvaliacaoAvaliacaoValido(Avaliacao avaliacao)
        {
            return AvaliacaoCliente.Factory.CriarAvaliacaoCliente(avaliacao.Id, CLIENTE_ID, 8.5, "Motivo Teste");
        }

        public List<AvaliacaoCliente> ObterListaAvaliacaoClienteValidoParaAtualizar(Avaliacao avaliacao)
        {
            return new List<AvaliacaoCliente>
            {
                AvaliacaoCliente.Factory.CriarAvaliacaoCliente(avaliacao.Id, CLIENTE_ID, 8.5, "Motivo Teste"),
                AvaliacaoCliente.Factory.CriarAvaliacaoCliente(avaliacao.Id, CLIENTE_ID, 10, "Motivo Teste"),
            };
        }

        public List<AvaliacaoCliente> ObterListaAvaliacaoClienteInvalidoParaAtualizar(Avaliacao avaliacao)
        {
            return new List<AvaliacaoCliente>
            {
                AvaliacaoCliente.Factory.CriarAvaliacaoCliente(avaliacao.Id, CLIENTE_ID, 10.5, string.Empty),
                AvaliacaoCliente.Factory.CriarAvaliacaoCliente(avaliacao.Id, CLIENTE_ID, 15, string.Empty),
            };
        }

        public AvaliacaoCliente ObterListaAvaliacaoClienteInvalido(Avaliacao avaliacao)
        {
            return AvaliacaoCliente.Factory.CriarAvaliacaoCliente(avaliacao.Id, CLIENTE_ID, 15, string.Empty);
        }

        public RegistrarAvaliacaoDto ObterAvaliacaoCompletaValido(Avaliacao avaliacao, AvaliacaoCliente avaliacaoCliente)
        {
            return new RegistrarAvaliacaoDto()
            {
                DataDeReferencia = avaliacao.DataDeReferencia,
                AvaliacaoClientes = new List<AvaliacaoClienteDto>()
                  {
                      new AvaliacaoClienteDto()
                      {
                          ClienteId = avaliacaoCliente.ClienteId,
                          MotivoNota = avaliacaoCliente.MotivoNota,
                          Nota = avaliacaoCliente.Nota
                      }
                  }
            };
        }

        public AtualizarAvaliacaoDto ObterAvaliacaoCompletaValidoParaAtualizar(Avaliacao avaliacao, List<AvaliacaoCliente> avaliacaoClientes)
        {
            List<AvaliacaoClienteDto> avaliacaoClienteDtos = new();
            foreach (var item in avaliacaoClientes)
            {
                avaliacaoClienteDtos.Add(new AvaliacaoClienteDto
                {
                    ClienteId = item.ClienteId,
                    MotivoNota = item.MotivoNota,
                    Nota = item.Nota
                });
            }

            return new AtualizarAvaliacaoDto()
            {
                Id = AVALIACAO_ID,
                DataDeReferencia = avaliacao.DataDeReferencia,
                AvaliacaoClientes = avaliacaoClienteDtos

            };
        }

        public AtualizarAvaliacaoDto ObterAvaliacaoCompletaInvalidoParaAtualizar(Avaliacao avaliacao, List<AvaliacaoCliente> avaliacaoClientes)
        {
            List<AvaliacaoClienteDto> avaliacaoClienteDtos = new();
            foreach (var item in avaliacaoClientes)
            {
                avaliacaoClienteDtos.Add(new AvaliacaoClienteDto
                {
                    ClienteId = item.ClienteId,
                    MotivoNota = item.MotivoNota,
                    Nota = item.Nota
                });
            }

            return new AtualizarAvaliacaoDto()
            {
                Id = ID_INVALIDO,
                DataDeReferencia = avaliacao.DataDeReferencia,
                AvaliacaoClientes = avaliacaoClienteDtos

            };
        }
    }
}
