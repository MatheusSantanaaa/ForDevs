using ForDevs.IntegrationTests.Configurations;
using ForDevs.IntegrationTests.Fixtures;
using ForDevs.IntegrationTests.Models;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ForDevs.IntegrationTests
{
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class AvaliacaoScenarioTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly IntegrationTestsFixture<Program> _testsFixture;
        private readonly AvaliacaoTestFixture _avaliacaoTestFixture;

        public AvaliacaoScenarioTests(WebApplicationFactory<Program> factory, IntegrationTestsFixture<Program> testsFixture)
        {
            _factory = factory;
            _testsFixture = testsFixture;
            _avaliacaoTestFixture = new AvaliacaoTestFixture();
        }

        #region Registrar

        [Fact(DisplayName = "Registro de Avaliação com sucesso")]
        [Trait("Categoria", "Integração - Avaliação")]
        public async Task Avaliacao_Registrar_DeveExecutarComSucesso()
        {
            // Arrange
            await _testsFixture.RealizarLoginApi();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            var faker = _avaliacaoTestFixture.GerarAvaliacaoValido();

            var avaliacao = _avaliacaoTestFixture.ObterListaAvaliacaoAvaliacaoValido(faker);

            var avaliacaoCompleta = _avaliacaoTestFixture.ObterAvaliacaoCompletaValido(faker, avaliacao);

            // Act
            using var response = await _testsFixture.Client.PostAsync("/api/Avaliacao/registrar", _testsFixture.MontarContent(avaliacaoCompleta));

            //Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Registro de Avaliação com Erros")]
        [Trait("Categoria", "Integração - Avaliação")]
        public async Task Avaliacao_Registrar_DeveExecutarComErros()
        {
            // Arrange
            await _testsFixture.RealizarLoginApi();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            var faker = _avaliacaoTestFixture.GerarAvaliacaoValido();

            var avaliacao = _avaliacaoTestFixture.ObterListaAvaliacaoClienteInvalido(faker);

            var avaliacaoCompleta = _avaliacaoTestFixture.ObterAvaliacaoCompletaValido(faker, avaliacao);
            // Act
            using var response = await _testsFixture.Client.PostAsync("/api/Avaliacao/registrar", _testsFixture.MontarContent(avaliacaoCompleta));
            var error = await _testsFixture.DeserializeResponse<ApiResponseError>(response);

            // Assert
            Assert.Equal(400, error.status);
            Assert.Contains("A nota deve estar entre 0 e 10.", error.errors);
            Assert.Contains("O motivo da nota deve ser informado.", error.errors);
        }
        #endregion

        #region Atualizar
        [Fact(DisplayName = "Atualizar Avaliação com sucesso")]
        [Trait("Categoria", "Integração - Avaliação")]
        public async Task Avaliacao_Atualizar_DeveExecutarComSucesso()
        {
            // Arrange
            await _testsFixture.RealizarLoginApi();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            var faker = _avaliacaoTestFixture.GerarAvaliacaoValido();

            var avaliacao = _avaliacaoTestFixture.ObterListaAvaliacaoClienteValidoParaAtualizar(faker);

            var avaliacaoCompleta = _avaliacaoTestFixture.ObterAvaliacaoCompletaValidoParaAtualizar(faker, avaliacao);
            // Act
            using var response = await _testsFixture.Client.PostAsync("/api/Avaliacao/registrar", _testsFixture.MontarContent(avaliacaoCompleta));

            //Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Atualizar Avaliação com Erros")]
        [Trait("Categoria", "Integração - Avaliação")]
        public async Task Avaliacao_Atualizar_DeveExecutarComErros()
        {
            // Arrange
            await _testsFixture.RealizarLoginApi();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            var faker = _avaliacaoTestFixture.GerarAvaliacaoValido();

            var avaliacao = _avaliacaoTestFixture.ObterListaAvaliacaoClienteInvalidoParaAtualizar(faker);

            var avaliacaoCompleta = _avaliacaoTestFixture.ObterAvaliacaoCompletaInvalidoParaAtualizar(faker, avaliacao);
            // Act
            using var response = await _testsFixture.Client.PostAsync("/api/Avaliacao/registrar", _testsFixture.MontarContent(avaliacaoCompleta));
            var error = await _testsFixture.DeserializeResponse<ApiResponseError>(response);

            // Assert
            Assert.Equal(400, error.status);
            Assert.Contains("A nota deve estar entre 0 e 10.", error.errors);
            Assert.Contains("O motivo da nota deve ser informado.", error.errors);
        }
        #endregion

        #region Remover
        [Fact(DisplayName = "Remover Avaliação com Sucesso")]
        [Trait("Categoria", "Integração - Avaliação")]
        public async Task Avaliacao_Remover_DeveExecutarComSucesso()
        {
            // Arrange
            await _testsFixture.RealizarLoginApi();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            var id = Guid.Parse("9A36864E-AF1B-49DB-9FAF-A04256C312BF");
            // Act
            using var response = await _testsFixture.Client.DeleteAsync($"/api/Avaliacao/remover/{id}");

            //Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Remover Avaliação com Erro")]
        [Trait("Categoria", "Integração - Avaliação")]
        public async Task Avaliacao_Remover_DeveExecutarComErro()
        {
            // Arrange
            await _testsFixture.RealizarLoginApi();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            var id = Guid.Parse("1D994D11-63D9-4E7C-A2BE-C0FDFCC91F77");

            // Act
            using var response = await _testsFixture.Client.DeleteAsync($"/api/Avaliacao/remover/{id}");

            var error = await _testsFixture.DeserializeResponse<ApiResponseError>(response);

            // Assert
            Assert.Equal(400, error.status);
            Assert.Contains("Avaliação inválida ou inexistente", error.errors);
        }

        #endregion
    }
}
