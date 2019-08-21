using EventsApi.Models.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EventsApi.Controllers
{
    [Route("voters")]
    public class VotersController : ApiController
    {
        public MongoDatabase mongoDatabase;
        private VotersCollection _voters = new VotersCollection();

        [HttpPost]
        public string addvoter(int eventid, int sessionid, string votername)
        {
            return _voters.AddVoter(eventid, sessionid, votername);
        }

        [HttpDelete]
        public string deletevoter(int eventid, int sessionid, string votername)
        {
            return _voters.DeleteVoter(eventid, sessionid, votername);
        }
    }
}