using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace newminiproject2.ViewModle
{
    public class UserViewModle
    {
        public int Id { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int? MobileNumber { get; set; }
        public string Password { get; set; }

        public string RePassword { get; set; }

        public string NewPassword { get; set; }


    }
}