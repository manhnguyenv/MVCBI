using System.Collections.Generic;
using System.Web.Http;
using VASJ.BI.Filters;

namespace VASJ.BI.WebAPI
{
    [JwtAuthentication]
    public class ValuesController : ApiController
    {
        // GET api/<controller>
        [JwtAuthentication]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}