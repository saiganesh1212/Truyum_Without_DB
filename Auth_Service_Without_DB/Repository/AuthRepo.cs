using Auth_Service_Without_DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth_Service_Without_DB.Repository
{
    public class AuthRepo : IAuthRepo
    {
        private static List<User> users=new List<User>() { new User { UserId = 1, Username = "Ganesh", Password = "Sai@1212",Role="Admin" }, new User { UserId = 2, Username = "Deepak", Password = "Deep@40",Role="User" }, new User { UserId = 3, Username = "Nani", Password = "Nan@145",Role="User" }, new User { UserId = 4, Username = "Suresh", Password = "Sur@34",Role="User" } };
        
        public User Login(User user)
        {
            var userdetails= users.Where(x=>x.Username==user.Username&&x.Password==user.Password).FirstOrDefault();
            return userdetails;
        }
    }
}
