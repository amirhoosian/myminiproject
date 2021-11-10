using System;
using System.Collections.Generic;

#nullable disable

namespace newminiproject2.Models
{
    public partial class User
    {
        public User()
        {
            ChatContactPages = new HashSet<Chat>();
            ChatSenderPages = new HashSet<Chat>();
            Messages = new HashSet<Message>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int? MobileNumber { get; set; }
        public string Password { get; set; }
        public string ProfileImageUrl { get; set; }
        public string RePassword { get; set; }

        public virtual ICollection<Chat> ChatContactPages { get; set; }
        public virtual ICollection<Chat> ChatSenderPages { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
