using EventsApi.Models;
using EventsApi.Models.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace EventsApi.Controllers
{
    public class EventController : ApiController
    {
        public MongoDatabase mongoDatabase;
        private EventCollection _events = new EventCollection();

        [HttpGet]
        [Route("events")]
        public  List<Event> GetEvents()
        {
            return _events.GetEvents();
        }

        [HttpGet]
        [Route("events/{id:int}")]
        public Event GetEvent(int id)
        {
            return _events.GetEvent(id);
        }
        [HttpPost]
        [Route("events")]
        public string AddEvent(Event @event)
        {
            var response = _events.addEvent(@event);
            return response;
        }
    }
}