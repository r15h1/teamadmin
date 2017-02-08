namespace TeamAdmin.Lib.Repositories.EFContext
{
    internal class TeamMedia : Media
    {
        public int? TeamId { get; set; }

        public virtual Team Team { get; set; }
    }
}
