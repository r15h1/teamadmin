namespace TeamAdmin.Lib.Repositories.EFContext
{
    internal class Club
    {
        public int? ClubId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public bool? Deleted { get; set; }
    }
}
