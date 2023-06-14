using Bogus;
using Bogus.Extensions.Brazil;
using ForDevs.Application.Dtos.Cliente;
using ForDevs.Domain.Models;

namespace ForDevs.UniTests.Domain.Fixtures
{
    [CollectionDefinition(nameof(ClienteCollection))]
    public class ClienteCollection : ICollectionFixture<CLienteTestFixture> { }
    public class CLienteTestFixture
    {
        public string CNPJ_INVALIDO = "00000000000000";
        public Guid CLIENTE_ID = Guid.Parse("922A6E9D-0DAB-40DB-B29D-2D6F7A0E1E04");
        public Guid CLIENTE_ID_INVALIDO = Guid.Parse("8028F6D0-A78A-4D01-B379-674B7F67EB15");

        private readonly Faker<Cliente> Faker;

        public CLienteTestFixture()
        {
            Faker = new Faker<Cliente>("pt_BR");
        }

        public Cliente GerarClienteValido()
        {
            var cliente = Faker.CustomInstantiator(f =>
                Cliente.Factory.CriarCliente(
                   f.Person.FirstName,
                      f.Person.FullName,
                          f.Company.Cnpj()));

            return cliente;
        }

        public RegistrarClienteDto GerarClienteDtoRegistrar(Cliente cliente)
        {
            return new RegistrarClienteDto
            {
                NomeContato = cliente.NomeContato,
                NomeDoCliente = cliente.NomeDoCliente,
                Cnpj = cliente.Cnpj,
            };
        }

        public AtualizarClienteDto GerarClienteDtoAtualizarInvalido()
        {
            var cliente = Cliente.Factory.CriarCliente(string.Empty, string.Empty, CNPJ_INVALIDO);

            return new AtualizarClienteDto
            {
                Id = Guid.Empty,
                NomeContato = cliente.NomeContato,
                NomeDoCliente = cliente.NomeDoCliente,
                Cnpj = cliente.Cnpj,
            };
        }

        public RegistrarClienteDto GerarClienteDtoRegistrarInvalido()
        {
            var cliente = Cliente.Factory.CriarCliente(string.Empty, string.Empty, CNPJ_INVALIDO);

            return new RegistrarClienteDto
            {
                NomeContato = cliente.NomeContato,
                NomeDoCliente = cliente.NomeDoCliente,
                Cnpj = cliente.Cnpj,
            };
        }

        public AtualizarClienteDto GerarClienteDtoAtualizar(Cliente cliente)
        {
            return new AtualizarClienteDto
            {
                Id = CLIENTE_ID,
                NomeContato = cliente.NomeContato,
                NomeDoCliente = cliente.NomeDoCliente,
                Cnpj = cliente.Cnpj,
            };
        }
        public AtualizarClienteDto GerarClienteDtoAtualizarIdInvalido(Cliente cliente)
        {
            return new AtualizarClienteDto
            {
                Id = CLIENTE_ID_INVALIDO,
                NomeContato = cliente.NomeContato,
                NomeDoCliente = cliente.NomeDoCliente,
                Cnpj = cliente.Cnpj,
            };
        }



        public Cliente GerarClienteComCnpjInvalido()
        {
            var cliente = Faker.CustomInstantiator(f =>
                Cliente.Factory.CriarCliente(
                   f.Person.FirstName,
                      f.Person.FullName,
                          CNPJ_INVALIDO));

            return cliente;
        }
    }
}
