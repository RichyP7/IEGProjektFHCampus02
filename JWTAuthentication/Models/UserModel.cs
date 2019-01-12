using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthentication.Models
{
    public class UserModel
    {
        private string username;
        private string emailaddress;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        

        public string EmailAddress
        {
            get { return emailaddress; }
            set { emailaddress = value; }
        }


    }
}
