using Sefacan.Core.Entities;
using Sefacan.Data;
using System.Collections.Generic;
using System.Linq;

namespace Sefacan.Service
{
    public class CategoryService : ICategoryService
    {
        #region Fields
        private readonly IRepository<Category> categoryRepository;
        #endregion

        #region Ctor
        public CategoryService(IRepository<Category> _categoryRepository)
        {
            categoryRepository = _categoryRepository;
        }
        #endregion

        #region Method
        public Category GetById(int Id)
        {
            return categoryRepository.Find(x => x.Id == Id);
        }

        public IEnumerable<Category> GetActives()
        {
            return (from c in categoryRepository.TableNoTracking
                    where c.IsActive && !c.IsDelete
                    orderby c.Name
                    select c).ToList();
        }

        public IEnumerable<Category> GetCategories()
        {
            return (from c in categoryRepository.TableNoTracking
                    orderby c.Name
                    select c).ToList();
        }
        
        public bool InsertCategory(Category category)
        {
            return categoryRepository.Insert(category);
        }

        public bool UpdateCategory(Category category)
        {
            return categoryRepository.Update(category);
        }

        public bool DeleteCategory(Category category)
        {
            return categoryRepository.Delete(category);
        }
        #endregion
    }
}