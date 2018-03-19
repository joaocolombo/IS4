using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClienteIdentity.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        // this api call other api, so this is just for tests dont judge 
        [HttpGet]
        public IActionResult Get()
        {
            //get IS4 connection(IS4 url)
            var disco = DiscoveryClient.GetAsync("https://localhost:44332").Result;
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
            }

            //get a endpoint, endpoint is a client configurete in IS4  config
            var tokenClient = new TokenClient(disco.TokenEndpoint, "sistemasbischoffapi", "85200a6b-fdea-4864-9abd-6dbe77b5936e"
                , AuthenticationStyle.PostValues);
            

            // so here is where the magic happens, heheh, where we go get a token in IS4, 
            // so we need pi the rigth request client(the request set in the IS4 config)
             var tokenResponse = tokenClient.RequestClientCredentialsAsync("api1" ).Result;
            // var tokenResponse = tokenClient.RequestResourceOwnerPasswordAsync("joao.c@outlook.com"
            // ,"123","api1").Result;
             
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return Ok(tokenResponse.Error);
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            // call other api
            // here we create a request for the other api, we need set the token in here
            //if you want see the token acess jwt.io
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);
            //just put de api destin and be haapy(api url)
            var response = client.GetAsync("http://localhost:5001/api/values").Result;
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                return BadRequest(response.StatusCode);
            }

            var content = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content);
            return Ok(tokenResponse.AccessToken);
        }





        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
