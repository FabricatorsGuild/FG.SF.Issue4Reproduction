﻿using FG.CallContext;

namespace ActorService1
{
    using System.Threading;
    using System.Threading.Tasks;

    using global::ActorService1.Interfaces;

    using Microsoft.ServiceFabric.Actors;
    using Microsoft.ServiceFabric.Actors.Runtime;

    /// <remarks>
    ///     This class represents an actor.
    ///     Every ActorID maps to an instance of this class.
    ///     The StatePersistence attribute determines persistence and replication of actor state:
    ///     - Persisted: State is written to disk and replicated.
    ///     - Volatile: State is kept in memory only and replicated.
    ///     - None: State is kept in memory only and not replicated.
    /// </remarks>
    [StatePersistence(StatePersistence.None)]
    internal class ActorService1 : Actor, IActorService1
    {
        /// <summary>
        ///     Initializes a new instance of ActorService1
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public ActorService1(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        /// <summary>
        ///     TODO: Replace with your own actor method.
        /// </summary>
        /// <returns></returns>
        Task<string> IActorService1.GetCorrelationIdAsync(int value)
        {
            var cid = CallContext.Current.CorrelationId();
            return Task.FromResult(cid);
        }
    }
}