using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLogic.Users.Messages
{
    public class GetUserReponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
