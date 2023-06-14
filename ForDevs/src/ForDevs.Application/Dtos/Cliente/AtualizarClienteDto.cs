namespace ForDevs.Application.Dtos.Cliente
{
    public class AtualizarClienteDto
    {
        public Guid Id { get; set; }
        public string NomeDoCliente { get; set; }
        public string NomeContato { get; set; }
        public string? Cnpj { get; set; }
    }
}
