using Sefacan.Core.Entities;
using Sefacan.Data;
using System.Collections.Generic;
using System.Linq;

namespace Sefacan.Service
{
    public class TagService : ITagService
    {
        #region Fields
        private readonly IRepository<Tag> tagRepository;
        private readonly IRepository<PostTag> postTagRepository;
        #endregion

        #region Ctor
        public TagService(IRepository<Tag> _tagRepository,
            IRepository<PostTag> _postTagRepository)
        {
            tagRepository = _tagRepository;
            postTagRepository = _postTagRepository;
        }
        #endregion

        #region Methods
        public IEnumerable<Tag> GetTags(int take = 0)
        {
            if (take == 0)
                return tagRepository.TableNoTracking.ToList();

            return tagRepository.TableNoTracking.Take(take).ToList();
        }

        public IEnumerable<Tag> GetTags(int postId, int take = 0)
        {
            if (take == 0)
            {
                return (from t in tagRepository.TableNoTracking
                        join tp in postTagRepository.Table on t.Id equals tp.TagId
                        where tp.PostId == postId
                        select t).ToList();
            }

            return (from t in tagRepository.TableNoTracking
                    join tp in postTagRepository.Table on t.Id equals tp.TagId
                           where tp.PostId == postId
                           select t).Take(take).ToList();
        }
        #endregion
    }
}