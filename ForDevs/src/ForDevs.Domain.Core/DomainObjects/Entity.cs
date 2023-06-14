﻿using ForDevs.Domain.Core.Communication.Messages;

namespace ForDevs.Domain.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }

        private List<Event> _notificacoes;
        public IReadOnlyCollection<Event> Notificacoes => _notificacoes?.AsReadOnly();

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public virtual bool IsValido()
        {
            throw new NotImplementedException();
        }

        public void AdicionarEvento(Event evento)
        {
            _notificacoes = _notificacoes ?? new List<Event>();
            _notificacoes.Add(evento);
        }

        public void RemoverEvento(Event evento)
        {
            _notificacoes?.Remove(evento);
        }

        public void LimparEvento()
        {
            _notificacoes.Clear();
        }
    }
}
