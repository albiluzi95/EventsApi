using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApi.Models.Repository
{
    public class VotersCollection
    {
        internal MongoDbRepo _repo = new MongoDbRepo();
        //Contains all documents inside the collection
        public IMongoCollection<Event> Collection;

        //Constructor
        public VotersCollection()
        {
            Collection = _repo.Db.GetCollection<Event>("events");
        }

        public string AddVoter(int eventid, int SessionId, string VoterName)
        {
            try
            {
                Event @event = this.Collection.Find(new BsonDocument { { "id", eventid } }).FirstAsync().Result; 
                @event.sessions.Where(s => s.session_id == SessionId).FirstOrDefault().voters.Add(VoterName);
                var update = Collection.FindOneAndUpdateAsync(Builders<Event>.Filter.Eq("id", @event.id ), Builders<Event>.Update.Set("sessions", @event.sessions));
                return "Success";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public string DeleteVoter(int eventid, int SessionId, string VoterName)
        {
            try
            {
                Event @event = this.Collection.Find(new BsonDocument { { "id", eventid } }).FirstAsync().Result;
                @event.sessions.Where(s => s.session_id == SessionId).FirstOrDefault().voters.Remove(VoterName);
                var update = Collection.FindOneAndUpdateAsync(Builders<Event>.Filter.Eq("id", @event.id), Builders<Event>.Update.Set("sessions", @event.sessions));
                return "Success";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool UserHasVoter(int eventid, int SessionId, string VoterName)
        {
            try
            {
                List<Event> events = Collection.Find(new BsonDocument()).ToListAsync().Result;
                Event @event = events.Where(e => e.id == eventid).FirstOrDefault();
                Sessions session = @event.sessions.Where(s => s.session_id == SessionId).FirstOrDefault();
                return session.voters.Contains(VoterName);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}