using System.Reflection.Metadata;

namespace ForDevs.Application.Dtos.Cliente
{
    public class RegistrarClienteDto
    {
        public string NomeDoCliente { get; set; }
        public string NomeContato { get; set; }
        public string? Cnpj { get; set; }
    }
}
