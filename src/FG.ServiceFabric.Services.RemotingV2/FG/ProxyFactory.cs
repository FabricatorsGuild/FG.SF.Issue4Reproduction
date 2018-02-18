using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;

namespace FG.ServiceFabric.Services.RemotingV2.FG
{
    using System;

    using Microsoft.ServiceFabric.Services.Client;
    using Microsoft.ServiceFabric.Services.Communication.Client;
    using Microsoft.ServiceFabric.Services.Remoting;
    using Microsoft.ServiceFabric.Services.Remoting.Client;
    using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;

    public class ProxyFactory
    {
        public static ServiceProxyFactory DefaultServiceProxyFactory { get; } = new ServiceProxyFactory(
            c => new FabricTransportServiceRemotingClientFactory(serializationProvider: new ServiceRemotingDataContractSerializationProvider()));


        public static ActorProxyFactory DefaultActorProxyFactory { get; } = new ActorProxyFactory(
            c => new FabricTransportServiceRemotingClientFactory(serializationProvider: new ServiceRemotingDataContractSerializationProvider()));

        public static TServiceInterface CreateServiceProxy<TServiceInterface>(
            Uri serviceUri,
            ServicePartitionKey partitionKey = null,
            TargetReplicaSelector targetReplicaSelector = TargetReplicaSelector.Default,
            string listenerName = null)
            where TServiceInterface : IService
        {
            return DefaultServiceProxyFactory.CreateServiceProxy<TServiceInterface>(serviceUri, partitionKey, targetReplicaSelector, listenerName);
        }

        public static TServiceInterface CreateActorServiceProxy<TServiceInterface>(
            ActorId actorId,
            Uri serviceUri,
            string listenerName = null)
            where TServiceInterface : IService
        {
            return DefaultActorProxyFactory.CreateActorServiceProxy<TServiceInterface>(serviceUri, actorId, listenerName);
        }

        public static TActorServiceInterface CreateActorProxy<TActorServiceInterface>(
            ActorId actorId,
            Uri serviceUri,
            string listenerName = null)
            where TActorServiceInterface : IService, IActor
        {
            return DefaultActorProxyFactory.CreateActorProxy<TActorServiceInterface>(serviceUri, actorId, listenerName);
        }
    }
}