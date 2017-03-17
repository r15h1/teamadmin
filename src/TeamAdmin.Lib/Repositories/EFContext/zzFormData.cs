using System;

namespace TeamAdmin.Lib.Repositories.EFContext
{
    internal class zzFormData
    {
        public int FormId { get; set; }
        public long? FormDataId { get; set; }
        public string Data { get; set; }
        public DateTime DateCreated { get; set; }
        public bool? Viewed { get; set; }
    }
}
