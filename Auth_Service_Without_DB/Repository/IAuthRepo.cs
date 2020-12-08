using Auth_Service_Without_DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth_Service_Without_DB.Repository
{
    
        public interface IAuthRepo
        {
            public User Login(User user);
        }
    
}
