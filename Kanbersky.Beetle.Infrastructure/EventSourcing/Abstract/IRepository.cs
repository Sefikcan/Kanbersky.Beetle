﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kanbersky.Beetle.Infrastructure.EventSourcing.Abstract
{
    public interface IRepository<TAggregate> where TAggregate : IAggregate
    {
        Task<TAggregate> Find(Guid id);

        Task<ICollection<TAggregate>> Find(ICollection<Guid> id);

        Task Add(TAggregate aggregate);

        Task Add(ICollection<TAggregate> aggregates);

        Task Update(TAggregate aggregate);

        Task Update(ICollection<TAggregate> aggregates);

        Task Delete(TAggregate aggregate);

        Task Delete(ICollection<TAggregate> aggregates);
    }
}
