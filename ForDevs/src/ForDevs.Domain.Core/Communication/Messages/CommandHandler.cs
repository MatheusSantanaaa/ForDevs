﻿using FluentValidation.Results;
using ForDevs.Domain.Core.Data;

namespace ForDevs.Domain.Core.Communication.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AdicionarErro(string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        }

        protected async Task<ValidationResult> PersistirDados(IUnitOfWork unitOfWork)
        {
            if (!await unitOfWork.Commit()) AdicionarErro("Erro ao tentar salvar no banco.");

            return ValidationResult;
        }
    }
}
