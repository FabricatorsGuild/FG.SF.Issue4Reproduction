using ActorService1.Interfaces;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;

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
            //var t1 = TestServiceAsync();
            //await t1;

            var t2 = TestActorAsync();
            await t2;

            //await Task.WhenAll(t1, t2);
        }

        private static async Task TestActorAsync()
        {
            // new Uri("fabric:/FG.SF.Issue4Reproduction/StatelessService1")
            var actorProxy = ActorProxy.Create<IActorService1>(new ActorId(1));

            var cid = Guid.NewGuid().ToString("D");
            CallContext.Current.CorrelationId(cid);

            var correlationId = await actorProxy.GetCorrelationIdAsync(123);

            if (string.CompareOrdinal(cid, correlationId) != 0)
            {
                throw new Exception("Correlation id does not get returned, Holmes");
            }
        }

        private static async Task TestServiceAsync()
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