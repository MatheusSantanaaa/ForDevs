using AutoMapper;
using ForDevs.Application.Dtos.Avaliacao;
using ForDevs.Application.Dtos.Cliente;
using ForDevs.Domain.Commands.Avaliacao;
using ForDevs.Domain.Commands.Cliente;
using ForDevs.Domain.Models;

namespace ForDevs.Application.AutoMapper
{
    public class MappingHelperProfile : Profile
    {
        public MappingHelperProfile()
        {
            #region ViewModels e Models
            CreateMap<ClienteDto, Cliente>().ReverseMap();
            CreateMap<RegistrarClienteDto, Cliente>().ReverseMap();
            CreateMap<AtualizarClienteDto, Cliente>().ReverseMap();

            CreateMap<AvaliacaoDto, Avaliacao>().ReverseMap();
            CreateMap<RegistrarAvaliacaoDto, Avaliacao>().ReverseMap();
            CreateMap<AtualizarAvaliacaoDto, Avaliacao>().ReverseMap();
            CreateMap<AvaliacaoClienteDto, AvaliacaoCliente>().ReverseMap();
            #endregion

            #region ViewModels e Commands
            CreateMap<RegistrarClienteDto, RegistrarClienteCommand>().ReverseMap();
            CreateMap<AtualizarClienteDto, AtualizarClienteCommand>().ReverseMap();

            CreateMap<RegistrarAvaliacaoDto, RegistrarAvaliacaoCommand>().ReverseMap();
            CreateMap<AtualizarAvaliacaoDto, AtualizarAvaliacaoCommand>().ReverseMap();
            CreateMap<AvaliacaoClienteDto, RegistrarAvaliacaoCommand>().ReverseMap();
            CreateMap<AvaliacaoClienteDto, AtualizarAvaliacaoCommand>().ReverseMap();
            #endregion
        }
    }
}
