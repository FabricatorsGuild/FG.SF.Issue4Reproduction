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
        public static ServiceProxyFactory Default { get; } = new ServiceProxyFactory(
            c => new FabricTransportServiceRemotingClientFactory(serializationProvider: new ServiceRemotingDataContractSerializationProvider()));

        public static TServiceInterface CreateServiceProxy<TServiceInterface>(
            Uri serviceUri,
            ServicePartitionKey partitionKey = null,
            TargetReplicaSelector targetReplicaSelector = TargetReplicaSelector.Default,
            string listenerName = null)
            where TServiceInterface : IService
        {
            return Default.CreateServiceProxy<TServiceInterface>(serviceUri, partitionKey, targetReplicaSelector, listenerName);
        }
    }
}