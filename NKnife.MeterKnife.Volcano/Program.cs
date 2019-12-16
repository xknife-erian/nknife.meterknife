using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.Owin.Hosting;
using Owin;

namespace NKnife.MeterKnife.Volcano
{
    internal class Program
    {
        public static bool IsRun { get; set; } = true;

        private static void Main(string[] args)
        {
            var baseAddress = "http://localhost:34401/";

            WebApp.Start<Startup>(baseAddress);
            while (IsRun)
                Console.ReadLine();
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "v1/{controller}/{id}",
                new {id = RouteParameter.Optional}
            );

            appBuilder.UseWebApi(config);
        }
    }

    public class ValuesController : ApiController
    {
        // GET api/values 
        public IEnumerable<string> Get()
        {
            return new[] {$"{DateTime.Now:O}", Guid.NewGuid().ToString()};
        }

        // GET api/values/5 
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values 
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5 
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
        }
    }
}