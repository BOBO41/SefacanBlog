using Sefacan.Core.Entities;
using System.Collections.Generic;

namespace Sefacan.Service
{
    public interface ICategoryService
    {
        Category GetById(int Id);
        IEnumerable<Category> GetActives();
        IEnumerable<Category> GetCategories();
        bool InsertCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
    }
}