using System.Collections.Generic;

namespace TeamAdmin.Core.Repositories
{
    public interface IMediaRepository<T> where T: class
    {
        IEnumerable<Media> AddMedia(int entityId, IEnumerable<Media> mediaList);
        IEnumerable<Media> GetMedia(int entityId);
        int GetMediaCount(int entityId);
        bool DeleteMedia(long mediaId);
        void UpdateMediaCaption(long mediaId, string newCaption);
        bool SetMediaPosition(long mediaId, int newPosition);
    }
}