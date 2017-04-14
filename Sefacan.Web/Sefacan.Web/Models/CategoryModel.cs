using Sefacan.Framework.Paging;

namespace Sefacan.Web.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int PostCount { get; set; }
    }

    public class CategoryDetailModel
    {
        public string Name { get; set; }
        public IPagedList<PostModel> Posts { get; set; }
    }
}