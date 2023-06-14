using ForDevs.IntegrationTests.Models;
using ForDevs.Services.Api.Dtos;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using System.Text.Json;

namespace ForDevs.IntegrationTests.Configurations
{
    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<Program>> { }
    public class IntegrationTestsFixture<TProgram> : IDisposable where TProgram : class
    {
        public readonly ForDevsAppFactory<TProgram> Factory;
        public HttpClient Client;
        public string UsuarioToken;

        public IntegrationTestsFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
                BaseAddress = new Uri("http://localhost"),
                HandleCookies = true,
                MaxAutomaticRedirections = 7
            };

            Factory = new ForDevsAppFactory<TProgram>();
            Client = Factory.CreateClient(clientOptions);
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }

        public StringContent MontarContent(object dado)
        {
            return new StringContent(
                JsonSerializer.Serialize(dado),
                Encoding.UTF8,
                "application/json");
        }

        public async Task<T> DeserializeResponse<T>(HttpResponseMessage ResponseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(await ResponseMessage.Content.ReadAsStringAsync(), options);
        }

        public async Task RealizarLoginApi()
        {
            var usuario = new UsuarioLoginDto()
            {
                Email = "master@teste.com.br",
                Senha = "Alpha@4456",
            };

            var content = MontarContent(usuario);

            Client = Factory.CreateClient();

            var response = await Client.PostAsync("api/Autenticacao/entrar/", content);
            response.EnsureSuccessStatusCode();

            var apiResponse = await DeserializeResponse<ApiResponseSucess>(response);

            UsuarioToken = apiResponse.model?.accessToken;

        }
    }
}
