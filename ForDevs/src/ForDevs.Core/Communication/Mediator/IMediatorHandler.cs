using FluentValidation.Results;
using ForDevs.Core.Communication.Messages;

namespace ForDevs.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task<ValidationResult> EnviarComando<T>(T comando) where T : Command;
    }
}
