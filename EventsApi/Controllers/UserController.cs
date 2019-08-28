using EventsApi.Models;
using EventsApi.Models.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EventsApi.Controllers
{
    
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        public MongoDatabase mongoDatabase;
        private UserCollection _user = new UserCollection();

        [HttpPost]
        [Route("user/login")]
        public User login(string username, string password)
        {
            try
            {
            return _user.loginUser(username, password);
            }
            catch (Exception e) {
                throw new Exception("Insert All the inputs");
            }
        }

        [HttpPut]
        [Route("user")]
        public string updateCurrentUser([FromBody] User currentUser)
        {
            try
            {
                //return _user.updateCurrentUser(currentUser);
                return "success";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


    }
}