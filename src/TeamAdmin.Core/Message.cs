using System;

namespace TeamAdmin.Core
{
    public class Message
    {
        public long? MessageId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime? DateCreated{ get; set; }
        public  bool? Viewed { get; set; }
    }
}
