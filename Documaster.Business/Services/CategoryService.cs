using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Documaster.Model.Entities;

namespace Documaster.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IGenericRepository<Category> _categoryRepository;

        public CategoryService(IGenericRepository<Category> categoryRepository,
                               IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        //Create new category
        public Category CreateCategory(Category category)
        {
           var newCategory = _categoryRepository.Create(category);
           _unitOfWork.SaveChanges();
           return newCategory;
        }

        //Get category by specific Id
        public Category GetById(int id)
        {
            return _categoryRepository.Get(id);
        }

        //Get all categories
        public List<Category> GetCategories()
        {
            return _categoryRepository.GetAll().OrderBy(x => x.Name).ToList();
        }

        //Delete category by specific Id
        public Category DeleteById(int id)
        {
            return _categoryRepository.Get(id);
        }

        //Delete category
        public bool DeleteCategory(Category category)
        {
            var deleteCategory = _categoryRepository.Delete(category.Id);
            _unitOfWork.SaveChanges();
            return deleteCategory;
        }

        //Edit category
        public bool EditCategory(Category category)
        {
            var edit = _categoryRepository.Update(category, new List<string> { "Name" });
            _unitOfWork.SaveChanges();
            return edit;
        }

    
    }
}
