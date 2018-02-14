// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace FG.ServiceFabric.Services.RemotingV2
{
    using global::FG.CallContext;

    using global::Microsoft.ServiceFabric.Services.Remoting.V2;

    internal class DataContractRemotingMessageFactory : IServiceRemotingMessageBodyFactory
    {
        public IServiceRemotingRequestMessageBody CreateRequest(string interfaceName, string methodName, int numberOfParameters)
        {
            // Create a new request object and add the correlationId property name
            return new ServiceRemotingRequestMessageBody(numberOfParameters)
                .Set(Constants.ExecutionTree.CorrelationIdPropertyName, CallContext.Current.CorrelationId());
        }

        public IServiceRemotingResponseMessageBody CreateResponse(string interfaceName, string methodName)
        {
            return new ServiceRemotingResponseMessageBody();
        }
    }
}
