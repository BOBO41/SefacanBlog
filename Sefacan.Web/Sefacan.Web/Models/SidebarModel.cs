using System.Collections.Generic;

namespace Sefacan.Web.Models
{
    public class SidebarModel
    {
        public SidebarModel()
        {
            Categories = new List<CategoryModel>();
            RecentPosts = new List<PostModel>();
            PopularPosts = new List<PostModel>();
            Tags = new List<TagModel>();
            RecentComments = new List<CommentModel>();
        }

        public IEnumerable<CategoryModel> Categories { get; set; }
        public IEnumerable<PostModel> RecentPosts { get; set; }
        public IEnumerable<PostModel> PopularPosts { get; set; }
        public IEnumerable<TagModel> Tags { get; set; }
        public IEnumerable<CommentModel> RecentComments { get; set; }
    }
}