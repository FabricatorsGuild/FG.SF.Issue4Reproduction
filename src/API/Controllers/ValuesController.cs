namespace API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FG.CallContext;
    using FG.ServiceFabric.Services.RemotingV2;
    using FG.ServiceFabric.Services.RemotingV2.FG;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.ServiceFabric.Services.Remoting.Client;
    using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;
    using StatelessService1.Interfaces;

    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public async Task GetAsync()
        {
            var serviceProxy = ProxyFactory.CreateServiceProxy<IStatelessService1>(new Uri("fabric:/FG.SF.Issue4Reproduction/StatelessService1"));

            var cid = Guid.NewGuid().ToString("D");
            CallContext.Current.CorrelationId(cid);

            var returnValue = await serviceProxy.DoItAsync(
                new RequestDataModel
                    {
                        IntValue = 123,
                        StringValue = "OneTwoThree"
                    },
                "The text");

            if (string.CompareOrdinal(cid, returnValue.CorrelationId) != 0)
            {
                throw new Exception("Correlation id does not get returned, Holmes");
            }
        }

    }
}