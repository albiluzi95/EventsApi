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
            Collection = _repo.Db.GetCollection<User>("user");
        }

        public string SaveNewUser(string userName, string password)
        {
            try
            {
                User user = new User();
                user._id = ObjectId.GenerateNewId();
                user.userName = userName;
                user.password = password;
                user.id = 0;
                user.firstName = "";
                user.lastName = "";
                this.Collection.InsertOneAsync(user);
                return "Success";
            }
            catch (Exception e)
            {

                return e.Message;
            }
        }

        public string loginUser(string userName, string password)
        {
            try
            {
              User user = this.Collection.Find(new BsonDocument { { "userName", userName } , { "password", password} }).FirstAsync().Result;
                return "Success";
            }
            catch (Exception e)
            {

                return e.Message;
            }
        }

    }
}
