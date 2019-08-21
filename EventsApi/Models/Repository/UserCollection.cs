using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApi.Models.Repository
{
    public class UserCollection
    {
        internal MongoDbRepo _repo = new MongoDbRepo();
        //Contains all documents inside the collection
        public IMongoCollection<User> Collection;

        //Constructor
        public UserCollection()
        {
            Collection = _repo.Db.GetCollection<User>("users");
        }

    }
}
