namespace StatelessService1
{
    using System;
    using System.Collections.Generic;
    using System.Fabric;
    using System.Threading;
    using System.Threading.Tasks;

    using FG.CallContext;
    using FG.ServiceFabric.Services.RemotingV2;

    using global::StatelessService1.Interfaces;

    using Microsoft.ServiceFabric.Services.Communication.Runtime;
    using Microsoft.ServiceFabric.Services.Remoting.FabricTransport.Runtime;
    using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Runtime;
    using Microsoft.ServiceFabric.Services.Runtime;

    /// <summary>
    ///     An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class StatelessService1 : StatelessService, IStatelessService1
    {
        public StatelessService1(StatelessServiceContext context)
            : base(context)
        {
        }

        public Task<ReturnDataModel> DoItAsync(RequestDataModel request, string text)
        {
            return Task.FromResult(
                new ReturnDataModel
                    {
                        IntValue = request.IntValue,
                        StringValue = request.StringValue,
                        CorrelationId = CallContext.Current.CorrelationId()
                    });
        }

        /// <summary>
        ///     Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new[]
                       {
                           new ServiceInstanceListener(
                               c => new FabricTransportServiceRemotingListener(
                                   c,
                                   this,
                                   new FabricTransportRemotingListenerSettings(),
                                   new ServiceRemotingDataContractSerializationProvider()))
                       };
        }

        /// <summary>
        ///     This is the main entry point for your service instance.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
        /// <returns>Task</returns>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following sample code with your own logic 
            // or remove this RunAsync override if it's not needed in your service.
            long iterations = 0;

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }
    }
}