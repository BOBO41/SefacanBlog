using System;
using System.Collections.Generic;

namespace Sefacan.Web.Models
{
    public class PostModel
    {
        public string PicturePath { get; set; }
        public string Title { get; set; }
        public string ShortContent { get; set; }
        public string CategoryName { get; set; }
        public string CategoryUrl { get; set; }
        public int ViewCount { get; set; }
        public int CommentCount { get; set; }
        public string Url { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class PostDetailModel
    {
        public PostDetailModel()
        {
            Tags = new List<TagModel>();
            RelatedPosts = new List<PostModel>();
            Comments = new List<CommentListModel>();
        }

        public int Id { get; set; }
        public string PicturePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FullContent { get; set; }
        public string Url { get; set; }
        public string CategoryName { get; set; }
        public string CategoryUrl { get; set; }
        public int ViewCount { get; set; }
        public int CommentCount { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool AllowComment { get; set; }

        public IEnumerable<TagModel> Tags { get; set; }
        public IEnumerable<PostModel> RelatedPosts { get; set; }
        public IEnumerable<CommentListModel> Comments { get; set; }
    }

    public class CommentModel
    {
        public string FullName { get; set; }
        public string PostName { get; set; }
        public string PostUrl { get; set; }
    }

    public class CommentListModel
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string FullName { get; set; }
        public string Content { get; set; }
        public string CreateDate { get; set; }
        public IEnumerable<CommentListModel> ChildComments { get; set; }
    }
}