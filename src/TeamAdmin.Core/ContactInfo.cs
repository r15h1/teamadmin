namespace TeamAdmin.Core
{
    public class ContactInfo
    {
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
    }

    public class PlayerContactInfo : ContactInfo
    {
        public string ContactName { get; set; }
        public string Relationship { get; set; }

    }
}