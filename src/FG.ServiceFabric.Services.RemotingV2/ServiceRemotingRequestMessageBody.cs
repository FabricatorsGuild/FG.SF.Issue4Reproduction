namespace FG.ServiceFabric.Services.RemotingV2
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using Microsoft.ServiceFabric.Services.Remoting.V2;

    [DataContract(Name = "msgBody", Namespace = "urn:FG.ServiceFabric.Communication")]
    internal class ServiceRemotingRequestMessageBody : IServiceRemotingRequestMessageBody
    {
        [DataMember]
        private readonly Dictionary<string, object> parameters;

        public ServiceRemotingRequestMessageBody(int numberOfParameters)
        {
            this.parameters = new Dictionary<string, object>(numberOfParameters);
        }

        public object GetParameter(int position, string paramName, Type paramType)
        {
            return this.parameters[paramName];
        }

        public T GetParameter<T>(int position, string paramName)
        {
            return (T)this.parameters[paramName];
        }

        public void SetParameter(int position, string paramName, object parameter)
        {
            this.parameters[paramName] = parameter;
        }

        public ServiceRemotingRequestMessageBody Set(string paramName, object value)
        {
            this.parameters[paramName] = value;
            return this;
        }
    }
}