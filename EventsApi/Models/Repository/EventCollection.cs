using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApi.Models.Repository
{
    public class EventCollection
    {
        internal MongoDbRepo _repo = new MongoDbRepo();
        //Contains all documents inside the collection
        public IMongoCollection<Event> Collection;

        //Constructor
        public EventCollection()
        {
            Collection = _repo.Db.GetCollection<Event>("events");
        }

        public List<Event> GetEvents()
        {
            var query = Collection.Find(new BsonDocument()).ToListAsync();
            return query.Result;
        }

        public Event GetEvent(int id)
        {
            return this.Collection.Find(new BsonDocument { { "id", id } }).FirstAsync().Result;
        }

        public string addEvent(Event tempEvent)
        {
            try
            {
                if(this.Collection.Find(new BsonDocument { { "id", tempEvent.id } }).CountDocuments() > 0)
                {
                    this.Collection.FindOneAndUpdateAsync(Builders<Event>.Filter.Eq("id", tempEvent.id), Builders<Event>.Update.Set("sessions", tempEvent.sessions));
                } else
                {
                    tempEvent._id = ObjectId.GenerateNewId();
                    this.Collection.InsertOneAsync(tempEvent);
                }
                return "Success";
            }
            catch (Exception)
            {

                return "Event was not added in.";
            }
        }
    }
}