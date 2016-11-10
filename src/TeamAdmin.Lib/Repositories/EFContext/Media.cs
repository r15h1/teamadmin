using System.ComponentModel.DataAnnotations;

namespace TeamAdmin.Lib.Repositories.EFContext
{
    internal class Media
    {
        public string Caption { get; set; }

        [Key]
        public long? MediaId { get; set; }
        public byte MediaType { get; set; }
        public string Url { get; set; }
        public int Position { get; set; }
    }
}