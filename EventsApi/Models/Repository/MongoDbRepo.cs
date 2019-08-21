using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsApi.Models.Repository
{
    public class MongoDbRepo
    {
        //The client that manage the connection
        public MongoClient Client;
        //The interface that manage the database
        public IMongoDatabase Db;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">Mongo server url</param>
        /// <param name="database">Database name</param>
        public MongoDbRepo()
        {
            string url = "mongodb://albiluzi1:albiluzi1@ds155606.mlab.com:55606/events";
            this.Client = new MongoClient(url);
            //if the database is not exist, creates the database
            string database = "events";
            this.Db = this.Client.GetDatabase(database);
        }
    }
}