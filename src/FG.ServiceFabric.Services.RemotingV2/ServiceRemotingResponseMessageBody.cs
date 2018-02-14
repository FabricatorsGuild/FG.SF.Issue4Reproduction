namespace FG.ServiceFabric.Services.RemotingV2
{
    using System;
    using System.Runtime.Serialization;

    using Microsoft.ServiceFabric.Services.Remoting.V2;

    [DataContract(Name = "msgResponse", Namespace = "urn:FG.ServiceFabric.Communication")]
    internal class ServiceRemotingResponseMessageBody : IServiceRemotingResponseMessageBody
    {
        [DataMember]
        private object response;

        public object Get(Type paramType)
        {
            return this.response;
        }

        public void Set(object response)
        {
            this.response = response;
        }
    }
}