using FluentValidation.Results;
using ForDevs.Domain.Core.Communication.Messages;

namespace ForDevs.Domain.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task<ValidationResult> EnviarComando<T>(T comando) where T : Command;
    }
}
