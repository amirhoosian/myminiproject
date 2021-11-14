using System;
using System.Collections.Generic;

#nullable disable

namespace newminiproject2.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public int? SenderPageId { get; set; }
        public string Message1 { get; set; }
        public bool? IsDeleted { get; set; }
        public int ChatId { get; set; }

        public virtual User Chat { get; set; }
        public virtual Chat SenderPage { get; set; }
    }
}
