using ForDevs.IntegrationTests.Configurations;
using ForDevs.IntegrationTests.Fixtures;
using ForDevs.IntegrationTests.Models;
using Microsoft.AspNetCore.Mvc.Testing;


namespace ForDevs.IntegrationTests
{
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class ClienteScenarioTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly IntegrationTestsFixture<Program> _testsFixture;
        private readonly CLienteTestFixture _clienteTestFixture;

        public ClienteScenarioTests(WebApplicationFactory<Program> factory, IntegrationTestsFixture<Program> testsFixture)
        {
            _factory = factory;
            _testsFixture = testsFixture;
            _clienteTestFixture = new CLienteTestFixture();
        }

        #region Registrar

        [Fact(DisplayName = "Registro de Cliente com sucesso")]
        [Trait("Categoria", "Integração - Cliente")]
        public async Task Cliente_Registrar_DeveExecutarComSucesso()
        {
            // Arrange
            var faker = _clienteTestFixture.GerarClienteValido();

            var cliente = _clienteTestFixture.GerarClienteDtoRegistrar(faker);

            // Act
            using var response = await _testsFixture.Client.PostAsync("/api/Cliente/registrar", _testsFixture.MontarContent(cliente));

            //Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Registro de Cliente com Cnpj Inválido")]
        [Trait("Categoria", "Integração - Cliente")]
        public async Task Cliente_Registrar_DeveExecutarComErro()
        {
            // Arrange
            var faker = _clienteTestFixture.GerarClienteComCnpjInvalido();

            var cliente = _clienteTestFixture.GerarClienteDtoRegistrar(faker);

            // Act
            using var response = await _testsFixture.Client.PostAsync("/api/Cliente/registrar", _testsFixture.MontarContent(cliente));

            var error = await _testsFixture.DeserializeResponse<ApiResponseError>(response);

            // Assert
            Assert.Equal(400, error.status);
            Assert.Contains("O CNPJ informado não é valido.", error.errors);
        }

        [Fact(DisplayName = "Registrar Cliente com Erros")]
        [Trait("Categoria", "Integração - Cliente")]
        public async Task Cliente_Cliente_DeveExecutarComErroInvalido()
        {
            // Arrange
            var faker = _clienteTestFixture.GerarClienteDtoRegistrarInvalido();

            // Act
            using var response = await _testsFixture.Client.PostAsync("/api/Cliente/registrar", _testsFixture.MontarContent(faker));

            var error = await _testsFixture.DeserializeResponse<ApiResponseError>(response);

            // Assert
            Assert.Equal(400, error.status);
            Assert.Contains("O nome do contato é obrigatório.", error.errors);
            Assert.Contains("O CNPJ informado não é valido.", error.errors);
            Assert.Contains("O nome do cliente é obrigatório.", error.errors);
        }

        #endregion

        #region Atualizar

        [Fact(DisplayName = "Atualizar Cliente com sucesso")]
        [Trait("Categoria", "Integração - Cliente")]
        public async Task Cliente_Atualizar_DeveExecutarComSucesso()
        {
            // Arrange
            var faker = _clienteTestFixture.GerarClienteValido();

            var cliente = _clienteTestFixture.GerarClienteDtoAtualizar(faker);

            // Act
            using var response = await _testsFixture.Client.PutAsync("/api/Cliente/atualizar", _testsFixture.MontarContent(cliente));

            //Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Atualizar Cliente com Erro Id inválido")]
        [Trait("Categoria", "Integração - Cliente")]
        public async Task Cliente_Atualizar_DeveExecutarComErroIdInvalido()
        {
            // Arrange
            var faker = _clienteTestFixture.GerarClienteValido();

            var cliente = _clienteTestFixture.GerarClienteDtoAtualizarIdInvalido(faker);

            // Act
            using var response = await _testsFixture.Client.PutAsync("/api/Cliente/atualizar", _testsFixture.MontarContent(cliente));

            var error = await _testsFixture.DeserializeResponse<ApiResponseError>(response);

            // Assert
            Assert.Equal(400, error.status);
            Assert.Contains("Cliente inválido ou inexistente.", error.errors);
        }

        [Fact(DisplayName = "Atualizar Cliente com Erros")]
        [Trait("Categoria", "Integração - Cliente")]
        public async Task Cliente_Atualizar_DeveExecutarComErroInvalido()
        {
            // Arrange
            var faker = _clienteTestFixture.GerarClienteDtoAtualizarInvalido();

            // Act
            using var response = await _testsFixture.Client.PutAsync("/api/Cliente/atualizar", _testsFixture.MontarContent(faker));

            var error = await _testsFixture.DeserializeResponse<ApiResponseError>(response);

            // Assert
            Assert.Equal(400, error.status);
            Assert.Contains("O Id é obrigatório.", error.errors);
            Assert.Contains("O CNPJ informado não é valido.", error.errors);
        }

        #endregion

        #region Remover

        [Fact(DisplayName = "Remover Cliente com sucesso")]
        [Trait("Categoria", "Integração - Cliente")]
        public async Task Cliente_Remover_DeveExecutarComSucesso()
        {
            // Arrange
            var id = Guid.Parse("FC361B0C-E48E-4F9D-AF78-16D7D438ECE4");

            // Act
            using var response = await _testsFixture.Client.DeleteAsync($"/api/Cliente/remover/{id}");

            //Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Remover Cliente com Erro Id Inválido")]
        [Trait("Categoria", "Integração - Cliente")]
        public async Task Divida_Remover_DeveExecutarComErroIdObrigatorio()
        {
            // Arrange
            var id = Guid.Parse("00000000-0000-0000-0000-000000000000");

            // Act
            using var response = await _testsFixture.Client.DeleteAsync($"/api/Cliente/remover/{id}");
            var error = await _testsFixture.DeserializeResponse<ApiResponseError>(response);

            // Assert
            Assert.Equal(400, error.status);
            Assert.Contains("O Id é obrigatório.", error.errors);

        }

        [Fact(DisplayName = "Remover Divida com Erro CLiente Inválido")]
        [Trait("Categoria", "Integração - Cliente")]
        public async Task Divida_Remover_DeveExecutarComErroIdInexistente()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            using var response = await _testsFixture.Client.DeleteAsync($"/api/Cliente/remover/{id}");
            var error = await _testsFixture.DeserializeResponse<ApiResponseError>(response);

            // Assert
            Assert.Equal(400, error.status);
            Assert.Contains("Cliente inválido ou inexistente.", error.errors);

        }

        #endregion

    }
}
