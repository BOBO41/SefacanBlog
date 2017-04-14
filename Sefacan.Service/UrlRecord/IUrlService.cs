using Sefacan.Core.Entities;
using Sefacan.Core.Enums;

namespace Sefacan.Service
{
    public interface IUrlService
    {
        UrlRecord GetUrl(int Id);
        UrlRecord GetUrl(string url);
        UrlRecord GetByEntity(int entityId, EntityType type);
        string GetUrl(int entityId, EntityType type);
        bool UpdateUrl(UrlRecord urlRecord);
        bool InsertUrl(UrlRecord urlRecord);
    }
}