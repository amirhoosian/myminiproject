using System;
using System.Collections.Generic;

#nullable disable

namespace newminiproject2.Models
{
    public partial class Chat
    {
        public Chat()
        {
            Messages = new HashSet<Message>();
        }

        public int Id { get; set; }
        public int SenderPageId { get; set; }
        public int ContactPageId { get; set; }

        public virtual User ContactPage { get; set; }
        public virtual User SenderPage { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
