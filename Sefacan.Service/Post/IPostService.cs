using Sefacan.Core.Entities;
using System.Collections.Generic;

namespace Sefacan.Service
{
    public interface IPostService
    {
        IEnumerable<Post> GetPosts();
        Post GetById(int Id);
        IEnumerable<Post> GetActives();
        IEnumerable<Post> GetActives(int categoryId);
        IEnumerable<Post> GetRecents();
        IEnumerable<Post> GetPopulars();
        IEnumerable<Post> GetRelatedPosts(int categoryId, int postId);
        IEnumerable<Comment> GetComments(int postId);
        IEnumerable<Comment> GetChildComments(int postId, int parentId);
        int GetCommentCount(int postId);
        IEnumerable<Comment> GetRecentComments(int take = 0);
        int GetCount(int categoryId);
        IEnumerable<Post> SearchPosts(string term);
        bool UpdatePost(Post post);
        bool InsertPost(Post post);
        bool DeletePost(Post post);
        bool InsertComment(Comment comment);
    }
}