using FluentValidation.Results;
using ForDevs.Application.AutoMapper;
using ForDevs.Application.Interfaces;
using ForDevs.Application.Services;
using ForDevs.Domain.Commands.Avaliacao;
using ForDevs.Domain.Commands.Cliente;
using ForDevs.Domain.Core.Communication.Mediator;
using ForDevs.Domain.Interfaces;
using ForDevs.Domain.Interfaces.Usuario;
using ForDevs.Infra.CrossCutting;
using ForDevs.Infra.Data.Context;
using ForDevs.Infra.Data.Repository;
using MediatR;

namespace ForDevs.Services.Api.Configurations
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //Auto Mapper
            services.AddAutoMapper(typeof(MappingHelperProfile));

            //Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            //Repository
            services.AddScoped<ForDevsContext>();
            services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();

            // Application - Commands
            services.AddScoped<IRequestHandler<RegistrarClienteCommand, ValidationResult>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarClienteCommand, ValidationResult>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverClienteCommand, ValidationResult>, ClienteCommandHandler>();

            services.AddScoped<IRequestHandler<RegistrarAvaliacaoCommand, ValidationResult>, AvaliacaoCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarAvaliacaoCommand, ValidationResult>, AvaliacaoCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverAvaliacaoCommand, ValidationResult>, AvaliacaoCommandHandler>();

            // Application - Services
            services.AddScoped<IClienteAppService, CLienteAppService>();
            services.AddScoped<IAvaliacaoAppService, AvaliacaoAppService>();

            // Infra - Identity
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}
