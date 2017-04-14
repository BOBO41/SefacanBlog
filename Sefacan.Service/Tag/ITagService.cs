using Sefacan.Core.Entities;
using System.Collections.Generic;

namespace Sefacan.Service
{
    public interface ITagService
    {
        IEnumerable<Tag> GetTags(int take = 0);
        IEnumerable<Tag> GetTags(int postId, int take = 0);
    }
}