using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApi.Models
{
    public class Event
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public decimal price { get; set; }
        public string imageUrl { get; set; }
        public Location location { get; set; }
        public string onlineUrl { get; set; }
        public List<Sessions> sessions { get; set; }

        public class Location
        {
            public string address { get; set; }
            public string city { get; set; }
            public string country { get; set; }

        }
    }

    public class Sessions
    {
        public int session_id { get; set; }
        public string name { get; set; }
        public string presenter { get; set; }
        public int duration { get; set; }
        public string level { get; set; }
        public string @abstract { get; set; }
        public List<string> voters { get; set; }
    }
}