using System.Collections.Generic;

namespace Sefacan.Web.Models
{
    public class FooterModel
    {
        public FooterModel()
        {
            Categories = new List<CategoryModel>();
            Tags = new List<TagModel>();
        }

        public IEnumerable<CategoryModel> Categories { get; set; }
        public IEnumerable<TagModel> Tags { get; set; }
    }
}