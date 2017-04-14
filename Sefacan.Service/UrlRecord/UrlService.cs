using Sefacan.Core.Entities;
using Sefacan.Core.Enums;
using Sefacan.Data;

namespace Sefacan.Service
{
    public class UrlService : IUrlService
    {
        #region Fields
        private readonly IRepository<UrlRecord> urlRepository;
        #endregion

        #region Ctor
        public UrlService(IRepository<UrlRecord> _urlRepository)
        {
            urlRepository = _urlRepository;
        }
        #endregion

        #region Methods
        public UrlRecord GetUrl(int Id)
        {
            return urlRepository.Find(x => x.Id == Id);
        }

        public UrlRecord GetUrl(string url)
        {
            return urlRepository.Find(x => x.UniqueUrl == url);
        }

        public UrlRecord GetByEntity(int entityId, EntityType type)
        {
            return urlRepository.Find(x => x.EntityId == entityId && x.EntityType == type);
        }

        public string GetUrl(int entityId, EntityType type)
        {
            var urlRecord = urlRepository.Find(x => x.EntityId == entityId && x.EntityType == type);

            if (urlRecord != null)
                return urlRecord.UniqueUrl;

            return string.Empty;
        }

        public bool UpdateUrl(UrlRecord urlRecord)
        {
            return urlRepository.Update(urlRecord);
        }

        public bool InsertUrl(UrlRecord urlRecord)
        {
            return urlRepository.Insert(urlRecord);
        }
        #endregion
    }
}