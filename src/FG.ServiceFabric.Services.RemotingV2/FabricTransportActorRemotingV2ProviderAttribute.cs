using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ServiceFabric.Actors.Generator;
using Microsoft.ServiceFabric.Actors.Remoting.FabricTransport;
using Microsoft.ServiceFabric.Actors.Remoting.V2.FabricTransport.Client;
using Microsoft.ServiceFabric.Actors.Remoting.V2.FabricTransport.Runtime;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Communication.Client;
using Microsoft.ServiceFabric.Services.Remoting.FabricTransport;
using Microsoft.ServiceFabric.Services.Remoting.FabricTransport.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.V2;
using Microsoft.ServiceFabric.Services.Remoting.V2.Client;

namespace FG.ServiceFabric.Services.RemotingV2
{
    public class FabricTransportActorRemotingV2ProviderAttribute : FabricTransportActorRemotingProviderAttribute
    {
        public override IServiceRemotingListener CreateServiceRemotingListenerV2(ActorService actorService)
        {
            return new FabricTransportActorServiceRemotingListener(
                actorService, 
                new ServiceRemotingDataContractSerializationProvider());
        }

        public override IServiceRemotingClientFactory CreateServiceRemotingClientFactoryV2(IServiceRemotingCallbackMessageHandler callbackMessageHandler)
        {
            return new FabricTransportActorRemotingClientFactory(new FabricTransportRemotingSettings(), callbackMessageHandler, (IServicePartitionResolver)null, (IEnumerable<IExceptionHandler>)null, (string)null, new ServiceRemotingDataContractSerializationProvider());
        }
    }
}
