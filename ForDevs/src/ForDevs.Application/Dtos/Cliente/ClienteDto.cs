using ForDevs.Application.Dtos.Avaliacao;
using ForDevs.Domain.Enums;
using ForDevs.Domain.Models;

namespace ForDevs.Application.Dtos.Cliente
{
    public class ClienteDto
    {
        public Guid Id { get; set; }
        public string NomeDoCliente { get; set; }
        public string NomeContato { get; set; }
        public string Cnpj { get; set; }
        public Categoria Categoria { get; set; }
        public DateTime DateDeCriacao { get; set; }
        public DateTime UltimaAvaliacao { get; set; }
        public ICollection<AvaliacaoClienteDto> AvaliacaoClientes { get; set; }
    }
}
