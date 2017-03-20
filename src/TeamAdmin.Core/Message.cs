using System;

namespace TeamAdmin.Core
{
    public class Message
    {
        public long? MessageId { get; set; }
        public MessageType MessageType { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime? DateCreated{ get; set; }
        public  bool? Viewed { get; set; }
    }

    public enum MessageType
    {
        General_Information = 1,
        Registration = 2,
        TryOut = 3,
        SummerCamp = 4
    }
}
