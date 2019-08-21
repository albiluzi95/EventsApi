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
    [Route("user")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        public MongoDatabase mongoDatabase;
        private UserCollection _user = new UserCollection();

        [HttpPost]
        public string addvoter([FromBody] dynamic loginInfo)
        {
            try
            {
            string userName = loginInfo["username"];
            string password = loginInfo["password"];
            return _user.loginUser(userName, password);
            }
            catch (Exception e) {
                throw new Exception("Insert All the inputs");
            }
        }

        [HttpPut]
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