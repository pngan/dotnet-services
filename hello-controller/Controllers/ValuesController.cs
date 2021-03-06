﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Greet;
using Grpc.Core;

namespace hello_controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            // The port number here must match the port of the gRPC server
            var channel = new Channel("hello-grpc:80", ChannelCredentials.Insecure);
            var client = new Greeter1.Greeter1Client(channel);

            var reply = await client.SayHolaAsync(
                                          new HelloRequest { Name = "Greeter1Client" });
            Console.WriteLine("Greeting1: " + reply.Message);

            await channel.ShutdownAsync();
            return new string[] { reply.Message };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
