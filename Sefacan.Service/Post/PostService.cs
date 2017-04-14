using Sefacan.Core.Entities;
using Sefacan.Data;
using System.Collections.Generic;
using System.Linq;

namespace Sefacan.Service
{
    public class PostService : IPostService
    {
        #region Fields
        private readonly IRepository<Post> postRepository;
        private readonly IRepository<Comment> commentRepository;
        #endregion

        #region Ctor
        public PostService(IRepository<Post> _postRepository,
            IRepository<Comment> _commentRepository)
        {
            postRepository = _postRepository;
            commentRepository = _commentRepository;
        }
        #endregion

        #region Methods
        public IEnumerable<Post> GetPosts()
        {
            return postRepository.TableNoTracking.ToList();
        }

        public Post GetById(int Id)
        {
            return postRepository.Find(x => x.Id == Id);
        }

        public IEnumerable<Post> GetActives()
        {
            return (from p in postRepository.TableNoTracking
                    where p.IsActive && !p.IsDelete
                    orderby p.CreateDate descending
                    select p).ToList();
        }

        public IEnumerable<Post> GetActives(int categoryId)
        {
            return (from p in postRepository.TableNoTracking
                    where p.CategoryId == categoryId &&
                    p.IsActive && !p.IsDelete
                    orderby p.CreateDate descending
                    select p).ToList();
        }

        public IEnumerable<Post> GetRecents()
        {
            return (from p in postRepository.TableNoTracking
                    where p.IsActive && !p.IsDelete
                    orderby p.CreateDate descending
                    select p).Take(3).ToList();
        }

        public IEnumerable<Post> GetPopulars()
        {
            return (from p in postRepository.TableNoTracking
                    where p.IsActive && !p.IsDelete
                    orderby p.ViewCount descending
                    select p).Take(3).ToList();
        }

        public IEnumerable<Post> GetRelatedPosts(int categoryId, int postId)
        {
            return (from p in postRepository.TableNoTracking
                    where p.CategoryId == categoryId && p.Id != postId &&
                    p.IsActive && !p.IsDelete
                    orderby p.ViewCount descending
                    select p).Take(3).ToList();
        }

        public IEnumerable<Comment> GetComments(int postId)
        {
            return commentRepository.Get(x => x.PostId == postId && x.ParentId == 0).OrderByDescending(x => x.CreateDate).ToList();
        }

        public IEnumerable<Comment> GetChildComments(int postId, int parentId)
        {
            return commentRepository.Get(x => x.PostId == postId && x.ParentId == parentId).OrderBy(x => x.CreateDate).ToList();
        }

        public int GetCommentCount(int postId)
        {
            return commentRepository.TableNoTracking.Count(x => x.PostId == postId);
        }

        public IEnumerable<Comment> GetRecentComments(int take = 0)
        {
            if (take == 0)
                return commentRepository.TableNoTracking.OrderByDescending(x => x.CreateDate)
                    .GroupBy(x => x.PostId).Select(x => x.FirstOrDefault()).ToList();

            return commentRepository.TableNoTracking.OrderByDescending(x => x.CreateDate)
                   .GroupBy(x => x.PostId).Select(x => x.FirstOrDefault()).Take(take).ToList();
        }

        public int GetCount(int categoryId)
        {
            return (from p in postRepository.TableNoTracking
                    where p.CategoryId == categoryId &&
                    p.IsActive && !p.IsDelete
                    orderby p.CreateDate descending
                    select p).Count();
        }

        public IEnumerable<Post> SearchPosts(string term)
        {
            return (from p in postRepository.TableNoTracking
                    where (p.IsActive && !p.IsDelete) &&
                    (p.Title.Contains(term) || p.Description.Contains(term) ||
                    p.ShortContent.Contains(term) || p.FullContent.Contains(term))
                    orderby p.CreateDate descending
                    select p).ToList();
        }

        public bool UpdatePost(Post post)
        {
            var prevValue = postRepository.IsSetAutoEditableValue;
            postRepository.IsSetAutoEditableValue = false;
            var result = postRepository.Update(post);
            postRepository.IsSetAutoEditableValue = prevValue;
            return result;
        }

        public bool InsertPost(Post post)
        {
            return postRepository.Insert(post);
        }

        public bool DeletePost(Post post)
        {
            return postRepository.Delete(post);
        }

        public bool InsertComment(Comment comment)
        {
            return commentRepository.Insert(comment);
        }
        #endregion
    }
}